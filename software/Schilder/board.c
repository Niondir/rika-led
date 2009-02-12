#include <stdlib.h>
#include <avr/io.h>
#include "board.h"

void initPIOs(void)
{
    DDR_PIO1 &= ~(1<<PIO_1); //input
    PORT_PIO1 |= (1<<PIO_1); //PullUp an

    DDR_PIO2 &= ~(1<<PIO_2); //input
    PORT_PIO2 |= (1<<PIO_2); //PullUp an

    DDR_PIO3 &= ~(1<<PIO_3); //input
    PORT_PIO3 |= (1<<PIO_3); //PullUp an
}

void initLEDs(void)
{
    DDRD |= 1<<PD5;
// DDRD |= 1<<PD6;
// DDRD |= 1<<PD7;
}
