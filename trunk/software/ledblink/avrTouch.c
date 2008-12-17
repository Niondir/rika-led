#include <avr/io.h>
#include <avr/interrupt.h> 
#include <util/delay.h>
#include <avr/sleep.h>
#include <stdlib.h>

#include <stdint.h>
#include <stdio.h>

#include "avrTouch.h"
#include "uart.h"
#include "xbee.h"

void init_adc(void);
void init_timer(uint16_t delay_ms); //Nach Ablauf des timers wird der adc gestartet
void goto_sleep(void);
void start_XYmessung(void);
uint8_t test_pressed(void);
void ButtonPullUp(char on); 

char ring_buf_x(uint16_t add);
char ring_buf_y(uint16_t add);

volatile uint8_t  touch_state   = 0;
volatile uint8_t  new_touch_val = 0;
volatile uint8_t  sleep_flag    = 0;

uint16_t 		  x_val_adc     = 0;
uint16_t 		  y_val_adc     = 0; 
int16_t          nt_x, nt_y, x_old, y_old;
int16_t           diff_x, diff_y;
char              debug[100];
signed char       xDiff, yDiff, Buttons;

uint8_t DebugModus = 0;

uint8_t warmUpcnt = 0;

int main(void)
{
  sei(); //global irq an



  init_adc();
  init_uart();
  uartSW_init(); //software uart
  
  if(init_Buttons()) //Buttons init, returns true if all Buttons are pressed (enter DebugModus)
  {
	DebugModus = 1;
	uartSW_puts("\r\n**** GroupMouse: Debug Modus active ****\r\n");
  }

  init_xbee();
  

  
 
   
 //   DDR_XBEE_SLEEP |= 1<< XBEE_SLEEP;
 //   XBEE_WAKEUP;
 //   PORT_XBEE_SLEEP&= ~(1<< XBEE_SLEEP);
 //XBEE_WAKEUP;
 
  DDRD|=1<<6;
  PORTD|=1<<6;
 
  if(test_pressed())
  {
	   start_XYmessung();
  }
  else goto_sleep(); //after wakeup: start_XYmessung()

  
  while(1)
  {
       
	while(!new_touch_val && !sleep_flag);

	
	if(sleep_flag) //Touch nicht mehr gedrückt, sleep flag wird u.U. nach einer Messung gesetzt wenn touch nicht mehr gedrückt
	{
	 
	   goto_sleep();
	   sleep_flag=0;
	}
	else
	{
	   new_touch_val=0;

	   if(warmUpcnt<WARMUP_CNT) warmUpcnt++;
	   else
	   { 
		  // nt_x= (NT_SCREEN_X/ (NT_XMAX-NT_XMIN)) * x_val_adc - ((NT_SCREEN_X*NT_XMIN)/(NT_XMAX-NT_XMIN));
		  // nt_y= (NT_SCREEN_Y/ (NT_YMAX-NT_YMIN)) * y_val_adc - ((NT_SCREEN_Y*NT_YMIN)/(NT_YMAX-NT_YMIN));
		  // sprintf(debug, "x: %u y: %u\r\n", nt_x, nt_y);
      
	       
	   
		   x_old = nt_x;
		   y_old = nt_y;

		  // uart_puts(debug);
		  if(DebugModus)
		  {
			  sprintf(debug, "\r\n%d %d",  x_val_adc, y_val_adc);
			  uartSW_puts(debug);
		  }

	   } // end warmUpcnt check

	   start_XYmessung(); //Nähste Messung starten...
	   
	}



  }


  return 0;
}


void init_adc(void)
{
  ADMUX |= (1<<REFS0); // VCC = Ref
  ADCSRA = (1<<ADEN) | (1<<ADPS2)| (1<<ADPS1);    // Frequenzvorteiler /64
  
  ADCSRA |= (1<<ADSC);              // dummy Messung
  while ( ADCSRA & (1<<ADSC) )
  {
     ;    
  }

  ADCSRA |= (1<<ADIE); //adc irq an
}


void start_adc(void)
{
  ADCSRA |= (1<<ADSC);
}

