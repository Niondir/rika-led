/*****************************************************
This program was produced by the
CodeWizardAVR V2.03.8a Evaluation
Automatic Program Generator
© Copyright 1998-2008 Pavel Haiduc, HP InfoTech s.r.l.
http://www.hpinfotech.com

Project : 
Version : 
Date    : 30.11.2008
Author  : Freeware, for evaluation and non-commercial use only
Company : 
Comments: 


Chip type           : ATmega8
Program type        : Application
Clock frequency     : 1,843200 MHz
Memory model        : Small
External RAM size   : 0
Data Stack size     : 256
*****************************************************/


#include <avr/io.h>
#include <avr/eeprom.h>
#include <avr/interrupt.h>
#include <inttypes.h>
#include <util/delay.h>
#include <stdlib.h>
#include "uart.h"
#include <stdint.h>
#include <stdio.h>
#include <string.h>


#ifndef EEMEM
// alle Textstellen EEMEM im Quellcode durch __attribute__ ... ersetzen
#define EEMEM  __attribute__ ((section (".eeprom")))
#endif

#define CYCLEDELAY 1000
#define RCVBUFSIZE 120
#define DEBUG
#define TRUE  1
#define FALSE  0
#define UBRR_VAL ((F_CPU+BAUD*8)/(BAUD*16)-1)   // clever runden

char tempstring[120];

char rcvbuf[RCVBUFSIZE]="\0";
int  rcvbuf_iterator=0;

char lampid[5];
char csum[7];

//FLAGS
int rcvbuf_receiving = FALSE;		//
int rcvbuf_invalid = FALSE; 		// rcv buffer contains invalid data
int packet_received = FALSE;		// locks rcvbuf, until data handled
int buffer_empty = TRUE;
int setupmode = TRUE;
int signmode = FALSE;
int admode = TRUE;
int send_trace = FALSE;
int output_updated = TRUE;

//default outputs
char defaultpricetag[120]="2|3|Aepfel|3,99|1324\0";
char defaultad[120]="7|0|zeile1|zeile2|zeile3|zeile4|123\0";

//Variablen fuer Output
char outbuffer[120];


void set_dest(uint32_t destlow, uint32_t desthigh){
char dat;
	_delay_ms(20);
	sprintf(tempstring, "+++");
	uart_puts(tempstring);
	_delay_ms(20);
	while((dat=uart_getc())!='\r') uartSW_putc(dat);	
	sprintf(tempstring, "ATDH%lx,DL%lx,CN\r",desthigh,destlow); //, desthigh);
	uart_puts(tempstring);
	_delay_ms(10);
}

///////////////////////////////////////////
/// gets lampid
///////////////////////////////////////////
void init_lamp(){
	_delay_ms(20);
	sprintf(tempstring, "+++");
	uart_puts(tempstring);
	_delay_ms(20);
	sprintf(tempstring, "ATMY\r");
	uart_puts(tempstring);
	_delay_ms(1000);
	rcvbuf[rcvbuf_iterator]='\0';
	for(int i=0; i<5; i++){
		lampid[i]=rcvbuf[i];
		if(rcvbuf[i]=='\0') break;
	}
	uartSW_puts(lampid);
	_delay_ms(1000);
	rcvbuf_invalid = TRUE;
	setupmode = FALSE;
};

void addsigntobuffer(){	
};



int get_csum_index(){
	int csumindex=0;
	while(rcvbuf[csumindex]!='\0'){
		csumindex++;
	}
	while(rcvbuf[csumindex]!='|'){
		csumindex--;
	}
	csumindex++;
	return(csumindex);
}

////////////////////////////////
//// calculates csum
////////////////////////////////
void calculate_csum(int csumindex){
	int tmpcsum=0;
	int i=0;
	
	while(i<csumindex){
		tmpcsum+=rcvbuf[i];
		i++;
	}
	sprintf(tempstring, "%d", tmpcsum);
	strcpy(csum, tempstring);
}

int checksum_failed(){
	int csum_index=get_csum_index();
	calculate_csum(csum_index);
	uartSW_puts(csum);
	uartSW_putc(';');
	uartSW_puts(&rcvbuf[csum_index]);
	return(strcmp(csum, &rcvbuf[csum_index]));
}


void change_lampid(){
	int i=0;
	while(rcvbuf[i]!='\0'){
		lampid[i]=rcvbuf[i];
		i++;
	}
}


void copy_ad_to_buffer(){}
void copy_pricetag_to_buffer(){}
void process_send_trace(){}


void process_packet(){
	if(!checksum_failed()){
		switch (rcvbuf[0]){
		// send trace packet
			case '1':	{
									send_trace = TRUE;
								}
		// set ad packet
			case '2':	{
									if (admode && strcmp(rcvbuf, outbuffer)){
										copy_ad_to_buffer();
									}
									break;
								}
		//	change sign id
			case '6':	{
									break;
								}
		// set pricetag packet
			case '7':	{
									if (pricetagmode && strcmp(rcvbuf, outbuffer)){
										copy_pricetag_to_buffer();
									}
									break;
								}
			default:	{
									rcvbuf_invalid= TRUE;
								}
		}
	} else {
			uartSW_puts(csum);
			uartSW_putc('?');
			rcvbuf_invalid= TRUE;	
	}
}

