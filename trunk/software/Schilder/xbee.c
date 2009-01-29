#include <avr/io.h>
#include <stdlib.h>
#include <util/delay.h>
#include <stdio.h>

#include "xbee.h"
#include "uart.h"

/*
void set_dest(uint32_t destlow, uint32_t desthigh)
{
	char tempstring[35];
	_delay_ms(XBEE_GUARDTIME);
	sprintf(tempstring, "+++");
	uart_puts(tempstring);
	_delay_ms(XBEE_GUARDTIME);	
	sprintf(tempstring, "ATDH%lx,DL%lx,CN\r",desthigh, destlow);
	uart_puts(tempstring);
	_delay_ms(XBEE_GUARDTIME/2);
}
*/

void set_dest2(char* destlow, char* desthigh)
{
	char tempstring[35];
	_delay_ms(XBEE_GUARDTIME);
	sprintf(tempstring, "+++");
	uart_puts(tempstring);
	_delay_ms(XBEE_GUARDTIME);	
	sprintf(tempstring, "ATDH%s,DL%s,CN\r",desthigh, destlow);
	uart_puts(tempstring);
	_delay_ms(XBEE_GUARDTIME/2);
}

