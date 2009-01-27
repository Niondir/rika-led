/*
  Schild Firmware
	T.Rohde, Rika Projekt
*/
#include <avr/io.h>
#include <avr/interrupt.h> 
#include <util/delay.h>
#include <avr/sleep.h>
#include <stdlib.h>
#include <stdint.h>
#include <stdio.h>
#include <string.h>

#include "main.h"
#include "uart.h"
#include "xbee.h"
#include "board.h"
#include "display.h"

// Gloable Vars
sign_t    sign;
packet_t  packet;
trace_t   trace;
uint8_t   csum_debug = 22;
uint8_t   csum_debug_calc = 22;

#define DEBUG_ON

int main(void)
{
  uint8_t packetRecvStatus;

	char    debug[21];
	uint8_t toggleFlag=0;

  _delay_ms(1000);     // startup delay
  sign.signType = SIGN_TYPE_NOTHING;
  sei();               // global irq an
  uartSW_init();       // software uart
  init_Display(4);     // Display initialisieren
  initPIOs();          // Mode Jumper Pins konfigurieren
  detectSignMode();    // Hardware kann von extern als Wagen oder Presischild konfiguriert werden, Preisschiler sogar mit bis zu 7 Adressen
  initTraceCounter(1); // TraceTimer konfigurieren und starten

  show_status();       // Schild Informationen anzeigen, insbesondere SchildID und Typ (AD/Price)
  _delay_ms(2000);     // Zeit zum Anzeigen der Informationen

  while(1)
  {
		if ( (packetRecvStatus = get_packet()) == 0)
		{ 
		   //debug_packet();
		    packet_action();
			  if(sign.displayRefreshFlag)
			  {
				   clr_Screen();
	         write_Display(sign.displayMemory[0],1,1);
					 write_Display(sign.displayMemory[1],1,2);
					 write_Display(sign.displayMemory[2],1,3);
					 write_Display(sign.displayMemory[3],1,4);
					 sign.displayRefreshFlag = 0;
			   }
       
  #ifdef DEBUG_ON
	       toggleFlag=!toggleFlag;
				 if(toggleFlag)
				 {
			   	sprintf(debug, "OK");
				  write_Display(debug,19,4);			   
				 }
				 else
				 {
			   	sprintf(debug, "ok");
				  write_Display(debug,19,4);			   
				 }
  #endif
		}	
	#ifdef DEBUG_ON
		else
		{ // Debugging
			sprintf(debug, "E%d",packetRecvStatus);
			write_Display(debug,19,4);
			_delay_ms(500);
		} 
  #endif

		if(detectSignMode()) // Mode wurde verstellt!
		{
       show_status();       // Schild Informationen anzeigen, insbesondere SchildID und Typ (AD/Price)
       _delay_ms(2000);     // Zeit zum Anzeigen der Informationen	
			 sign.displayRefreshFlag = 1;	 //Display Memory neuzeichnen, da durch show_status anderer Inhalt angezeigt wurde 
		}
  }

  return 0;
}



uint8_t detectSignMode(void) //gibt 1 zurück wenn sich der Mode geändert hat
{
  
	uint8_t signType_yet = sign.signType;
	uint8_t adr_yet      = sign.signUniqueID;
	
	if(!ISSET_PIO1 && !ISSET_PIO2 && !ISSET_PIO3) // kein Jumper = Wagen-Schild
  {

    sign.signType        = SIGN_TYPE_TROLLEY;
	  sign.signUniqueID = 0; // Dummy, wird bei Wagenschild nicht benötigt
    //initXbee();          // Xbee auf zeiladresse des trace empfängers konfigurieren TODO

  }
  else // Preis-Schild mit 3 Bit Adresse über Jumper einstellbar
  {
    char adr=0;

		sign.signType        = SIGN_TYPE_PRICE;
	  
		// Adresse dekodieren, nur für Präsentation um unterschiedliche Preisschilder realisieren zu können
		adr|=ISSET_PIO1;
	  adr|=ISSET_PIO2<<1;
		adr|=ISSET_PIO3<<2;

		sign.signUniqueID=(uint16_t)adr;
  }

	if((signType_yet!=sign.signType) || (adr_yet!=sign.signUniqueID))return 1;
	else                           return 0;

}


void show_status(void)
{
  char      textvalue[25];

	clr_Screen();

  //Status Infos ausgeben
  sprintf(textvalue, "FirmwareVersion:%d",  FIRMWARE_VERSION);
  write_Display(textvalue,1,1);

  if(sign.signType==SIGN_TYPE_TROLLEY) write_Display("SignType: Trolley", 1, 2);
  else write_Display("SignType: Price ", 1, 2);

  sprintf(textvalue, "UniqueSignID: %d",  sign.signUniqueID);
  write_Display(textvalue,1,3); 

}


