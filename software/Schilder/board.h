#ifndef BOARDH
#define BOARDH


#define SET_LED1   (PORTD |= 1<<PD5)
#define CLR_LED1   (PORTD &= ~(1<<PD5))
#define TGL_LED1   ((PORTD & (1<<PD5))?CLR_LED1:SET_LED1)

#define SET_LED2   (PORTD |= 1<<PD6)
#define CLR_LED2   (PORTD &= ~(1<<PD6))
#define TGL_LED2   ((PORTD & (1<<PD6))?CLR_LED2:SET_LED2)

#define SET_LED3   (PORTD |= 1<<PD7)
#define CLR_LED3   (PORTD &= ~(1<<PD7))
#define TGL_LED3   ((PORTD & (1<<PD7))?CLR_LED3:SET_LED3)


#define PIO_1       PC5   
#define DDR_PIO1    DDRC
#define PORT_PIO1   PORTC
#define PIN_PIO1    PINC  
#define ISSET_PIO1  ((PIN_PIO1 & (1<<PIO_1))?0:1)

#define PIO_2       PC4
#define DDR_PIO2    DDRC
#define PORT_PIO2   PORTC
#define PIN_PIO2    PINC  
#define ISSET_PIO2  ((PIN_PIO2 & (1<<PIO_2))?0:1)

#define PIO_3       PD3
#define DDR_PIO3    DDRD
#define PORT_PIO3   PORTD
#define PIN_PIO3    PIND 
#define ISSET_PIO3  ((PIN_PIO3 & (1<<PIO_3))?0:1) 


#endif
