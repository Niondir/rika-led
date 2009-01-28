#ifndef DISPLAYH
#define DISPLAYH

#include "uart.h"

#define LCD_PUTC uartSW_putc 
#define LCD_PUTS uartSW_puts

#define DISPLAY_ROWS        4
#define DISPLAY_ROWCHARS    20

void init_Display(int8_t modus);
void clr_Screen(void);
void set_CursorPos(char col, char row);
void set_CursorForm(char mode);
void set_DisplayModus(char mode);
void write_Display(char* data, char col, char row);
void clear_row(char row);

#endif