// Versucht Pakete der Form <CMD-NR|Field1|Field2...|....|FieldN> zu empfangen, prüft auch die csum
// Returnt 0 bei korrekt empfangenen Paket
int8_t get_packet(void)
{

  int8_t  lastChar;
  char    wBuff[10];   									 //working Buffer für die CMDnr
  int8_t  pos=0;
  int8_t  ArgAnzahl;
  uint8_t csum=0;     									 //checksummen immer über alles bis vor der csum, inklusive "|", ohne "<" 
  char    csumCompare[4];                                 //8Bit Checksum max Val 254+'\0' = 4 Zeichen
  uint8_t  csumCompareInt;
  

 // Reset buffers
  csum_debug_calc = 0;
  csum_debug = 0;

  while(uartSW_getc_wait()!='<');                        //Auf Start eines Pakets warten
  //if (uartSW_getc_wait()!='<') return 1;
 

// CMD Nr empfangen:
  
  //CMD Nr muss folgen
  lastChar = uartSW_getc_wait();                         
  if(lastChar<'0' || lastChar>'9') return 2;               //Keine Ziffer, Fehler
  else 
  {                          
     wBuff[pos++]=lastChar;                              //Ziffer speichern
	 csum+=lastChar;
  }
  
  //Weitere Ziffern oder Separator sind erlaubt, sonst return 1
  do                                                     
  {
	  lastChar = uartSW_getc_wait();  
	  if      ( lastChar == '<' || lastChar == '>') return 3;                            //StartStop Zeichen an unerwarteter Stelle => Fehler

	  if      ( !(lastChar<'0' || lastChar>'9') ) { wBuff[pos++]=lastChar; csum+=lastChar; } //weitere Ziffer abspeichern
      else if ( lastChar == '|')				  { csum+=lastChar;  break;                } //Separator gefunden
	  else                      				    return 4;                                //ungültiges Zeichen
  }while(1);
  //char 2 int wandeln
  wBuff[pos]='\0';                              					   
  packet.packetCmdNr = (uint8_t)atoi(wBuff);



// Argumente empfangen:
  switch(packet.packetCmdNr)
  { 
		  case CMDNR_SEND_TRACE: ArgAnzahl = ARGCNT_SEND_TRACE; break;
			case CMDNR_SET_AD:     ArgAnzahl = ARGCNT_SET_AD;     break;
			case CMDNR_SHOW_ID:    ArgAnzahl = ARGCNT_SHOW_ID;    break;
			case CMDNR_SET_PRICE : ArgAnzahl = ARGCNT_SET_PRICE;  break;
			default:                                              return 5;
  }


  for(int i=0; i<ArgAnzahl; i++)
  {
	  //Argumente empfangen
	  pos=0;
	  do                                                                   
	  {
		  lastChar = uartSW_getc_wait();  
		  if      ( lastChar == '<' || lastChar == '>' || pos > 24) return 6;                            //ungültiges Zeichen, oder zuviele empfangen

		  if      ( lastChar != '|' ) {(packet.args)[i][pos++]=lastChar; csum+=lastChar;}     //Zeichen abspeichern
	      else                        { (packet.args)[i][pos]='\0'; csum+=lastChar;  break; }             //Separator gefunden
	  }while(1);
  }


// Csum empfangen:
   pos=0;
   lastChar = uartSW_getc_wait();  

   //mindestens 1 Zahl muss empfangen werden
   if      ( !(lastChar<'0' || lastChar>'9') ) { csumCompare[pos++]=lastChar;}           //Zahl gefunden
   else    return 7;                                                                  //Keine Zahl = Fehler

   do  //max 2 weitere Ziffern der checksumme empfangen                                                                 
   {
	  lastChar = uartSW_getc_wait();  
	  if ( lastChar == '>') break;                                               //Paket empfang abgeschlossen                         
      
	  if      ( !(lastChar<'0' || lastChar>'9')  && pos<3 ) { csumCompare[pos++]=lastChar; }
	  else return 8;
	  
   }while(1);

   

// Checksummen vergleichen:
  csumCompare[pos]  = '\0'; 
  csumCompareInt    = (uint8_t)atoi(csumCompare);

  csum_debug_calc = csum;
  csum_debug = csumCompareInt;
  if(csumCompareInt==csum)return 0; //Packet korrekt empfangen
  else                    return 9;

}


