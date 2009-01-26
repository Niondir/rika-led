#include <avr/io.h>
#include <avr/interrupt.h>
#include <inttypes.h>
#include <util/delay.h>
#include <stdlib.h>
#include "uart.h"
#include <stdint.h>
#include <stdio.h>
#include <string.h>

#define ADBUFFERMAXSLOTS 2
#define SCHILDBUFFERMAXSLOTS 3
#define SCHILDIDBYTES 4
#define CYCLEDELAY 1000
#define RCVBUFSIZE 120
//#define DEBUG
#define TRUE  1
#define FALSE  0
#define UBRR_VAL ((F_CPU+BAUD*8)/(BAUD*16)-1)   // clever runden

char tempstring[120];

char rcvbuf[RCVBUFSIZE]="\0";
int  rcvbuf_iterator=0;

long count=0;

char lampid[5];
char csum[7];
int	 compare=0;
int	 command=0;

//FLAGS
int rcvbuf_receiving = FALSE;		//
int rcvbuf_invalid = FALSE; 		// rcv buffer contains invalid data
int packet_received = FALSE;		// locks rcvbuf, until data handled
int signbuf_empty = TRUE;
int adbuf_empty = TRUE;
int setupmode = TRUE;
int send_trace_mode = FALSE;



//Variablen fuer Schildbuffer
int  nextSchildOverwriteslot=0;
int  nextSchildShowslot=0;
int  Schildslotsused=0;
char schildbuffer[SCHILDBUFFERMAXSLOTS][60];


//Variablen fuer Adbuffer
int  nextAdOverwriteslot=0;
int  nextAdShowslot=0;
int  Adslotsused=0;
char adbuffer[ADBUFFERMAXSLOTS][120];



 /**
  * \brief  clears buffers
  *
  *         resets adbuffer and signbuffer		
  *
  */
void clear_buffers(){
	signbuf_empty = TRUE;
	nextSchildOverwriteslot=0;
	nextSchildShowslot=0;
	Schildslotsused=0;

	adbuf_empty = TRUE;
	nextAdShowslot=0;
	nextAdOverwriteslot=0;
	Adslotsused=0;
	#ifdef DEBUG
	sprintf(tempstring, "BUFFERS CLEARED \r\n");
	uartSW_puts(tempstring);
	#endif
};


 /**
  * \brief  init lamp
  *
  *         sends command to xbee module to get
  *			current my-id, saves it in lampid
  *
  */
void init_lamp(){
	_delay_ms(20);
	sprintf(tempstring, "+++");
	uart_puts(tempstring);
	_delay_ms(20);
	sprintf(tempstring, "ATMY\r");
	uart_puts(tempstring);
	_delay_ms(500);
	for(int i=0; i<5; i++){
		lampid[i]=rcvbuf[i];
		if(rcvbuf[i]=='\0') break;
	}
	//uartSW_puts(lampid);
	_delay_ms(1000);
	rcvbuf_invalid = TRUE;
	setupmode = FALSE;
};

 /**
  * \brief  gets csum index
  *
  *         This Function searches the location of the
  *			checksum in the recieve buffer array. 
  *
  * \return	            index of checksum in recievebuffer
  *
  */

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

 /**
  * \brief  calculates checksum
  *
  *         This Function calculates the checksum by
  *         adding the ascii values of all signs of the
  *			packet exept '<' '>' and the checksum and saves
  *			result in csum[]
  *
  * \param	csumindex   index of checksum in receivebufferarray
  *
  */
void calculate_csum(int csumindex){
	uint8_t tmpcsum=0;
	int i=0;
	
	while(i<csumindex){
		tmpcsum+=rcvbuf[i];
		i++;
	}
	sprintf(tempstring, "%d", tmpcsum);
	strcpy(csum, tempstring);
}

 /**
  * \brief  checks if checksum is wrong
  *
  *         This Function compares the recieved and the
  *			calculated checksum. It returns 0 if the checksum 
  *			is correct.
  *
  * \return	            Status-Code
  *
  */
int checksum_failed(){
	int csum_index=get_csum_index();
	calculate_csum(csum_index);
#ifdef DEBUG
	uartSW_puts("\r\n die richtige csum waere: ");
	uartSW_puts(csum);
	uartSW_puts("\r\n");
#endif
	return(strcmp(csum, &rcvbuf[csum_index]));
}

 /**
  * \brief  changes lamp id
  *
  *         This Function changes the lamp id in the
  *         XBee module as well as in lampid[]
  *
  */
void change_lampid(){
	int i=2;
	while(rcvbuf[i]!='|'){
		lampid[i-2]=rcvbuf[i];
		i++;
	}
	lampid[i-2]='\0';
	_delay_ms(20);
	sprintf(tempstring, "+++");
	uart_puts(tempstring);
	_delay_ms(20);
	sprintf(tempstring, "ATMY%s,CN\r",lampid);
	uart_puts(tempstring);
	_delay_ms(10);
}

 /**
  * \brief  Exemplarische Funktion
  *
  *         Diese Funktion gibt den übergebenen Parameter
  *         auf der Konsole aus.
  *
  * \param	parameter   Auszugebender Parameter
  * \return	            Status-Code
  *
  */