/// ****************************************
/// ******    ISR RX           *************
/// ****************************************

ISR(USART_RXC_vect)
{
// Code to be executed when the USART receives a byte here
	char tempchar=uart_getc();

	if(!setupmode){
		switch (tempchar) {
			case '<': 	{
				if (rcvbuf_receiving){
					rcvbuf_invalid = TRUE;
				}
				else {
					rcvbuf_receiving = TRUE;
					rcvbuf_iterator = 0;
				}
				break;
			}
			case '>': 	{
				if(rcvbuf_iterator<RCVBUFSIZE){
					if(rcvbuf_receiving){
						packet_received = TRUE;
						rcvbuf_receiving = FALSE;
						rcvbuf[rcvbuf_iterator]='\0';
						rcvbuf_iterator=0;
					}
				}
				else{
					rcvbuf_invalid = TRUE;
				}
				break;
			}

			default:	{	
				if(rcvbuf_iterator<RCVBUFSIZE && !packet_received){
					rcvbuf[rcvbuf_iterator]=tempchar;
					rcvbuf_iterator++;
				}
				else{
					//rcvbuf_invalid = TRUE;
				}
			}
		}
	}	else{
			if(tempchar!='O' && tempchar!='K' && tempchar!='\r'){
				rcvbuf[rcvbuf_iterator]=tempchar;
				rcvbuf_iterator++;
			}
		}
}


// Timer 0 overflow interrupt service routine
ISR(TIMER0_OVF_vect) 
{
// Place your code here
}



void main(void)
{

// Input/Output Ports initialization
// Port B initialization
// Func7=In Func6=In Func5=In Func4=In Func3=In Func2=Out Func1=In Func0=In 
// State7=T State6=T State5=T State4=T State3=T State2=T State1=T State0=T 
PORTB=0x00;
DDRB=0x04;

// Port C initialization
// Func6=In Func5=In Func4=In Func3=In Func2=In Func1=In Func0=In 
// State6=T State5=T State4=T State3=T State2=T State1=T State0=T 
PORTC=0x00;
DDRC=0x00;

// Port D initialization
// Func7=Out Func6=Out Func5=Out Func4=In Func3=In Func2=In Func1=Out Func0=In 
// State7=0 State6=0 State5=0 State4=T State3=T State2=T State1=0 State0=T 
PORTD=0x00;
DDRD=0xE2;

// Timer/Counter 0 initialization
// Clock source: System Clock
// Clock value: 1,800 kHz
TCCR0=0x05;
TCNT0=0x00;

// Timer/Counter 1 initialization
// Clock source: System Clock
// Clock value: Timer 1 Stopped
// Mode: Normal top=FFFFh
// OC1A output: Discon.
// OC1B output: Discon.
// Noise Canceler: Off
// Input Capture on Falling Edge
// Timer 1 Overflow Interrupt: Off
// Input Capture Interrupt: Off
// Compare A Match Interrupt: Off
// Compare B Match Interrupt: Off
TCCR1A=0x00;
TCCR1B=0x00;
TCNT1H=0x00;
TCNT1L=0x00;
ICR1H=0x00;
ICR1L=0x00;
OCR1AH=0x00;
OCR1AL=0x00;
OCR1BH=0x00;
OCR1BL=0x00;

// Timer/Counter 2 initialization
// Clock source: System Clock
// Clock value: Timer 2 Stopped
// Mode: Normal top=FFh
// OC2 output: Disconnected
ASSR=0x00;
TCCR2=0x00;
TCNT2=0x00;
OCR2=0x00;

// External Interrupt(s) initialization
// INT0: Off
// INT1: Off
MCUCR=0x00;

// Timer(s)/Counter(s) Interrupt(s) initialization
TIMSK=0x01;

// Analog Comparator initialization
// Analog Comparator: Off
// Analog Comparator Input Capture by Timer/Counter 1: Off
ACSR=0x80;
SFIOR=0x00;


// USART initialization
// Communication Parameters: 8 Data, 1 Stop, No Parity
// USART Receiver: On
// USART Transmitter: On
// USART Mode: Asynchronous
// USART Baud Rate: 9600
UCSRA=0x00;
UCSRB=0x18;
UCSRC=0x86;
UCSRB |= (1 << RXCIE);
UBRRH=0x00;
UBRRL=0x0B; 



// Global enable interrupts
//#asm("sei")
sei();
init_uart();
uartSW_init(); //software uart
init_lamp();


while (1){
	if(packet_received){

		#ifdef DEBUG
		uartSW_puts("Paket erkannt, Processing \r\n");
		sprintf(tempstring, "Inhalt: %s \r\n", rcvbuf);		
		uartSW_puts(tempstring);
		#endif

		process_packet();
		packet_received=FALSE;
	}

	if(output_updated){
		uartSW_puts(outbuffer);
	}

	if(rcvbuf_invalid){
		uartSW_putc('!');
		rcvbuf_iterator=0;
		rcvbuf_receiving=FALSE;
		packet_received=FALSE;
		rcvbuf_invalid=FALSE;
	}

	if(send_trace){
		process_send_trace();
	}

#ifdef DEBUG
	_delay_ms(1000);
#endif

};
};

