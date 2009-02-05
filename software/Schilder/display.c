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
  //LCD_PUTC(ESC);
  //LCD_PUTC('M');
  //LCD_PUTC(mode);
}

void set_CursorForm(char mode)
{
  //LCD_PUTC(ESC);
  //LCD_PUTC('C');
  //LCD_PUTC(mode);
}

void set_CursorPos(char col, char row)
{
  LCD_PUTC(ESC);
  LCD_PUTC('O');
  LCD_PUTC(col-1); //Spalte, x
  LCD_PUTC(row-1); //Zeile, y
 // _delay_ms(100);
}

void clr_Screen(void)
{
  LCD_PUTC('\f'); //Formfeed
  //_delay_ms(100);
}


void init_Display(int8_t modus)
{
//  set_DisplayModus(modus); // Clear Modus, automatischer Zeilenumbruch aus
//  set_CursorForm(0);   // Cursor unsichtbar
  clr_Screen();
  set_CursorPos(1,1);  // Default Pos
}

void write_Display(char* data, char col, char row)
{
  if(col>20) col=20;
  //if(row>4)row=4;
  clear_row(row);
  set_CursorPos(col,row);
  LCD_PUTS(data);
}

void write_Display4x20Border(void)
{
  
/*  set_CursorPos(21,1);
  LCD_PUTS("#");    						//Unterstrich unter "reales" Display
  set_CursorPos(21,2);
  LCD_PUTS("#"); 
  set_CursorPos(21,3);
  LCD_PUTS("#");
  set_CursorPos(21,*/

  set_CursorPos(1,5);

  LCD_PUTS("---------------------");    						//Unterstrich unter "reales" Display
 // LCD_PUTS("*********************");   
}

void write_credits(void)
{
  set_CursorPos(1,14);
  LCD_PUTS("---------------------"); 
  set_CursorPos(1,15);
  LCD_PUTS("RIKA Seminar WS 08/09"); 
  set_CursorPos(1,16);
  LCD_PUTS("---------------------"); 
}

void clear_row(char row)
{
  set_CursorPos(1,row);
  LCD_PUTS("                    "); //20 zeichen löschen
}

void clear_row_long(char row)
{
  set_CursorPos(1,row);
  LCD_PUTS("                     "); //21 zeichen löschen
}
