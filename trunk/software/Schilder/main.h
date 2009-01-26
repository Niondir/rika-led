#ifndef AVRTOUCH
#define AVRTOUCH

// Constants
#define FIRMWARE_VERSION    100
#define SIGN_TYPE_TROLLEY   0
#define SIGN_TYPE_PRICE     1

#define CMDNR_SEND_TRACE    1
#define CMDNR_SET_AD        2 
#define CMDNR_SET_PRICE     7
#define CMDNR_SHOW_ID       6

#define ARGCNT_SEND_TRACE   0
#define ARGCNT_SET_AD       5
#define ARGCNT_SET_PRICE    3
#define ARGCNT_SHOW_ID      0

#define MAX_ARGS            5
#define ARG_SIZE            25
#define TRACE_LAMP_CNT      25  //Plattz für 25 verschiedene Lampen IDs im Trace, max 255 derzeit (wg. pos datentyp)

// Prototypes
void detectSignMode(void);
void show_status(void);
int8_t get_packet(void);
void packet_action(void);
void initTraceCounter(int8_t run);

// Typedefs
typedef struct 
{
   uint16_t signUniqueID; //Nur für Preisschilder, Werbeschilder haben keine Adresse, auch schildID
   uint8_t  signType;
} sign_t;

typedef struct 
{
   uint8_t packetCmdNr; 
   char    args[MAX_ARGS][ARG_SIZE];
} packet_t;

typedef struct 
{
   uint8_t           pos; //schreib position
   volatile uint16_t  TimeNow;
   uint16_t           times[TRACE_LAMP_CNT]; 
   uint16_t           lampIDs[TRACE_LAMP_CNT];
} trace_t;

#endif
