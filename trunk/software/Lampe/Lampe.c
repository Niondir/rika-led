#include <avr/io.h>
#include <avr/interrupt.h>
#include <inttypes.h>
#include <util/delay.h>
#include <stdlib.h>
#include "uart.h"
#include <stdint.h>
#include <stdio.h>
#include <string.h>
#include "Lampe.h"


//#define DEBUG

#define ADBUFFERMAXSLOTS 1

#ifdef DEBUG
#define PRICETAGBUFFERMAXSLOTS 6
#else 
#define PRICETAGBUFFERMAXSLOTS 8
#endif


#define PRICETAGPACKETMAXLENGTH 60
#define ADPACKETMAXLENGTH 120
#define RCVBUFSIZE 120
#define TRUE  1
#define FALSE  0
#define AD_DELAY 100
#define PRICETAG_DELAY 50
#define SEND_TRACE_DELAY 200
#define COMMANDNR_INDEX 0
#define FIRST_ARG_INDEX 2

#define UBRR_VAL ((F_CPU+BAUD*8)/(BAUD*16)-1)   // clever runden


//global variables
char tempstring[120];
char lampid[5];
char sendtracepacket[30];
char csum[7];
char rcvbuf[RCVBUFSIZE]="\0";
int  rcvbuf_iterator=0;

//flags
int rcvbuf_receiving = FALSE;		// True after "<" received, until ">" received
int rcvbuf_invalid = FALSE; 		// rcv buffer contains invalid data
int packet_received = FALSE;		// locks rcvbuf until data handled
int pricetagbuf_empty = TRUE;
int adbuf_empty = TRUE;
int init_mode = TRUE;
int send_trace_mode = FALSE;

//PriceTagBuffer
char PriceTagBuffer[PRICETAGBUFFERMAXSLOTS][PRICETAGPACKETMAXLENGTH];
int  nextOverwritePriceTagBufferPos=0;
int  nextOutgoingPriceTag=0;
int  PriceTagBufferSlotsUsed=0;

//AdBuffer
char AdBuffer[ADBUFFERMAXSLOTS][ADPACKETMAXLENGTH];
int  nextOverwriteAdBufferPos=0;
int  nextOutgoingAd=0;
int  AdBufferSlotsUsed=0;

// init delays for outgoing packets
int addelay = AD_DELAY;
int pricetagdelay = PRICETAG_DELAY;
int sendtracedelay = SEND_TRACE_DELAY;


/// ***************************************************
/// ******    INTERRUPT SERVICE ROUTINES  *************
/// ***************************************************

ISR(TIMER0_OVF_vect) 
{
	if(addelay>0) addelay--;
	if(pricetagdelay>0) pricetagdelay--;
	if(sendtracedelay>0) sendtracedelay--;
}

ISR(USART_RXC_vect)
{
	char tempchar=uart_getc();
	if(!init_mode){
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
	}	
	else{
		if(tempchar!='O' && tempchar!='K' && tempchar!='\r'  && rcvbuf_iterator<RCVBUFSIZE){
			rcvbuf[rcvbuf_iterator]=tempchar;
			rcvbuf_iterator++;
			rcvbuf[rcvbuf_iterator]='\0';
		}
	}
}



/// ***************************************************
/// ******            FUNCTIONS                  ******
/// ***************************************************

 /**
  * \brief  clears buffers
  *
  *         resets ad buffer and price tag buffer		
  *
  */
void clear_buffers(){
	//reset pricetag buffer and corresponding variables
	pricetagbuf_empty = TRUE;
	nextOverwritePriceTagBufferPos=0;
	nextOutgoingPriceTag=0;
	PriceTagBufferSlotsUsed=0;

	//reset ad buffer and corresponding variables
	adbuf_empty = TRUE;
	nextOutgoingAd=0;
	nextOverwriteAdBufferPos=0;
	AdBufferSlotsUsed=0;

	#ifdef DEBUG
	sprintf(tempstring, "BUFFERS CLEARED \r\n");
	uartSW_puts(tempstring);
	#endif
};


 /**
  * \brief  init lamp
  *
  *		This function sends command to xbee module to get
  *			current my-id and saves it in lampid
  *
  */
void init_lamp(){
	_delay_ms(GUARDTIME);
	sprintf(tempstring, "+++");
	uart_puts(tempstring);
	_delay_ms(GUARDTIME);
	sprintf(tempstring, "ATMY\r");
	uart_puts(tempstring);
	_delay_ms(500);
	copy_argument(0, lampid);
	rcvbuf_invalid = TRUE;
	init_mode = FALSE;
};

 /**
  * \brief  gets csum index
  *
  *			This function searches the location of the
  *			checksum in the recieve buffer array. 
  *
  * \return	            index of checksum in receivebuffer
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
  *         This function calculates the checksum by
  *         adding the ascii values of all characters of the
  *					packet exept '<' '>' and the checksum itself and saves
  *					result in csum[]
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
  *     This Function compares the recieved and the
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
  * \brief  copies argument from rcvbuf to destination
  *
  *         This function copies an argument of the packet in the recieve buffer
	*					to the selected destination character array. 
  *
  * \param	rcvBufPos		the starting position of the argument in the recievebuffer
  * \param	destArray		pointer pointing on the destination array  
	*
  */
