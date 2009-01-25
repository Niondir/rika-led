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

// Constants
#define FIRMWARE_VERSION    100
#define SIGN_TYPE_TROLLEY   0
#define SIGN_TYPE_PRICE     1

#define CMDNR_SEND_TRACE    1
#define CMDNR_SET_AD        2 
#define CMDNR_SET_PRICE     7
#define CMDNR_SHOW_ID       6

#define ARGCNT_SEND_TRACE   0
#define ARGCNT_SET_AD       5
#define ARGCNT_SET_PRICE    3
#define ARGCNT_SHOW_ID      0

#define MAX_ARGS            5
#define ARG_SIZE            25

// Prototypes
void detectSignMode(void);
void show_status(void);
int8_t get_packet(void);
void packet_action(void);

// Typedefs
typedef struct 
{
   uint16_t signUniqueID; //Nur für Preisschilder, Werbeschilder haben keine Adresse, auch schildID
   uint8_t  signType;
} sign_t;

typedef struct 
{
   uint8_t packetCmdNr; 
   char    args[MAX_ARGS][ARG_SIZE];
} packet_t;

// Gloable Vars
sign_t    sign;
packet_t  packet;
volatile  int16_t trace_cnt=0;

int main(void)
{

  _delay_ms(500);   // startup delay
  sei();            // global irq an
  uartSW_init();    // software uart
  init_Display();
  initPIOs(); 
  detectSignMode(); // Hardware kann von extern als Wagen oder Presischild konfiguriert werden, Preisschiler sogar mit bis zu 7 Adressen

  show_status();
   _delay_ms(500);

   


  while(1)
  {
     while(get_packet()); //auf Paket warten
	 packet_action();

  }

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
 

//----------> CMD Nr empfangen
  
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



//----------> Argumente empfangen
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


//----------> Csum empfangen
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

   

//----------> Checksummen vergleichen
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

	
	
						break;

	case CMDNR_SET_AD: 
	                    if(sign.signType==SIGN_TYPE_TROLLEY)
						{
							//packet.args[0] //lampen id für trace
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


void cfgTraceCounter(int8_t run)
{ 
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

}
