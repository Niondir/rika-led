#ifndef AVRTOUCH
#define AVRTOUCH

void setup_read_x(void);
void setup_read_y(void);
char init_Buttons(void);

//Zeit zwischen 2 Messungen 
//falls Touch gedrückt gehalten wird
#define UPDATE_RATE_MS 2 //pause zwischen 2 Messvorgängen
#define SPEED_MULTI    3  //Speed multiplikator
#define EQUAL_CNT      5 //Soviele Ähnliche Messungen bis neuer Messwert ausgeben wird (an arm)
#define EQUAL_DISTANCE 60 //max +- Abweichung damit messungen als Ähnlich gelten (pixel)

#define WARMUP_CNT 7 //Anzahl komplette x,y Messung bis ergebnisse "ausgespuckt" werden, "entprellen"

#define DEBUG
//Bedingungen für die Pins:
// X1 muss ADC Channel sein
// Y1 muss ADC Channel sein
// X2 muss auf INT0 liegen
// Y2 beliebiger PIO

#define X1      PC0   //-> adc
#define DDR_X1  DDRC
#define PORT_X1 PORTC
#define PIN_X1  PINC  //für test_pressed()

#define X2      PD2   //-> INT0 
#define DDR_X2  DDRD  
#define PORT_X2 PORTD 
#define PIN_X2  PIND  //für test_pressed()

#define Y1      PC1   //-> adc
#define DDR_Y1  DDRC
#define PORT_Y1 PORTC

#define Y2      PC2   //-> beliebigen freien PIO
#define DDR_Y2  DDRC
#define PORT_Y2 PORTC

#define NT_XMIN 85 //max und min ADC Ergebnisse (wichtig für umrechnung auf eine bestimmte Display Größe)
#define NT_XMAX 927
#define NT_YMIN 100
#define NT_YMAX 920

#define NT_SCREEN_X (320.0)
#define NT_SCREEN_Y (240.0)



#define DDR_BUTTON1  DDRC
#define PORT_BUTTON1 PORTC
#define PIN_BUTTON1  PINC
#define BUTTON1      PC5

#define DDR_BUTTON2  DDRC
#define PORT_BUTTON2 PORTC
#define PIN_BUTTON2  PINC
#define BUTTON2      PC4

#define DDR_BUTTON3  DDRD
#define PORT_BUTTON3 PORTD
#define PIN_BUTTON3  PIND
#define BUTTON3      PD3





#endif
