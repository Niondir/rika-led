/*
  Schild Firmware
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

int main(void)
{

  _delay_ms(300);      // startup delay
  sei();               // global irq an
  uartSW_init();       // software uart
  init_Display();      // Display initialisieren
  initPIOs();          // Mode Jumper Pins konfigurieren
  detectSignMode();    // Hardware kann von extern als Wagen oder Presischild konfiguriert werden, Preisschiler sogar mit bis zu 7 Adressen
  initTraceCounter(1); // TraceTimer konfigurieren und starten

  if(sign.signType == SIGN_TYPE_TROLLEY);  //initXbee();        // Xbee auf zeiladresse des trace empfängers konfigurieren
 
  show_status();       // Schild Informationen anzeigen, insbesondere SchildID und Typ (AD/Price)
  
   _delay_ms(500);

  while(1)
  {
     while(get_packet()); //auf Paket warten
	 packet_action();

  }
  //todo watchdog nutzen um reset auslösen zu können, wenn via pio mode umgestellt wurde

  /*
  write_Display("Hallo Welt!", 1,1 );
  while(1)
  {
   clr_Screen();
    if(ISSET_PIO1)write_Display("PIO1 set!", 1,2 );
	else          write_Display("PIO1     ", 1,2 );
	if(ISSET_PIO2)write_Display("PIO2 set!", 1,3 );
	else          write_Display("PIO2     ", 1,3 );
	if(ISSET_PIO3)write_Display("PIO3 set!", 1,4 );
	else          write_Display("PIO3     ", 1,4 );
	_delay_ms(500);
  }
  */

  return 0;
}


void detectSignMode(void)
{
  if(!ISSET_PIO1 && !ISSET_PIO2 && !ISSET_PIO3) // kein Jumper gesteckt => Wagen Schild
  {
    sign.signType        = SIGN_TYPE_TROLLEY;
	sign.signUniqueID = 0; // Dummy, wird bei Wagenschild nicht benötigt
  }
  else
  {
    char adr=0;

	sign.signType        = SIGN_TYPE_PRICE;
    //Adresse dekodieren, nur für Präsentation um unterschiedliche Preisschilder realisieren zu können
    
	adr|=ISSET_PIO1;
    adr|=ISSET_PIO2<<1;
	adr|=ISSET_PIO3<<2;

	sign.signUniqueID=(uint16_t)adr;

  }

}

void show_status(void)
{
  char      textvalue[25];

  //Status Infos ausgeben
  sprintf(textvalue, "FirmwareVersion:%d",  FIRMWARE_VERSION);
  write_Display(textvalue,1,1);
  if(sign.signType==SIGN_TYPE_TROLLEY) write_Display("SignType: Trolley", 1, 2);
  else write_Display("SignType: AD ", 1, 2);

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
  int8_t  csumCompareInt;
  

  while(uartSW_getc_wait()!='<');                        //Auf Start eines Pakets warten
 

// CMD Nr empfangen:
  
  //CMD Nr muss folgen
  lastChar = uartSW_getc_wait();                         
  if(lastChar<'0' || lastChar>'9') return 1;               //Keine Ziffer, Fehler
  else 
  {                          
     wBuff[pos++]=lastChar;                              //Ziffer speichern
	 csum+=lastChar;
  }
  
  //Weitere Ziffern oder Separator sind erlaubt, sonst return 1
  do                                                     
  {
	  lastChar = uartSW_getc_wait();  
	  if      ( lastChar == '<' || lastChar == '>') return 1;                            //StartStop Zeichen an unerwarteter Stelle => Fehler

	  if      ( !(lastChar<'0' || lastChar>'9') ) { wBuff[pos++]=lastChar; csum+=lastChar; } //weitere Ziffer abspeichern
      else if ( lastChar == '|')				  { csum+=lastChar;  break;                } //Separator gefunden
	  else                      				    return 1;                                //ungültiges Zeichen
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
	default:                                              return 1;
  }


  for(int i=0; i<ArgAnzahl; i++)
  {
	  //Argumente empfangen
	  pos=0;
	  do                                                                   
	  {
		  lastChar = uartSW_getc_wait();  
		  if      ( lastChar == '<' || lastChar == '>' || pos > 24) return 1;                            //ungültiges Zeichen, oder zuviele empfangen

		  if      ( lastChar != '|' ) {(packet.args)[i][pos++]=lastChar; csum+=lastChar;}     //Zeichen abspeichern
	      else                        { csum+=lastChar;  break;                }             //Separator gefunden
	  }while(1);
  }


// Csum empfangen:
   pos=0;
   lastChar = uartSW_getc_wait();  

   //mindestens 1 Zahl muss empfangen werden
   if      ( !(lastChar<'0' || lastChar>'9') ) { csumCompare[pos++]=lastChar;}           //Zahl gefunden
   else    return 1;                                                                  //Keine Zahl = Fehler

   do  //max 2 weitere Ziffern der checksumme empfangen                                                                 
   {
	  lastChar = uartSW_getc_wait();  
	  if ( lastChar == '>') break;                                               //Paket empfang abgeschlossen                         
      
	  if      ( !(lastChar<'0' || lastChar>'9')  && pos<3 ) { csumCompare[pos++]=lastChar; }
	  else return 1;
	  
   }while(1);

   

// Checksummen vergleichen:
  csumCompare[pos]  = '\0'; 
  csumCompareInt    = (uint8_t)atoi(csumCompare);

  if(csumCompareInt==csum)return 0; //Packet korrekt empfangen
  else                    return 1;

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

							write_Display(packet.args[1],1,1); //Text1 in 1. Zeile schreiben
							write_Display(packet.args[2],1,2); //Text2 in 2. Zeile schreiben
							write_Display(packet.args[3],1,3); //Text3 in 3. Zeile schreiben
							write_Display(packet.args[4],1,4); //Text4 in 4. Zeile schreiben
	                    }
						break;

	case CMDNR_SHOW_ID:        
	                    show_status();
						_delay_ms(2000);

						break;

	case CMDNR_SET_PRICE:   
	                    if((sign.signType==SIGN_TYPE_PRICE) && atoi(packet.args[0]) == sign.signUniqueID) //Check, ob Preis für dieses Schild bestimmt ist
						{
						    write_Display(packet.args[1],1,1); //Name in 1. Zeile schreiben
							write_Display(packet.args[2],1,2); //Preis in 2. Zeile schreiben
						}
	
						break;
  }
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
