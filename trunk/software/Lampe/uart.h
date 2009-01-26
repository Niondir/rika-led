#ifndef AVRUART
#define AVRUART

#include <stdint.h>

//#define BAUD 115200UL          // Baudrate
#define BAUD 9600UL

void init_uart(void);
void uart_puts(char *s);
int  uart_putc(char c);
uint8_t uart_getc(void);
void uart_gets( char* Buffer, uint8_t MaxLen );


//Software Uart (Debugzwecke) PB0, PB1



//Software UART
#define BAUDRATE 9600
#define nop() __asm volatile ("nop")

#define SUART_TXD
#define SUART_RXD



void uartSW_init();
void uartSW_putc (const char c);
int uartSW_getc_nowait();
int uartSW_getc_wait();
void uartSW_puts (char *s);
	


#ifdef SUART_TXD

     //TOBIMODE
	 
#define SUART_TXD_PORT PORTC
#define SUART_TXD_DDR DDRC
#define SUART_TXD_BIT PC5


//normal mode
/*
//    #define SUART_TXD_PORT PORTB
//    #define SUART_TXD_DDR  DDRB
//    #define SUART_TXD_BIT  PB1 
*/	
    #define SUART_TXD_PORT PORTC
    #define SUART_TXD_DDR  DDRC
	#define SUART_TXD_BIT  PC5 

    static volatile uint16_t outframe;
#endif // SUART_TXD 

#ifdef SUART_RXD
    #define SUART_RXD_PORT PORTB
    #define SUART_RXD_PIN  PINB
    #define SUART_RXD_DDR  DDRB
    #define SUART_RXD_BIT  PB0
    static volatile uint16_t inframe;
    static volatile uint8_t inbits, received;

    #ifdef _FIFO_H_
        #define INBUF_SIZE 4
        static uint8_t inbuf[INBUF_SIZE];
        fifo_t infifo;
    #else // _FIFO_H_ 
        static volatile uint8_t indata;
    #endif // _FIFO_H_ 
#endif // SUART_RXD 

#endif
