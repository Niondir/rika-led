#ifndef AVRTOUCH
#define AVRTOUCH

#include "display.h"

// Constants
#define FIRMWARE_VERSION    110

#define SIGN_TYPE_TROLLEY        0
#define SIGN_TYPE_PRICE          1
#define SIGN_TYPE_NOT_DETECTED   2 //fürs init

#define CMDNR_SEND_TRACE    1
#define CMDNR_SET_AD        2 
#define CMDNR_SET_PRICE     7
#define CMDNR_SHOW_ID       6

#define ARGCNT_SEND_TRACE   2
#define ARGCNT_SET_AD       5
#define ARGCNT_SET_PRICE    3
#define ARGCNT_SHOW_ID      0

#define MAX_ARGS            5
#define ARG_SIZE            21  //20 Zeichen+ '\0'
#define TRACE_LAMP_CNT      25  //benötiger Platz ist TRACE_LAMP_CNT*2*2, da zu jeder 16Bit LampenId auch ein 16Bit Zeitstempel gehört



// Prototypes
uint8_t detectSignMode(void);
void show_status(uint8_t WriteToDisplayBuffer);
int8_t  get_packet(void);
void    packet_action(void);
void    initTraceCounter(int8_t run);
void    debug_packet(void);
uint8_t calc_csum(char* data);

// Typedefs
typedef struct 
{
   uint16_t signUniqueID;        // Nur für Preisschilder, Werbeschilder haben keine Adresse, auch schildID
   uint8_t  signType;
   char     displayMemory[DISPLAY_ROWS][DISPLAY_ROWCHARS+1];
   uint8_t  displayRefreshFlag;  // Wird gesetzt, wenn das Display in jedem Fall neugezeichnet werden muss (mit dem Inhalt des D-Buffers)
   uint16_t packetsOK;
   uint16_t packetsBAD;
} sign_t;

typedef struct 
{
   uint8_t packetCmdNr; 
   char    args[MAX_ARGS][ARG_SIZE];
} packet_t;

typedef struct 
{
   uint8_t            pos; //schreib position
   volatile uint16_t  TimeNow;
   uint16_t           times[TRACE_LAMP_CNT]; 
   uint16_t           lampIDs[TRACE_LAMP_CNT];
} trace_t;


#endif
