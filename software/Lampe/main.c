/*! \mainpage RIKA - Lampe
 *
 * Firmware fuer die Lampe des RIKA Projektes "MyStore".
 * Autor: Vincent Goebel
 * Datum: WS 08/09
 * 
 */


/*! \file main.c
    \brief Main Code.
    
    Contains most of the lamp code
*/



/// ***************************************************
/// **************        INCLUDES       **************
/// ***************************************************

#include <avr/io.h>
#include <avr/interrupt.h>
#include <util/delay.h>
#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include "main.h"



/// ***************************************************
/// **********        GLOBAL VARIABLES       **********
/// ***************************************************

char tempstring[120];
char tempchar;
char lamp_id[5];
char send_trace_packet[30];
char csum[7];
char rcv_buf[RCV_BUF_SIZE] = "\0";
int  rcv_buf_iterator = 0;

// flags
int rcv_buf_receiving = FALSE;		// True after "<" received, until ">" received
int rcv_buf_invalid = FALSE;			// rcv buffer contains invalid data
int packet_received = FALSE;			// while true locks rcvbuf until data handled
int price_tag_buf_empty = TRUE;		
int adbuf_empty = TRUE;
int init_mode = TRUE;
int send_trace_mode = FALSE;

// price tag buffer
char price_tag_buf[PRICE_TAG_BUFFER_SLOTS][PRICE_TAG_PACKET_MAX_LENGTH];
int  next_overwrite_price_tag_buffer_pos = 0;
int  next_outgoing_price_tag = 0;
int  price_tag_buffer_slots_used = 0;

// ad buffer
char ad_buffer[AD_BUFFER_SLOTS][AD_PACKET_MAX_LENGTH];
int  next_overwrite_ad_buffer_pos = 0;
int  next_outgoing_ad = 0;
int  ad_buffer_slots_used = 0;

// delays
int ad_delay;
int price_tag_delay;
int send_trace_delay;

/// ***************************************************
/// ******    INTERRUPT SERVICE ROUTINES  *************
/// ***************************************************

 /**
  * \brief  decreases delaycounters
  *
  *         when timer0 overflow occurs the delay timers
	*					are deacreased by 1		
  *
  */
ISR(TIMER0_OVF_vect) {
	if(ad_delay > 0) ad_delay--;
	if(price_tag_delay > 0) price_tag_delay--;
	if(send_trace_delay > 0) send_trace_delay--;
}

 /**
 	*	\fn USART_RXC_vect
  * \brief  mangages incoming chars from uart
  *
  *         This function handles all incoming chars from the
	*					Xbee module. In init mode it just writes them to the receive
	*					buffer (needed to communicate with Xbee), 
	*					in normal mode it checks if incoming character fits to the
	*					general packet structure. If not the buffer will be reset. 
  *
  */
