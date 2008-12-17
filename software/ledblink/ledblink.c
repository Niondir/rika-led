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
#include <avr/interrupt.h>
#include <inttypes.h>
#include <util/delay.h>
#include <stdlib.h>
#include "uart.h"
#include <stdint.h>
#include <stdio.h>
#include <string.h>

#define UBRR_VAL ((F_CPU+BAUD*8)/(BAUD*16)-1)   // clever runden

int slowcount=0;
unsigned char test[15]="Hello World";
char tempstring[100];
long count=0;
char schildinc[100]="\0";
char schildbuffer[3][100]={{'\0'},{'\0'},{'\0'}};

int  overwriteslot=0;
int  showslot=0;
int  slotsused=0;
int	 compare=0;
int SCHILDIDBYTES=4;


ISR(USART_RXC_vect)
{
   // Code to be executed when the USART receives a byte here
   	if(uart_getc()=='<'){
		cli();
		uart_gets(schildinc, 100);
		for(int k=0; k<slotsused; k++){
			for(int j=2; j<2+SCHILDIDBYTES; j++){
				if (schildinc[j]==schildbuffer[k][j]){
					compare+=1;
				}
			}
			if (compare==SCHILDIDBYTES) {
				overwriteslot=k;
				compare=0;
				break;
			}
			else {
				compare=0;			
			}
		}		


		for(int i=0; i<100; i++) {schildbuffer[overwriteslot][i]=schildinc[i];};
		if((slotsused<3)&&(overwriteslot==slotsused)) slotsused++;
		overwriteslot= (overwriteslot+1)%3;

		sei();
	}
	/*uart_gets(schildinc, 100);
	
	*/
} 


void set_dest(uint32_t destlow, uint32_t desthigh){
//uint32_t destlow;
//uint32_t desthigh;
char dat;
//	destlow=0x4001CF13;
//	desthigh=0x13A200;
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
//set_dest();
init_uart();
uartSW_init(); //software uart
//_delay_ms(1025);
//set_dest(1,0);
//_delay_ms(1025);


while (1)
      {
      // Place your code here
//		set_dest((count%2)+1 , 0);
//		_delay_ms(1000);
//		sprintf(tempstring, "Nachricht mit ein paar mehr zeichen nur mal so zum test und dann war da ja noch das Hello, world! %ld \r\n", count);
//		uart_puts(tempstring);
//		uartSW_putc(uart_getc());
		//PORTD=PIND+(1<<5);
		uartSW_puts(&schildbuffer[showslot][0]);
		if(slotsused!=0) showslot=(showslot+1)%slotsused;
//		uartSW_puts("boing\r\n");
		sprintf(tempstring, "overwriteslot=%d;showslot=%d;slotsused=%d; \r\n", overwriteslot, showslot, slotsused);
		_delay_ms(1000);		
		//uartSW_putc('F');
		uartSW_puts(tempstring);
      };
}
