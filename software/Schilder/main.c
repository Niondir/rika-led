/*
 - RIKA EMPFÄNGER -
 
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


#define SET_LED1   (PORTD |= 1<<PD5)
#define CLR_LED1   (PORTD &= ~(1<<PD5))
#define TGL_LED1   ((PORTD & (1<<PD5))?CLR_LED1:SET_LED1);


typedef struct {
    uint8_t  ID[10];
    uint8_t  Text1[40];
	uint8_t  Text2[40];
    uint8_t  csum;
} Anzeige_t;


int main(void)
{
  uint8_t i;
  Anzeige_t anzg;

  sei(); //global irq an

  uartSW_init(); // software uart

  uartSW_putc(12);//clear display
  
  DDRD|= 1<<PD5; //LED1
  CLR_LED1;;

  //Cursor nicht anzeigen cmd:
  uartSW_putc(27);
  uartSW_putc('C');
  uartSW_putc(0);

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

	uartSW_puts("Schild-ID: ");
	uartSW_puts(anzg.ID);
	uartSW_puts("\r\n");
	uartSW_puts("Text1: ");
	uartSW_puts(anzg.Text1);
	uartSW_puts("\r\n");
	uartSW_puts("Text2: ");
	uartSW_puts(anzg.Text2);
	uartSW_puts("\r\n");
  

  }
  return 0;
}