void adc_set_channel(char chan)
{
  ADMUX &= 0xf0;  //mux regs clearen
  if(chan=='x')
  {
    if(X1>0)
	ADMUX |= X1;
  }
  else //y
  {
    if(Y1>0)
	ADMUX |= Y1;
  }
}


inline uint16_t get_ADCval()
{
  uint16_t result;

  result = ADCL;      
  result += (ADCH<<8); 
  return result;
}


void setup_read_x(void)
{
   adc_set_channel('y');   // messen über y(Y1), adc mux setzen
    
   DDR_Y1 &= ~(1<<Y1);  // y1 = Eingang
   DDR_Y2 &= ~(1<<Y2);  // y2 = Eingang
   PORT_Y1 &= ~(1<<Y1); // y1 pullups aus
   PORT_Y2 &= ~(1<<Y2); // y2 pullups aus


   
   DDR_X1  |= 1<<X1; 
   PORT_X1 |= 1<<X1; //X1 Ausgang high

   DDR_X2  |= 1<<X2;
   PORT_X2 &= ~(1<<X2); //X2 Ausgang low

   
}

void setup_read_y(void)
{
   adc_set_channel('x');   // messen über x(X1), adc mux setzen
 
   DDR_X1 &= ~(1<<X1);  // x1 = Eingang
   DDR_X2 &= ~(1<<X2);  // x2 = Eingang
   PORT_X1 &= ~(1<<X1); // x1 pullups aus
   PORT_X2 &= ~(1<<X2); // x2 pullups aus
   
   DDR_Y1  |= 1<<Y1; 
   PORT_Y1 |= 1<<Y1; //y1 Ausgang high

   DDR_Y2  |= 1<<Y2;
   PORT_Y2 &= ~(1<<Y2); //y2 Ausgang low 
}


void goto_sleep(void)
{
  ADCSRA &= ~(1<<ADEN); //Disable adc 2 save power
  
  //XBEE_SLEEP; //xbee sleep
  //PORT_XBEE_SLEEP|= 1<< XBEE_SLEEP

  DDR_Y1  |= 1<<Y1; //ausgang
  DDR_Y2  |= 1<<Y2;
  PORT_Y1 &= ~(1<<Y1); //Y1, Ausgang Low
  PORT_Y2 &= ~(1<<Y2); //Y2, Ausgang Low

  DDR_X1 &= ~(1<<X1);  // x1 = Eingang
  DDR_X2 &= ~(1<<X2);  // x2 = Eingang
  PORT_X1 |= 1<<X1;    // x1 pullups an
  PORT_X2 |= 1<<X2;    // x2 pullups an

  PORTD&= ~(1<<6); //LED aus

  ButtonPullUp(0); //Button Pullups aus... (StromSparen)

  if(DebugModus) uartSW_puts("\r\n Entering SleepMode");
  
  XBEE_ENTER_SLEEP;		  
  _delay_ms(1);
  
  //Goto Sleep Sequence
  set_sleep_mode(SLEEP_MODE_PWR_DOWN);
  cli();
  MCUCR &= ~( (1<< ISC01) | (1<<ISC00) ); //INT0 Low Level Interrupt
  GICR |= (1<<INT0);
  sleep_enable();
  sei();
  sleep_cpu();
   
  //After Wakeup via INT0 (Touch Press) ISR
  sleep_disable();
  XBEE_LEAVE_SLEEP;
  ButtonPullUp(1);
  warmUpcnt=0;
  if(DebugModus) uartSW_puts("\r\n Leaving SleepMode");

  init_adc();        //(re)anable adc
  start_XYmessung(); //nächste Messung starten
  
  //LED AN
  PORTD|=1<<6;

}


void start_XYmessung(void)
{
   touch_state = 0;
   setup_read_x();
   init_timer(UPDATE_RATE_MS); //nach ablauf wird der adc gestartet
}

uint8_t test_pressed(void)
{
  

  DDR_Y1  |= 1<<Y1; //ausgang
  DDR_Y2  |= 1<<Y2;
  PORT_Y1 &= ~(1<<Y1); //Y1, Ausgang Low
  PORT_Y2 &= ~(1<<Y2); //Y2, Ausgang Low

  DDR_X1 &= ~(1<<X1);  // x1 = Eingang
  DDR_X2 &= ~(1<<X2);  // x2 = Eingang
  PORT_X1 |= 1<<X1;    // x1 pullups an
  PORT_X2 |= 1<<X2;    // x2 pullups an

  _delay_ms(1);

 if( (! (PIN_X1 & (1<<X1)) ) || (! (PIN_X2 & (1<<X2)) ) )return 1;
 else return 0;
}


