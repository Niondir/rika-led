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

#endif
