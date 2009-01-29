#include "xbee.h"
#include <avr/io.h>
#include <stdlib.h>
#include <util/delay.h>
#include <stdio.h>

#include "xbee.h"
#include "uart.h"

void set_dest(char* destlow, char* desthigh)
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