#define F_TIMER  (F_CPU/1024)
#define TICKS_MS (F_TIMER/1000)

void init_timer(uint16_t delay_ms)
{
 
 TCNT0  = 0xff -( (delay_ms*TICKS_MS) );
 TCCR0  = 0x05;     // F_CPU/1024
 TIMSK |= 1<<TOIE0; // Timer0 Overflow IRQ enable

 
 /*
  TCNT1  = 0;
  OCR1A   = delay_ms*TICKS_MS; 
  
  TIMSK |= 1<<OCIE1A; //Output compare irq an

  TCCR1A = 0; // no pwm, no toggle, just counting
  TCCR1B = 0x0D; // F_CPU/1024 , clear on compare match, + start counter
 */

}  

ISR(TIMER0_OVF_vect)
{
  TIMSK &= ~(1<<OCIE1A); //overflow irq aus
  TCCR0  = 0; //timer0 aus
   
  start_adc(); //delay vorbei -> jetzt ADC starten (siehe State Diagramm)
}

/*
ISR(TIMER1_COMPA_vect)//Timer1 compare Interrupt
{
 TIMSK &= ~(1<<OCIE1A);  // timer1 compare irq aus
 TCCR1B   =0;            // Timer1 aus
 start_adc();
}
*/



ISR(ADC_vect) //ADC Interrupt
{ 
  if(touch_state==0)
  {
     x_val_adc = get_ADCval();
     touch_state = 1;
	 setup_read_y();
   
	 //init_timer(1); // 1mS mini pause auf umkonfiguration der ports warten warten , dann start_adc  (in timer isr)
	  start_adc();
  }
  else if(touch_state==1)
  {
    y_val_adc = get_ADCval();
  

	if(test_pressed()) // weitere messung
	{
	   new_touch_val=1; //werte verarbeiten
	}
	else // nicht mehr gedrückt -> goto sleep
	{
    	sleep_flag=1; //schlafen gehen (werte sind unbrauchbar, da gemessen im Moment des Loslassen)
	}

  }

}

ISR(INT0_vect)//Wakeup ISR
{
  GICR &= ~(1<<INT0); //INT0 irq aus
}

char init_Buttons(void) 
{

  DDR_BUTTON1 &= ~(1<<BUTTON1); //Button1 Eingang
  DDR_BUTTON2 &= ~(1<<BUTTON2); //Button2 Eingang
  DDR_BUTTON3 &= ~(1<<BUTTON3); //Button3 Eingang

  PORT_BUTTON1 |= (1<<BUTTON1); //Button 1 Pullup an
  PORT_BUTTON2 |= (1<<BUTTON2); //Button 2 Pullup an
  PORT_BUTTON3 |= (1<<BUTTON3); //Button 3 Pullup an

  _delay_ms(1); //Wait for singals stable

  //returns "true", if all Buttons are pressed (used to enter Debug Mode)
  return ( !(PIN_BUTTON2 & (1<<BUTTON1)) && !(PIN_BUTTON2 & (1<<BUTTON2)) && !(PIN_BUTTON3 & (1<<BUTTON3)) );
}

void ButtonPullUp(char on)
{
  if(on)
  {
	  PORT_BUTTON1 |= (1<<BUTTON1); //Button 1 Pullup an
	  PORT_BUTTON2 |= (1<<BUTTON2); //Button 2 Pullup an
	  PORT_BUTTON3 |= (1<<BUTTON3); //Button 3 Pullup an    
  }
  else
  {
	  PORT_BUTTON1 &= ~(1<<BUTTON1); //Button 1 Pullup an
	  PORT_BUTTON2 &= ~(1<<BUTTON2); //Button 2 Pullup an
	  PORT_BUTTON3 &= ~(1<<BUTTON3); //Button 3 Pullup an
  }
}


