#include "uart.h"
#include <avr/io.h>
#include <avr/interrupt.h> 

#define UBRR_VAL ((F_CPU+BAUD*8)/(BAUD*16)-1)   // clever runden

int uart_putc(char c)
{
    while (!(UCSRA & (1<<UDRE)))  /* warten bis Senden moeglich */
    {
    }                             
 
    UDR = c;                      /* sende Zeichen */
    return 0;
}
 
 
/* puts ist unabhaengig vom Controllertyp */
void uart_puts (char *s)
{
    while (*s)
    {   /* so lange *s != '\0' also ungleich dem "String-Endezeichen" */
        uart_putc(*s);
        s++;
    }
}

uint8_t uart_getc(void)
{
    while (!(UCSRA & (1<<RXC)))   // warten bis Zeichen verfuegbar
        ;
    return UDR;                   // Zeichen aus UDR an Aufrufer zurueckgeben
}


void uart_gets( char* Buffer, uint8_t MaxLen )
{
  uint8_t NextChar;
  uint8_t StringLen = 0;
 
  NextChar = uart_getc();         // Warte auf und empfange das nächste Zeichen
 
                                  // Sammle solange Zeichen, bis:
                                  // * entweder das String Ende Zeichen kam
                                  // * oder das aufnehmende Array voll ist
  while( (NextChar != '>') && (StringLen < MaxLen - 1 )) {
    *Buffer++ = NextChar;
    StringLen++;
    NextChar = uart_getc();
  }
 
                                  // Noch ein '\0' anhängen um einen Standard
                                  // C-String daraus zu machen
  *Buffer = '\0';
}



void init_uart(void)
{
    UCSRB |= (1<<TXEN);                // UART TX einschalten
	UCSRB |= ( 1 << RXEN );

    UCSRC |= (1<<URSEL)|(3<<UCSZ0);    // Asynchron 8N1 
 
    UBRRH = UBRR_VAL >> 8;
    UBRRL = UBRR_VAL & 0xFF;

}



// SOFTWARE UART
// taken from: http://www.roboternetz.de/wissen/index.php/Software-UART_mit_avr-gcc


void uartSW_init()
{
    uint8_t tifr = 0;
    uint8_t sreg = SREG;
    cli();

    // Mode #4 für Timer1 
    // und volle MCU clock 
    // IC Noise Cancel 
    // IC on Falling Edge 
    TCCR1A = 0;
    TCCR1B = (1 << WGM12) | (1 << CS10) | (0 << ICES1) | (1 << ICNC1);

    // OutputCompare für gewünschte Timer1 Frequenz 
    OCR1A = (uint16_t) ((uint32_t) F_CPU/BAUDRATE);

#ifdef SUART_RXD
    SUART_RXD_DDR  &= ~(1 << SUART_RXD_BIT);
    SUART_RXD_PORT |=  (1 << SUART_RXD_BIT);
    TIMSK |= (1 << TICIE1);
    tifr  |= (1 << ICF1) | (1 << OCF1B);
#else
    TIMSK &= ~(1 << TICIE1);
#endif // SUART_RXD 

#ifdef SUART_TXD
    tifr |= (1 << OCF1A);
    SUART_TXD_PORT |= (1 << SUART_TXD_BIT);
    SUART_TXD_DDR  |= (1 << SUART_TXD_BIT);
    outframe = 0;
#endif // SUART_TXD 

    TIFR = tifr;

    SREG = sreg;
}

#ifdef SUART_TXD
void uartSW_putc (const char c)
{
    do
    {
        sei(); nop(); cli(); // yield(); 
    } while (outframe);

    // frame = *.P.7.6.5.4.3.2.1.0.S   S=Start(0), P=Stop(1), *=Endemarke(1) 
    outframe = (3 << 9) | (((uint8_t) c) << 1);

    TIMSK |= (1 << OCIE1A);
    TIFR   = (1 << OCF1A);

    sei();
}

void uartSW_puts (char *s)
{
    while (*s)
    {   /* so lange *s != '\0' also ungleich dem "String-Endezeichen" */
        uartSW_putc(*s);
        s++;
    }
}



#endif // SUART_TXD 

#ifdef SUART_TXD
SIGNAL (SIG_OUTPUT_COMPARE1A)
{
    uint16_t data = outframe;
   
    if (data & 1)      SUART_TXD_PORT |=  (1 << SUART_TXD_BIT);
    else               SUART_TXD_PORT &= ~(1 << SUART_TXD_BIT);
   
    if (1 == data)
    {
        TIMSK &= ~(1 << OCIE1A);
    }   
   
    outframe = data >> 1;
}
#endif // SUART_TXD


#ifdef SUART_RXD
SIGNAL (SIG_INPUT_CAPTURE1)
{
    uint16_t icr1  = ICR1;
    uint16_t ocr1a = OCR1A;
   
    // Eine halbe Bitzeit zu ICR1 addieren (modulo OCR1A) und nach OCR1B
    uint16_t ocr1b = icr1 + ocr1a/2;
    if (ocr1b >= ocr1a)
        ocr1b -= ocr1a;
    OCR1B = ocr1b;
   
    TIFR = (1 << OCF1B);
    TIMSK = (TIMSK & ~(1 << TICIE1)) | (1 << OCIE1B);
    inframe = 0;
    inbits = 0;
}
#endif // SUART_RXD

#ifdef SUART_RXD
SIGNAL (SIG_OUTPUT_COMPARE1B)
{
    uint16_t data = inframe >> 1;
   
    if (SUART_RXD_PIN & (1 << SUART_RXD_BIT))
        data |= (1 << 9);
      
    uint8_t bits = inbits+1;
   
    if (10 == bits)
    {
        if ((data & 1) == 0)
            if (data >= (1 << 9))
            {
#ifdef _FIFO_H_         
                _inline_fifo_put (&infifo, data >> 1);
#else            
                indata = data >> 1;
#endif // _FIFO_H_            
                received = 1;
            }
      
        TIMSK = (TIMSK & ~(1 << OCIE1B)) | (1 << TICIE1);
        TIFR = (1 << ICF1);
    }
    else
    {
        inbits = bits;
        inframe = data;
    }
}
#endif // SUART_RXD


#ifdef SUART_RXD
#ifdef _FIFO_H_

int uartSW_getc_wait()
{
    return (int) fifo_get_wait (&infifo);
}

int uartSW_getc_nowait()
{
    return fifo_get_nowait (&infifo);
}

#else // _FIFO_H_

int uartSW_getc_wait()
{
    while (!received)   {}
    received = 0;
   
    return (int) indata;
}

int uartSW_getc_nowait()
{
    if (received)
    {
        received = 0;
        return (int) indata;
    }
   
    return -1;
}

#endif // _FIFO_H_
#endif // SUART_RXD
