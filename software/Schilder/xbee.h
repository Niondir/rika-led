#ifndef XBEEH
#define XBEEH

#include "uart.h"

//connected to PB2
#define XBEE_SLEEP PB2
#define DDR_XBEE_SLEEP DDRB
#define PORT_XBEE_SLEEP PORTB

#define XBEE_ENTER_SLEEP    PORT_XBEE_SLEEP |= 1<< XBEE_SLEEP
#define XBEE_LEAVE_SLEEP    PORT_XBEE_SLEEP &= ~(1<< XBEE_SLEEP)

#define XBEE_GUARDTIME 20
#define XBEE_SEND_STRING uart_puts


void set_dest(uint32_t destlow, uint32_t desthigh);

#endif