ISR(USART_RXC_vect){
	tempchar=uart_getc();
	if(!init_mode){
		if(!packet_received && !rcv_buf_invalid){
			switch (tempchar) { 
				case '<': 	{
											if (rcv_buf_receiving){
												rcv_buf_invalid = TRUE;
											}
											else {
												rcv_buf_receiving = TRUE;
												rcv_buf_iterator = 0;
											}
											break;
										}
				case '>': 	{
											if(rcv_buf_iterator < RCV_BUF_SIZE){
												if(rcv_buf_receiving){
													packet_received = TRUE;
													rcv_buf_receiving = FALSE;
													rcv_buf[rcv_buf_iterator] = '\0';
													rcv_buf_iterator=0;
												}
											}
											else{
												rcv_buf_invalid = TRUE;
											}
											break;
										}

				default:	{	
										if(rcv_buf_iterator < RCV_BUF_SIZE && !packet_received){
											rcv_buf[rcv_buf_iterator] = tempchar;
											rcv_buf_iterator++;
										}
									}
			}
		}
	}	
	else{
		if((tempchar != 'O') && (tempchar !='K') && (tempchar != '\r') && (rcv_buf_iterator < RCV_BUF_SIZE)){
			rcv_buf[rcv_buf_iterator] = tempchar;
			rcv_buf_iterator++;
			rcv_buf[rcv_buf_iterator] = '\0';
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
	price_tag_buf_empty = TRUE;
	next_overwrite_price_tag_buffer_pos = 0;
	next_outgoing_price_tag = 0;
	price_tag_buffer_slots_used = 0;

	//reset ad buffer and corresponding variables
	adbuf_empty = TRUE;
	next_outgoing_ad = 0;
	next_overwrite_ad_buffer_pos = 0;
	ad_buffer_slots_used = 0;

	insert_default_ad();
	#ifdef DEBUG
	sprintf(tempstring, "BUFFERS CLEARED \r\n");
	uartSW_puts(tempstring);
	#endif
};

 /**
  * \brief  init timer delays
  *
  *         initializes delays for outgoing packets
  *
  */
void init_timer_delays(){
	ad_delay = AD_DELAY;
	price_tag_delay = PRICE_TAG_DELAY;
	send_trace_delay = SEND_TRACE_DELAY;
}

 /**
  * \brief  init lamp
  *		
  *		This function configures the XBee module to send debug messages
	*		to the proper target ID. Afer that it gets the personal MY ID from
	*		the module and saves it in lamp_id[]. 
  *
  */
void init_lamp(){

	//set debug msg destination
	set_dest( DEBUG_MSG_TARGET_ID, "0");
	_delay_ms(XBEE_GUARDTIME);
	sprintf(tempstring,XBEE_CMD_SEQ);
	uart_puts(tempstring);

	//get my id
	_delay_ms(XBEE_GUARDTIME);
	sprintf(tempstring, "ATMY,CN\r");
	uart_puts(tempstring);
	_delay_ms(100);
	copy_argument(0, lamp_id);
	rcv_buf_invalid = TRUE;

	#ifdef DEBUG
	sprintf(tempstring, "INIT SUCCEEDED, LAMP %s RUNNING\r\n",lamp_id);
	uart_puts(tempstring);
	#endif

	init_mode = FALSE;
	insert_default_ad();
};

 /**
  * \brief  inserts default ad
  *		
  *		This Function creates a default ad that will be sent until a real
	*		advertisement is received. That ensures the traces will work evan if a region
	*		got no specific advertisement for a certain time.  
  *
  */
void insert_default_ad(){
	sprintf(tempstring, "2|");
	strcat(tempstring, lamp_id);
	strcat(tempstring, "|");
	strcat(tempstring,"|  Willkommen im| Mustermarkt|              [");
	strcat(tempstring,lamp_id);
	strcat(tempstring,"]|");
	calculate_csum(-1, tempstring);
	strcat(tempstring, csum);
	insert_in_ad_buffer(tempstring);
}

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
	int csum_index=0;
	while(rcv_buf[csum_index] != '\0'){
		csum_index++;
	}
	while(rcv_buf[csum_index] != '|'){
		csum_index--;
	}
	csum_index++;
	return(csum_index);
}

 /**
  * \brief  calculates checksum
  *
  *         This function calculates the checksum by
  *         adding the ascii values of all characters of the
  *					packet in the target buffer exept '<' '>' and the checksum itself and saves
  *					result in csum[]
  *
  * \param	csumindex   index of checksum in receivebufferarray
	* \param 	buffer			buffer containing the packet
  *
  */
void calculate_csum(int csumindex, char* buffer){
	uint8_t tmpcsum=0;
	int i = 0;
	if (csumindex != -1){
		while(i < csumindex){
			tmpcsum += buffer[i];
			i++;
		}
	}
	else{
		while(buffer[i] != '\0'){
			tmpcsum += buffer[i];
			i++;
		}
	}
	sprintf(csum, "%d", tmpcsum);
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
	int csum_index = get_csum_index();
	calculate_csum(csum_index,rcv_buf);

	#ifdef DEBUG
	uartSW_puts("\r\n die richtige csum waere: ");
	uartSW_puts(csum);
	uartSW_puts("\r\n");
	#endif

	return(strcmp(csum, &rcv_buf[csum_index]));
}

 /**
  * \brief  copies argument from rcvbuf to destination
  *
  *         This function copies an argument of the packet in the recieve buffer
	*					to the selected destination character array. 
  *
  * \param	rcv_buf_pos		the starting position of the argument in the recievebuffer
  * \param	dest_array		pointer pointing on the destination array  
	*
  */
void copy_argument(int rcv_buf_pos, char *dest_array){
	int i = rcv_buf_pos;

	while((rcv_buf[i] != '|') && (rcv_buf[i] != '>') && (rcv_buf[i] != '\0')){
		dest_array[i - rcv_buf_pos]=rcv_buf[i];
		i++;
	}
	dest_array[i - rcv_buf_pos]='\0';
}

 /**
  * \brief  changes lamp id
  *
  *         This Function changes the lamp id in the
  *         XBee module as well as in lamp_id[]
  *
  */
void change_lamp_id(){
	copy_argument(FIRST_ARG_INDEX, lamp_id);
	_delay_ms(XBEE_GUARDTIME);
	uart_puts(XBEE_CMD_SEQ);
	_delay_ms(XBEE_GUARDTIME);
	sprintf(tempstring, "ATMY%s,CN\r",lamp_id);
	uart_puts(tempstring);
	_delay_ms(XBEE_GUARDTIME/2);
	clear_buffers();
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
	if(rcv_buf[FIRST_ARG_INDEX] == '0'){
		send_trace_mode = FALSE;
	}	else	{
			strcpy(send_trace_packet, rcv_buf);
			send_trace_mode = TRUE;
	}
}

 /**
  * \brief  calculates next pricetag buffer slot to overwrite
  *
  *     This function calculates the next slot to be used in the pricetag
	*			buffer by comparing the target id of the new price tag packet
	*			with the existing ids in the price tag buffer. If the target id
	*			exists the new packet will replace the existing one. If not the
	*			"next_overwrite_price_tag_buffer_pos" will be used.
  *
  * \return	            the next price tag slot to overwrite
  *
  */
int calculate_overwriteslot(){
	int overwrite = next_overwrite_price_tag_buffer_pos;
	for(int i = 0; i < price_tag_buffer_slots_used; i++){
		for(int j = FIRST_ARG_INDEX; j < (FIRST_ARG_INDEX+ID_MAX_LENGTH + 1); j++){
			if(price_tag_buf[i][j] == rcv_buf[j]){
				if(price_tag_buf[i][j] == '|'){
					overwrite = i;
				}
			}	else{
					break;
			}
		}
	}
	return(overwrite);
}

 /**
  * \brief  forwards packet saved in the receive buffer
  *
  *     This function sends the packet in the receive buffer.
  *
  */
void forward_packet(){
	send_packet(rcv_buf);
}

 /**
  * \brief  inserts ad in ad buffer
  *
  *         This function could cycle through different ads. This will not be uses
	*					because advertisements per region (and so the buffer) got reduced to one.
  *
  * \param	source	pointer to the packet that should be inserted in the buffer
	*
  */
void insert_in_ad_buffer(char *source){
	strcpy(ad_buffer[next_overwrite_ad_buffer_pos], source);
	next_overwrite_ad_buffer_pos=(next_overwrite_ad_buffer_pos + 1)%AD_BUFFER_SLOTS;
	if((ad_buffer_slots_used < AD_BUFFER_SLOTS) && (next_overwrite_ad_buffer_pos >= ad_buffer_slots_used)){
		ad_buffer_slots_used++;
	};
	adbuf_empty = FALSE;
};

 /**
  * \brief  inserts price tag in price tag buffer
  *
  *         This function inserts new price tag packets to the price tag
	*					buffer. If the price tag already exists it will be replaced. 
  *
  * \param	source	pointer to the packet that should be inserted in the buffer  
	*
  */

void insert_in_pricetag_buffer(char *source){
	int overwriteslot = calculate_overwriteslot();
	strcpy(price_tag_buf[overwriteslot], source);
	next_overwrite_price_tag_buffer_pos = (next_overwrite_price_tag_buffer_pos+1) % PRICE_TAG_BUFFER_SLOTS;
	if(overwriteslot == price_tag_buffer_slots_used){
		price_tag_buffer_slots_used++;
	};
	price_tag_buf_empty = FALSE;
};

 /**
  * \brief  sends packet via uart
  *
  *         This function sends the packet after adding opening "<" and closing ">" via LEDs.
	*					If debug is defined a copy will be sent through Xbee module. 
  *
  * \param	packet	the packet to be sent
	*
  */
void send_packet(char *packet){
	uartSW_putc('<');
	uartSW_puts(packet);
	uartSW_putc('>');
	#ifdef DEBUG
	uart_putc('<');
	uart_puts(packet);
	uart_putc('>');
	uart_puts("\r\n");
	#endif

}

 /**
  * \brief  processes packet in buffer
  *
  *         After checking for the correct checksum this function analyses the command 
	*					number of the packet in the receive buffer and executes further steps depending on that number.
	*			
  */
void process_packet(){
	if(!checksum_failed()){
		switch (rcv_buf[COMMANDNR_INDEX]){
		// toggle send trace packets
			case '1':	{	
									set_send_trace();
									break;
								}
		// insert ad packet into buffer
			case '2':	{
									insert_in_ad_buffer(rcv_buf);
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
									change_lamp_id();
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
									insert_in_pricetag_buffer(rcv_buf);
									break;
								}
			default:	{
									rcv_buf_invalid= TRUE;
								}
		}
	} else {
			rcv_buf_invalid= TRUE;	
	}
}

 /**
  * \brief  sends next price tag
  *
  *         This function sends a price tag and determines the next price tag
	*					to be sent. After that the delay timer will be reset.
	*
  */
void send_next_price_tag(){
	send_packet(price_tag_buf[next_outgoing_price_tag]);
	next_outgoing_price_tag = (next_outgoing_price_tag + 1) % price_tag_buffer_slots_used;
	price_tag_delay = PRICE_TAG_DELAY;
}

 /**
  * \brief  sends next ad
  *
  *         This function sends an ad and determines the next price tag
	*					to be sent. After that the delay timer will be reset.
	*
  */
void send_next_ad(){
	send_packet(ad_buffer[next_outgoing_ad]);
	next_outgoing_ad = (next_outgoing_ad + 1) % ad_buffer_slots_used;
	ad_delay = AD_DELAY;
}




 /**
  * \brief  main function
  *
  *         This function first initializes the hardware. After
	*					that it goes to an endless main loop. In that loop it
	*					reacts to different flags, set by the interrupt service 
	*					routines.
	*
  */
int main(void)
{

init_atmega8();
sei();
init_uart();
uartSW_init(); //software uart
init_lamp();


/// ***************************************************
/// ******          MAIN LOOP             *************
/// ***************************************************
	while (1){
		if(packet_received){

			#ifdef DEBUG
			uartSW_puts("Paket erkannt, Processing \r\n");
			sprintf(tempstring, "Inhalt: %s \r\n", rcv_buf);		
			uartSW_puts(tempstring);
			#endif

			process_packet();
			packet_received=FALSE;
		}

		if(rcv_buf_invalid){
			rcv_buf_iterator=0;
			rcv_buf_receiving=FALSE;
			packet_received=FALSE;
			rcv_buf_invalid=FALSE;
		}

		if(!price_tag_buf_empty && !price_tag_delay){
			TGL_LED1;
			send_next_price_tag();
			TGL_LED1;
		}

		if(!adbuf_empty && !ad_delay){
			TGL_LED2;
			send_next_ad();
			TGL_LED2;
		}

		if(send_trace_mode && !send_trace_delay){
		  TGL_LED3;
			send_packet(send_trace_packet);
			send_trace_delay = SEND_TRACE_DELAY;
			TGL_LED3;
		}
	};
};

