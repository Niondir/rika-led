/*
 Lib für Electronic Assembly Display EA SER204-92, 4x20 Zeichen
*/


#include <stdlib.h>
#include <avr/io.h>
#include <util/delay.h>

#include "uart.h"
#include "display.h"

#define ESC 0x1b

void set_DisplayModus(char mode)
{
  LCD_PUTC(ESC);
  LCD_PUTC('M');
  LCD_PUTC(mode);
}

void set_CursorForm(char mode)
{
  LCD_PUTC(ESC);
  LCD_PUTC('C');
  LCD_PUTC(mode);
}

void set_CursorPos(char col, char row)
{
  LCD_PUTC(ESC);
  LCD_PUTC('O');
  LCD_PUTC(col); //Spalte
  LCD_PUTC(row); //Zeile
  _delay_ms(25);
}

void clr_Screen(void)
{
  LCD_PUTC(12); //Formfeed
  _delay_ms(100);
}


void init_Display(int8_t modus)
{
  set_DisplayModus(modus); // Clear Modus, automatischer Zeilenumbruch aus
  set_CursorForm(0);   // Cursor unsichtbar
  clr_Screen();
  set_CursorPos(1,1);  // Default Pos
}

void write_Display(char* data, char col, char row)
{
  if(col>20) col=20;
  if(row>4)row=4;

  set_CursorPos(col,row);
  LCD_PUTS(data);
}

void clear_row(char row)
{
  set_CursorPos(1,row);
  LCD_PUTS("                    ");
}