void packet_action(void)
{

  switch(packet.packetCmdNr)
  { 
    case CMDNR_SEND_TRACE:  

	  					if(trace.pos != 0)
						{
							for(int i=0;i<trace.pos;i++)
							{
							  //xbeesend(trace.times[i]);
							  //xbeesend(trace.lampIDs[i]);
							   trace.pos = 0;
							}
						}
					
						break;

	case CMDNR_SET_AD: 
	                    
										if(sign.signType==SIGN_TYPE_TROLLEY)
										{
			
				              
											//Trace Funktionalität
											if(trace.pos < (TRACE_LAMP_CNT-1)) // nur bearbeiten wenn min. noch ein freier traceplatz zur verfügung steht
											{
							  
												  uint16_t aktLampID = (uint16_t)atoi(packet.args[0]); //lamp id char->int
							  
												  if(trace.pos==0 || (aktLampID != (trace.lampIDs)[trace.pos])) //Nur "Neue" traces bzw. lampenIDs abspeichern
					                {
							                trace.pos++;
															trace.times[trace.pos]   = trace.TimeNow;
															trace.lampIDs[trace.pos] = aktLampID;
												  }
							 							 
							        }

	                    //Werbeanzeige Funktionialität
											if (    (strcmp(sign.displayMemory[0], packet.args[1]) == 0) //
											     && (strcmp(sign.displayMemory[1], packet.args[2]) == 0) 
													 && (strcmp(sign.displayMemory[2], packet.args[3]) == 0) 
													 && (strcmp(sign.displayMemory[3], packet.args[4]) == 0) 
												 ) 
											{
												return; //Der Display Memory ist noch aktuell
											}
											else //Display Memory neuschreiben
											{
												sign.displayRefreshFlag = 1;
												strcpy(sign.displayMemory[0], packet.args[1]); //Text1
												strcpy(sign.displayMemory[1], packet.args[2]); //Text2
												strcpy(sign.displayMemory[2], packet.args[3]); //Text3
												strcpy(sign.displayMemory[3], packet.args[4]); //Text4
											}
					          }

										break;

	case CMDNR_SHOW_ID:      
						          show_status();
											_delay_ms(3000);
											sign.displayRefreshFlag = 1;

											break;

	case CMDNR_SET_PRICE:   

	                  //Check, ob Preis für dieses Schild bestimmt ist  
										if((sign.signType==SIGN_TYPE_PRICE) && ((uint16_t)atoi(packet.args[0])) == sign.signUniqueID) 
										{
										  // Display Memory nochaktuell?
											if ( (strcmp(sign.displayMemory[0], packet.args[1]) == 0) && (strcmp(sign.displayMemory[1], packet.args[2]) == 0) ) 
											{
								
												return; //Der Display Memory ist noch aktuell
											}
											else //Display Memory neuschreiben
											{
												sign.displayRefreshFlag = 1;
												strcpy(sign.displayMemory[0], packet.args[1]); //Name
												strcpy(sign.displayMemory[1], packet.args[2]); //Preis
												memset(sign.displayMemory[2],' ', 20); sign.displayMemory[2][20]='\0';
												memset(sign.displayMemory[3],' ', 20); sign.displayMemory[3][20]='\0';
												//sign.displayMemory[2][0]='\0'; //Zeile 3 leer
												//2sign.displayMemory[3][0]='\0'; //Zeile 4 leer
											}

										}
	
						break;
  }
}


void debug_packet(void)
{
  char buf[25];

  sprintf(buf,"cmd Nr: %d", packet.packetCmdNr);

	sign.displayRefreshFlag = 1;
	strcpy(sign.displayMemory[0], buf); //Cmd Nr
	strcpy(sign.displayMemory[1], packet.args[1]); //Arg1
	strcpy(sign.displayMemory[2], packet.args[2]); //Arg2
	strcpy(sign.displayMemory[3], packet.args[3]); //Arg3
  //strcpy(sign.displayMemory[3], packet.args[4]); //Arg4

}

void initTraceCounter(int8_t run)
{ 
  trace.pos=0;      // init

  if(run)
  {
    TIMSK |= 1; //enable Overflow Interrupt
    TCCR0 = 5;  // clk=MCK/1024
  }
  else
  {
    TIMSK &= ~((char)1); //disable Overflow Interrupt
    TCCR0 = 0; // Timer stopped
  }
}


SIGNAL (SIG_OVERFLOW0)
{
  trace.TimeNow++;
}
