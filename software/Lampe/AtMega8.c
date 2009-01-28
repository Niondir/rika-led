#include "AtMega8.h"

void init_AtMega8(){
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
	// Clock value: 28,800 kHz
	TCCR0=0x03;
	TCNT0=0x00;

	// External Interrupt(s) initialization
	// INT0: Off
	// INT1: Off
	MCUCR=0x00;

	// Timer(s)/Counter(s) Interrupt(s) initialization
	TIMSK|=0x01;

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
}
