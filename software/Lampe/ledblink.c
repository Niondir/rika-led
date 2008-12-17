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

#define ADBUFFERMAXSLOTS 0
#define SCHILDBUFFERMAXSLOTS 3
#define SCHILDIDBYTES 4
//#define DEBUG

#include <avr/io.h>
#include <avr/interrupt.h>
#include <inttypes.h>
#include <util/delay.h>
#include <stdlib.h>
#include "uart.h"
#include <stdint.h>
#include <stdio.h>
#include <string.h>

#define UBRR_VAL ((F_CPU+BAUD*8)/(BAUD*16)-1)   // clever runden

char tempstring[100];
long count=0;

int	 compare=0;
int	 command=0;


//Variablen fuer Schildbuffer
char schildinc[100]="\0";
int  nextSchildOverwriteslot=0;
int  nextSchildShowslot=0;
int  Schildslotsused=0;
char schildbuffer[SCHILDBUFFERMAXSLOTS][100];


//Variablen fuer Adbuffer
char adinc[100]="\0";
int  nextAdOverwriteslot=0;
int  nextAdShowslot=0;
int  Adslotsused=0;
char adbuffer[ADBUFFERMAXSLOTS][100];


ISR(USART_RXC_vect)
{
   // Code to be executed when the USART receives a byte here
    cli();
	PORTD=PIND+(1<<5);
   	if(uart_getc()=='<'){
		PORTD=PIND+(1<<5);
		command=uart_getc();
		uart_getc();
		switch (command) {
		
		// 1: Schild
			case '1': {
					PORTD=PIND+(1<<5);		
					uart_gets(schildinc, 100);
					for(int k=0; k<Schildslotsused; k++){
						for(int j=0; j<SCHILDIDBYTES; j++){
							if (schildinc[j]==schildbuffer[k][j]){
								compare+=1;
							}
						}
						if (compare==SCHILDIDBYTES) {
							nextSchildOverwriteslot=k;
							compare=0;
							break;
						}
						else {
							compare=0;			
						}
					}		


					for(int i=0; i<100; i++) {schildbuffer[nextSchildOverwriteslot][i]=schildinc[i];};
					if((Schildslotsused<3)&&(nextSchildOverwriteslot==Schildslotsused)) {
						Schildslotsused++;
					}
					nextSchildOverwriteslot= (nextSchildOverwriteslot+1)%SCHILDBUFFERMAXSLOTS;
					break;
					}


		// 2: Ad
			case '2':	break;
		// 3: SendTrace
			case '3':	break;
		// 4: ClearBuffers
			case '4':	break;
		}
		sei();
	}
} 


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



// Timer 0 overflow interrupt service routine
ISR(TIMER0_OVF_vect) 
{
// Place your code here
/*	if (slowcount==7) {
		PORTD=PIND+(1<<5);
		slowcount=0;
	}	else {
			slowcount++;
	}
*/
}

// Declare your global variables here

void main(void)
{
// Declare your local variables here

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



while (1)
      {	
	  	if(Schildslotsused) uartSW_putc('<');
		uartSW_puts(&schildbuffer[nextSchildShowslot][0]);
		if(Schildslotsused) uartSW_putc('>');
		if(Schildslotsused!=0) nextSchildShowslot=(nextSchildShowslot+1)%Schildslotsused;


// DEBUG
		sprintf(tempstring, "  nextSchildOverwriteslot=%d;nextSchildShowslot=%d;Schildslotsused=%d; \r\n", nextSchildOverwriteslot, nextSchildShowslot, Schildslotsused);
		_delay_ms(1000);		
		uartSW_puts(tempstring);
//#endif
      };
}