void set_send_trace(){
	if(rcvbuf[2]=='0'){
		send_trace_mode=FALSE;
	}	else	{
			send_trace_mode=TRUE;
	}
}

//************************************
// 
//************************************
int calculate_overwriteslot(){
int overwrite=nextSchildOverwriteslot;
	for(int i=0; i<Schildslotsused; i++){
		for(int j=2; i<7; j++){
			if(schildbuffer[i][j]==rcvbuf[j]){
				if(schildbuffer[i][j]=='|'){
					overwrite=i;
				}
			}	else{
					break;
			}
		}
	}
	#ifdef DEBUG
	sprintf(tempstring, "ueberschrieben wird slot %d", overwrite);
	#endif
	uartSW_puts(tempstring);
	return(overwrite);
}

//************************************
// 
//************************************
void forward_packet(){
	uartSW_putc('<');
	uartSW_puts(rcvbuf);
	uartSW_putc('>');
}

//************************************
// 
//************************************
void insert_in_ad_buffer(){
	strcpy(adbuffer[nextAdOverwriteslot],rcvbuf);
	nextAdOverwriteslot=(nextAdOverwriteslot+1)%ADBUFFERMAXSLOTS;
	if((Adslotsused<ADBUFFERMAXSLOTS)&&(nextAdOverwriteslot>=Adslotsused)){
		Adslotsused++;
	};
	adbuf_empty = FALSE;
};

//************************************
// 
//************************************
void insert_in_pricetag_buffer(){
	int overwriteslot=calculate_overwriteslot();
	strcpy(schildbuffer[overwriteslot],rcvbuf);
	nextSchildOverwriteslot=(overwriteslot+1)%SCHILDBUFFERMAXSLOTS;
	if(Schildslotsused<SCHILDBUFFERMAXSLOTS && (overwriteslot>=Schildslotsused)){
		Schildslotsused++;
	};
	signbuf_empty = FALSE;
};

//************************************
// 
//************************************
void process_packet(){
	if(!checksum_failed()){
		switch (rcvbuf[0]){
		// send trace packet
			case '1':	{
									set_send_trace();
									break;
								}
		// set ad packet
			case '2':	{
									insert_in_ad_buffer();
									break;
								}
		// clear buffer / reset lamp
			case '3': 			{
									clear_buffers();
									break;
								}
		// toggle ad / pricetag
			case '4':			{
									forward_packet();
									break;
								}
		//	change lamp id for trace
			case '5': 			{	
									change_lampid();
									break;
								}
		//	show id
			case '6':			{
									forward_packet();
									break;
								}
		// set pricetag packet
			case '7':	{
									insert_in_pricetag_buffer();
									break;
								}
			default:	{
									rcvbuf_invalid= TRUE;
								}
		}
	} else {
			//uartSW_puts(csum);
			//uartSW_putc('?');
			rcvbuf_invalid= TRUE;	
	}
}

//************************************
// 
//************************************
void send_next_sign(){
	uartSW_putc('<');
	uartSW_puts(schildbuffer[nextSchildShowslot]);
	uartSW_putc('>');
	nextSchildShowslot=(nextSchildShowslot+1)%Schildslotsused;
	#ifdef DEBUG
	uartSW_puts("\r\n");
	#endif
}

//************************************
// 
//************************************
void send_next_ad(){
	uartSW_putc('<');
	uartSW_puts(adbuffer[nextAdShowslot]);
	uartSW_putc('>');
	nextAdShowslot=(nextAdShowslot+1)%Adslotsused;
	#ifdef DEBUG
	uartSW_puts("\r\n");
	#endif
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
			if(tempchar!='O' && tempchar!='K' && tempchar!='\r'  && rcvbuf_iterator<RCVBUFSIZE){
				rcvbuf[rcvbuf_iterator]=tempchar;
				rcvbuf_iterator++;
				rcvbuf[rcvbuf_iterator]='\0';
			}
		}
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

/*
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
*/

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



//************************************
// 	main loop
//************************************
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

	if(!signbuf_empty){
		send_next_sign();
	}

	if(!adbuf_empty){
		send_next_ad();
	}

	if(rcvbuf_invalid){
		rcvbuf_iterator=0;
		rcvbuf_receiving=FALSE;
		packet_received=FALSE;
		rcvbuf_invalid=FALSE;
	}
	if(send_trace_mode){
		sprintf(tempstring, "<1|173>");
		uartSW_puts(tempstring);	
	}
	_delay_ms(500);

#ifdef DEBUG
	sprintf(tempstring, "  nextSchildOverwriteslot=%d;nextSchildShow=%d;Schildslotsused=%d; \r\n", nextSchildOverwriteslot, nextSchildShowslot, Schildslotsused);		
	//sprintf(tempstring, "  nextadOverwriteslot=%d;nextadShow=%d;adsused=%d; \r\n", nextAdOverwriteslot, nextAdShowslot, Adslotsused);		
	//sprintf(tempstring, "  recd %d; invalid %d; rcv: %d ; lampid %c \r\n", packet_received, rcvbuf_invalid, rcvbuf_receiving, lampid[0]);		
	uartSW_puts(tempstring);
	_delay_ms(500);
#endif
};
};

