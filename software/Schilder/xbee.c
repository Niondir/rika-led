#include <avr/io.h>
#include <stdlib.h>
#include <util/delay.h>
#include <stdio.h>

#include "xbee.h"
#include "uart.h"

void sendXBeeCMD(char* cmd);

void init_xbee(void)
{
  //todo config tasks....
   
  //XBee activate
	XBEE_LEAVE_SLEEP;   //Sleep_RQ = Low = No Sleep
    DDR_XBEE_SLEEP  |= 1<< XBEE_SLEEP;      //Sleep_RQ = Out
    uartSW_puts("\r\n- Xbee: Entering CMD Mode...");

  //enter cmd mode:
   uart_puts("+++");
   _delay_ms(1000);

   sendXBeeCMD("ATSM02\r"); //Enable Sleep Modus
   
   sendXBeeCMD("ATCN\r"); //Leave CMD Mode

}


void sendXBeeCMD(char* cmd)
{
   
  char tempBuff[100];
  char dat;

  sprintf(tempBuff, "\r\n- Xbee: Sending CMD: \"%s\" Answer: ", cmd);
  uartSW_puts(tempBuff);

  uart_puts(cmd); //send to XBEE

  while((dat=uart_getc())!='\r') uartSW_putc(dat);  
}