void copy_argument(int rcvBufPos, char *destArray){
	int i = rcvBufPos;

	while(rcvbuf[i]!='|' && rcvbuf[i]!='>' && rcvbuf[i]!='\0'){
		destArray[i-rcvBufPos]=rcvbuf[i];
		i++;
	}
	destArray[i-rcvBufPos]='\0';
}

 /**
  * \brief  changes lamp id
  *
  *         This Function changes the lamp id in the
  *         XBee module as well as in lampid[]
  *
  */
void change_lampid(){
	copy_argument(FIRST_ARG_INDEX, lampid);
	_delay_ms(GUARDTIME);
	uart_puts("+++");
	_delay_ms(GUARDTIME);
	sprintf(tempstring, "ATMY%s,CN\r",lampid);
	uart_puts(tempstring);
	_delay_ms(GUARDTIME/2);
}

 /**
  * \brief  toggles if lamp sends send_trace packets
  *
  *         This function toggles between sending or not sending
	*					send trace packets. If the first argument is 0 it stops sending
	*					these packets, if 1 the packet with the target id will be copied
	*					and sending will be activated
  *
  */
void set_send_trace(){
	if(rcvbuf[FIRST_ARG_INDEX]=='0'){
		send_trace_mode=FALSE;
	}	else	{
			strcpy(sendtracepacket, rcvbuf);
			send_trace_mode=TRUE;
	}
}

//************************************
// 
//************************************
int calculate_overwriteslot(){
int overwrite=nextOverwritePriceTagBufferPos;
	for(int i=0; i<PriceTagBufferSlotsUsed; i++){
		for(int j=2; i<7; j++){
			if(PriceTagBuffer[i][j]==rcvbuf[j]){
				if(PriceTagBuffer[i][j]=='|'){
					overwrite=i;
				}
			}	else{
					break;
			}
		}
	}
	#ifdef DEBUG
	sprintf(tempstring, "ueberschrieben wird slot %d", overwrite);
	uartSW_puts(tempstring);
	#endif
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
	strcpy(AdBuffer[nextOverwriteAdBufferPos],rcvbuf);
	nextOverwriteAdBufferPos=(nextOverwriteAdBufferPos+1)%ADBUFFERMAXSLOTS;
	if((AdBufferSlotsUsed<ADBUFFERMAXSLOTS)&&(nextOverwriteAdBufferPos>=AdBufferSlotsUsed)){
		AdBufferSlotsUsed++;
	};
	adbuf_empty = FALSE;
};

//************************************
// 
//************************************
void insert_in_pricetag_buffer(){
	int overwriteslot=calculate_overwriteslot();
	strcpy(PriceTagBuffer[overwriteslot],rcvbuf);
	nextOverwritePriceTagBufferPos=(overwriteslot+1)%PRICETAGBUFFERMAXSLOTS;
	if(PriceTagBufferSlotsUsed<PRICETAGBUFFERMAXSLOTS && (overwriteslot>=PriceTagBufferSlotsUsed)){
		PriceTagBufferSlotsUsed++;
	};
	pricetagbuf_empty = FALSE;
};

//************************************
// 
//************************************
void process_packet(){
	if(!checksum_failed()){
		switch (rcvbuf[COMMANDNR_INDEX]){
		// toggle send trace packets
			case '1':	{	
									set_send_trace();
									break;
								}
		// insert ad packet into buffer
			case '2':	{
									insert_in_ad_buffer();
									break;
								}
		// clear lamp buffers
			case '3': {
									clear_buffers();
									break;
								}
		// toggle ad / pricetag
			case '4':	{
									forward_packet();
									break;
								}
		//	change lamp id
			case '5': {	
									change_lampid();
									break;
								}
		//	send show_id packet
			case '6':	{
									forward_packet();
									break;
								}
		// set pricetag packet
			case '7':	{			
									forward_packet();
									insert_in_pricetag_buffer();
									break;
								}
			default:	{
									rcvbuf_invalid= TRUE;
								}
		}
	} else {
			rcvbuf_invalid= TRUE;	
	}
}

//************************************
// 
//************************************
void send_next_sign(){
	uartSW_putc('<');
	uartSW_puts(PriceTagBuffer[nextOutgoingPriceTag]);
	uartSW_putc('>');
	nextOutgoingPriceTag=(nextOutgoingPriceTag+1)%PriceTagBufferSlotsUsed;
	#ifdef DEBUG
	uartSW_puts("\r\n");
	#endif
}

//************************************
// 
//************************************
void send_next_ad(){
	uartSW_putc('<');
	uartSW_puts(AdBuffer[nextOutgoingAd]);
	uartSW_putc('>');
	nextOutgoingAd=(nextOutgoingAd+1)%AdBufferSlotsUsed;
	#ifdef DEBUG
	uartSW_puts("\r\n");
	#endif
}





int main(void)
{

init_AtMega8();
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

	if(!pricetagbuf_empty && !pricetagdelay){
		send_next_sign();
		pricetagdelay = PRICETAG_DELAY;
	}

	if(!adbuf_empty && !addelay){
		send_next_ad();
		addelay = AD_DELAY;
	}

	if(rcvbuf_invalid){
		rcvbuf_iterator=0;
		rcvbuf_receiving=FALSE;
		packet_received=FALSE;
		rcvbuf_invalid=FALSE;
	}
	if(send_trace_mode && !sendtracedelay){
		uartSW_putc('<');
		uartSW_puts(sendtracepacket);
		uartSW_putc('>');	
		sendtracedelay = SEND_TRACE_DELAY;
	}
};
};

