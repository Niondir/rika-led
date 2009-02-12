#include "xbee.h"
#include "atmega8.h"
#include "board.h"
#include "uart.h"
/// ***************************************************
/// **********            DEFINES            **********
/// ***************************************************

//#define DEBUG

#define DEBUG_MSG_TARGET_ID ("f")

#define AD_BUFFER_SLOTS 1

#ifdef DEBUG
#define PRICE_TAG_BUFFER_SLOTS 4
#else 
#define PRICE_TAG_BUFFER_SLOTS 5
#endif

#define ID_MAX_LENGTH 4
#define PRICE_TAG_PACKET_MAX_LENGTH 60
#define AD_PACKET_MAX_LENGTH 120
#define RCV_BUF_SIZE 120
#define TRUE  1
#define FALSE  0

#define AD_DELAY 100
#define PRICE_TAG_DELAY 50
#define SEND_TRACE_DELAY 200
#define COMMANDNR_INDEX 0
#define FIRST_ARG_INDEX 2

/// ***************************************************
/// **********           PROTOTYPES          **********
/// ***************************************************

void 	clear_buffers();
void 	init_timer_delays();
void 	init_lamp();
void 	insert_default_ad();
int 	get_csum_index();
void 	calculate_csum(int csumindex, char* buffer);
int 	checksum_failed();
void 	copy_argument(int rcv_buf_pos, char *dest_array);
void 	change_lamp_id();
void 	set_send_trace();
int 	calculate_overwriteslot();
void 	forward_packet();
void 	insert_in_ad_buffer(char *source);
void 	insert_in_pricetag_buffer(char *source);
void 	send_packet(char *packet);
void 	process_packet();
void 	send_next_price_tag();
void 	send_next_ad();
