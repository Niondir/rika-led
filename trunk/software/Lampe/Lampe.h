#include "xbee.h"
#include "AtMega8.h"


//PROTOTYPES
void clear_buffers();
int get_csum_index();
void calculate_csum(int csumindex);
int checksum_failed();
void change_lampid();
void copy_argument(int rcvBufPos, char *destArray);
