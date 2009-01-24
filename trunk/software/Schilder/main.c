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

#include "main.h"
#include "uart.h"
#include "xbee.h"
#include "board.h"
#include "display.h"


#define FIRMWARE_VERSION 100
#define SIGN_TYPE_TROLLEY 0
#define SIGN_TYPE_PRICE   1

void detectSignMode(void);
void show_status(void);

typedef struct 
{
   uint16_t signUniqueAdress; //Nur für Preisschilder, Werbeschilder haben keine Adresse
   uint8_t  signType;
} sign_t;

sign_t sign;
char textvalue[25]; // für sprintf im prog



int main(void)
{
  uint8_t i;
 _delay_ms(1000);
  sei(); //global irq an
  uartSW_init(); // software uart
  init_Display();
  initPIOs(); 
  detectSignMode(); //Hardware kann von extern als Wagen oder Presischild konfiguriert werden, Preisschiler sogar mit bis zu 7 IDs

  show_status();
   _delay_ms(500);

   


  while(1);

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
/*
  while(1)
  {
     
	//<1234|preisodername|preisodername>
    
    while(uartSW_getc_wait()!='<');
    
    //ID

	i=0;
   while(1)
   {
	anzg.ID[i]=uartSW_getc_wait();
    if(anzg.ID[i]=='|')
	{
	  anzg.ID[i]='\0';
	  break;
	}
	else i++;
   }

    //TEXT1
	i=0;
   while(1)
   {
	anzg.Text1[i]=uartSW_getc_wait();
    if(anzg.Text1[i]=='|')
	{
	  anzg.Text1[i]='\0';
	  break;
	}
	else i++;
   }

   //TEXT2
   	i=0;
   while(1)
   {
	anzg.Text2[i]=uartSW_getc_wait();
    if(anzg.Text2[i]=='>')
	{
	  anzg.Text2[i]='\0';
	  break;
	}
	else i++;
   }


	//uartSW_putc(12); //clear display
  if(anzg.ID[0]=='0' && anzg.ID[1]=='0' && anzg.ID[2]=='0' && anzg.ID[3] =='1'){
	uartSW_puts("\r\n");
	uartSW_puts("ID: ");
	uartSW_puts(anzg.ID);
	uartSW_puts("\r\n");
	uartSW_puts("\r\n");
	uartSW_puts(anzg.Text1);
	uartSW_puts("\r\n");
	uartSW_puts(anzg.Text2);
	uartSW_puts(" EUR");
  }

//	uartSW_puts("\r\n");
  

  }
*/
  return 0;
}


void detectSignMode(void)
{
  if(!ISSET_PIO1 && !ISSET_PIO2 && !ISSET_PIO3) // kein Jumper gesteckt => Wagen Schild
  {
    sign.signType        = SIGN_TYPE_TROLLEY;
	sign.signUniqueAdress = 0; // Dummy, wird bei Wagenschild nicht benötigt
  }
  else
  {
    char adr=0;

	sign.signType        = SIGN_TYPE_PRICE;
    //Adresse dekodieren, nur für Präsentation um unterschiedliche Preisschilder realisieren zu können
    
	adr|=ISSET_PIO1;
    adr|=ISSET_PIO2<<1;
	adr|=ISSET_PIO3<<2;

	sign.signUniqueAdress=(uint16_t)adr;

  }

}

void show_status(void)
{
  //Status Infos ausgeben
  sprintf(textvalue, "FirmwareVersion:%d",  FIRMWARE_VERSION);
  write_Display(textvalue,1,1);
  if(sign.signType==SIGN_TYPE_TROLLEY) write_Display("SignType: Trolley", 1, 2);
  else write_Display("SignType: AD ", 1, 2);

  sprintf(textvalue, "UniqueAdress: %d",  sign.signUniqueAdress);
  write_Display(textvalue,1,3); 
 
}











