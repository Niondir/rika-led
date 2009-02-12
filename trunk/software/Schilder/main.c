/*
  "MyStore" - Schild Firmware
   
   Rika Projekt WS08/09
   Tobias Rohde
*/

#include <avr/io.h>
#include <avr/interrupt.h>
#include <util/delay.h>
#include <avr/sleep.h>
#include <stdlib.h>
#include <stdint.h>
#include <stdio.h>
#include <string.h>

#include "main.h"
#include "uart.h"
#include "xbee.h"
#include "board.h"
#include "display.h"

// Gloable Vars
sign_t    sign;
packet_t  packet;
trace_t   trace;
char Eurostring[2]="";


//#define DEBUG_MODE
//------------------------------------------------------------------------------
int main(void)
// Main Funktion. Enthält "Main-Loop", die permanent versucht neue Pakete
// zu empfangen/parsen und dann zu verarbeiten.
//------------------------------------------------------------------------------
{
    uint8_t packetRecvStatus;

#ifdef DEBUG_MODE
    char  debug[25];
    char  debug2[25];
    uint8_t toggleFlag=0;
#endif

    Eurostring[0] = EUROZEICHEN;
    Eurostring[1] = '\0';

    sei();               										    	// global irq an

    sign.signType   = SIGN_TYPE_NOT_DETECTED;
    sign.packetsOK  = 0;
    sign.packetsBAD = 0;

    initLEDs();
    uartSW_init();       												// software uart
    init_uart();

    _delay_ms(500);                 									// startup delay
    write_Display4x20Border();                                        // Rand um 4*20 Zeichen malen (virtuelles billig LCD)
    write_credits();
    initPIOs();          												// Mode Jumper Pins konfigurieren

    detectSignMode();    												// Hardware kann von extern als Wagen oder Presischild konfiguriert werden, Preisschiler sogar mit bis zu 7 Adressen
    initTraceCounter(1); 												// TraceTimer konfigurieren und starten

    show_status(0);       											// Schild Informationen anzeigen, insbesondere SchildID und Typ (AD/Price)
    _delay_ms(2000); 												    // Nach Start 2 Sek Systeminfos anzeigen

    //Main Loop
    while (1)
    {

        if ( (packetRecvStatus = get_packet()) == PACKET_OK)
        {
            packet_action();                  //Das korrekt empfangende Paket verarbeiten

            if (sign.displayRefreshFlag)      //ggf. das Display komplett neu zeichnen
            {
                clr_Screen();
                write_Display(sign.displayMemory[0],1,1);
                write_Display(sign.displayMemory[1],1,2);
                write_Display(sign.displayMemory[2],1,3);
                write_Display(sign.displayMemory[3],1,4);
                sign.displayRefreshFlag = 0;
            }


#ifdef DEBUG_MODE
            if (sign.packetsOK<0xffff)sign.packetsOK++;
            else
            {
                sign.packetsOK=0;
                sign.packetsBAD=0; //Reset both Counters
            }


            toggleFlag=!toggleFlag;
            switch (packet.packetCmdNr)
            {
            case CMDNR_SEND_TRACE:
                sprintf(debug2, "SEND_TRACE");
                break;
            case CMDNR_SET_AD:
                sprintf(debug2, "SET_AD");
                break;
            case CMDNR_SET_PRICE:
                sprintf(debug2, "SET_PRICE");
                break;
            case CMDNR_SHOW_ID:
                sprintf(debug2, "SHOW_ID");
                break;
            default:
                sprintf(debug2, "UNKNOWN");
                break;

            }
            if (toggleFlag) sprintf(debug, "OK: %s", debug2); //Paket korrekt empfangen
            else           sprintf(debug, "ok: %s", debug2); //trotzdem kann sich u.U z.B. durch falsche IDs nichts tun!
            write_Display(debug,1,8);			//in letzter Zeile ganz Rechts diese 2 Zeichen darstellen
            sprintf(debug, "%u Good  vs. %u Bad",  sign.packetsOK, sign.packetsBAD);
            write_Display(debug,1,11);
#endif

        }
        else //Paket nicht erfolgreich empfangen
        {

#ifdef DEBUG_MODE
            if (packetRecvStatus!=1) //Error1 ignorieren (Error1 = Derzeit kein Startzeichen "<" empfangen/verfügbar)
            {

                if (sign.packetsBAD<0xffff)sign.packetsBAD++;
                else
                {
                    sign.packetsOK=0;
                    sign.packetsBAD=0; //Reset both Counters
                }

                toggleFlag=!toggleFlag;
                if (toggleFlag)
                {
                    if (packetRecvStatus==9)sprintf(debug, "CHECKSUM WRONG");
                    else sprintf(debug, "PARSE ERROR(%u)",packetRecvStatus);
                }
                else
                {
                    if (packetRecvStatus==9)sprintf(debug, "Checksum Wrong");
                    else sprintf(debug, "Parse Error(%u)",packetRecvStatus);
                }

                write_Display(debug,1,8);

                sprintf(debug, "%u Good  vs. %u Bad",  sign.packetsOK, sign.packetsBAD);
                write_Display(debug,1,11);
            }
#endif

        }



        if (detectSignMode()==1) // Mode wurde über die externen Jumper verändert
        {
            show_status(1);                  //Schild Informationen anzeigen, insbesondere SchildID und Typ (AD/Price)
            _delay_ms(2000);                //Zeit zum Anzeigen der neuen Status Informationen
            sign.displayRefreshFlag = 1;	 //Display Memory neuzeichnen, da durch show_status anderer Inhalt angezeigt wurde
        }


    }

    return 0;
}


//------------------------------------------------------------------------------
uint8_t detectSignMode(void) 
// Funktion überwacht die drei externen Jumper, ob sich die Schild-ID geändert
// hat. Falls ja, wird die Schild-ID entsprechend der Jumper-Konstellation
// angepasst.
// Gibt 1 zurück, wenn sich eine Änderung ergeben hat gegenüber dem letztem
// Check.
//------------------------------------------------------------------------------
{
    uint8_t signType_yet = sign.signType;
    uint8_t adr_yet      = sign.signUniqueID;

    if (!ISSET_PIO1 && !ISSET_PIO2 && !ISSET_PIO3) // kein Jumper = Wagen-Schild
    {
        sign.signType        = SIGN_TYPE_TROLLEY;
        sign.signUniqueID = 0; // Dummy, wird bei Wagenschild nicht benötigt
        trace.pos =0;          //Trace reseten
    }
    else // Preis-Schild mit 3 Bit Adresse über Jumper einstellbar
    {
        char adr=0;
        sign.signType        = SIGN_TYPE_PRICE;

        // Adresse dekodieren
        adr|=ISSET_PIO1;
        adr|=ISSET_PIO2<<1;
        adr|=ISSET_PIO3<<2;

        sign.signUniqueID=(uint16_t)adr;
    }

    if ((signType_yet!=sign.signType) || (adr_yet!=sign.signUniqueID))
    {


        return 1;
    }
    else
        return 0;
}

//------------------------------------------------------------------------------
void show_status(uint8_t WriteToDisplayBuffer)
// Zeigt Status Informationen auf dem Display an
//------------------------------------------------------------------------------
{
    char tempBuf[DISPLAY_ROWCHARS+1];

    clr_Screen();

    //Firmware Version
    sprintf(tempBuf, "Firm.Version:%u", FIRMWARE_VERSION);
    if (WriteToDisplayBuffer)  strcpy(sign.displayMemory[0], tempBuf); // Zeile 1
    write_Display(tempBuf,1,1);

    //Schild Typ
    if (sign.signType==SIGN_TYPE_TROLLEY)
    {

        if (WriteToDisplayBuffer)  strcpy(sign.displayMemory[1], "SignType    :Trolley"); // Zeile 2
        write_Display("SignType    :Trolley", 1, 2);
    }
    else
    {
        if (WriteToDisplayBuffer)  strcpy(sign.displayMemory[1], "SignType    :Price"); // Zeile 2
        write_Display("SignType    :Price", 1, 2);
    }

    //Schild ID, über die Preisschilder adressiert werden
    sprintf(tempBuf, "UniqueSignID:%u",  sign.signUniqueID);
    if (WriteToDisplayBuffer)  strcpy(sign.displayMemory[2], tempBuf);
    write_Display(tempBuf,1,3);

    //Pakete OK und Pakete BAD anzeigen
    sprintf(tempBuf, "PK. OK:%u E:%u",  sign.packetsOK, sign.packetsBAD);
    if (WriteToDisplayBuffer)  strcpy(sign.displayMemory[3], tempBuf);
    write_Display(tempBuf,1,4);

}


//------------------------------------------------------------------------------
int8_t get_packet(void)
// Versucht Pakete der Form : <CMD-NR|Field1|Field2...|....|FieldN|CSUM>
// zu empfangen. Prüft auch die Csum und führt Syntax Tests durch.
// Gibt 0 bei korrekt empfangenen Paket zurück, ansonsten eine FehlerNummer
//------------------------------------------------------------------------------

{

    int8_t  lastChar;
    char    wBuff[10];   									 //working Buffer für die CMDnr
    int8_t  pos=0;
    int8_t  ArgAnzahl;
    uint8_t csum=0;     									 //checksummen immer über alles bis vor der csum, inklusive "|", ohne "<"
    char    csumCompare[4];                                 //8Bit Checksum max Val 254+'\0' = 4 Zeichen
    uint8_t csumCompareInt;




    //Auf Start eines Pakets warten
    if (uartSW_getc_nowait()!='<') return 1;

    //CMD Nr muss folgen
    lastChar = uartSW_getc_wait();
    if (lastChar<'0' || lastChar>'9') return 2;            //Keine Ziffer, Fehler
    else
    {
        wBuff[pos++]=lastChar;                              //Ziffer speichern
        csum+=lastChar;
    }

    do
    {
        lastChar = uartSW_getc_wait();
        //Nur weitere Ziffern oder Separator sind erlaubt, sonst Fehler
        if      ( !(lastChar<'0' || lastChar>'9') )
        {
            wBuff[pos++]=lastChar;    //weitere Ziffer abspeichern
            csum+=lastChar;
        }
        else if ( lastChar == '|')
        {
            csum+=lastChar;    //Separator gefunden
            wBuff[pos]='\0';
            break;
        }
        else                      				    return 4;                                //ungültiges Zeichen, Fehler
    }
    while (1);

    packet.packetCmdNr = (uint8_t)atoi(wBuff);


//Argumente empfangen:
    switch (packet.packetCmdNr)
    {
    case CMDNR_SEND_TRACE:
        ArgAnzahl = ARGCNT_SEND_TRACE;
        break;
    case CMDNR_SET_AD:
        ArgAnzahl = ARGCNT_SET_AD;
        break;
    case CMDNR_SHOW_ID:
        ArgAnzahl = ARGCNT_SHOW_ID;
        break;
    case CMDNR_SET_PRICE :
        ArgAnzahl = ARGCNT_SET_PRICE;
        break;
    default:
        return 5;
    }


    for (int i=0; i<ArgAnzahl; i++)
    {
        pos=0;
        do
        {
            lastChar = uartSW_getc_wait();
            if      ( lastChar == '<' || lastChar == '>' || pos > (ARG_SIZE-1))    return 6;       //ungültiges Zeichen, oder zuviele empfangen

            if ( lastChar != '|' )
            {
                (packet.args)[i][pos++]=lastChar;    //Zeichen abspeichern
                csum+=lastChar;
            }
            else
            {
                (packet.args)[i][pos]='\0';    //Separator gefunden
                csum+=lastChar;
                break;
            }
        }
        while (1);
    }


//Checksumme empfangen
    pos=0;
    lastChar = uartSW_getc_wait();

    //mindestens 1 Zahl muss empfangen werden
    if      ( !(lastChar<'0' || lastChar>'9') )
    {
        csumCompare[pos++]=lastChar;    //Zahl gefunden
    }
    else    return 7;                                                                  //Keine Zahl, Fehler

    do  //max 2 weitere Ziffern der checksumme empfangen, wg. 8Bit Größe
    {
        lastChar = uartSW_getc_wait();
        if ( lastChar == '>')
        {
            csumCompare[pos]  = '\0';    //Paket empfang abgeschlossen
            break;
        }

        if      ( !(lastChar<'0' || lastChar>'9')  && pos<3 )
        {
            csumCompare[pos++]=lastChar;    //Zahl empfangen und weniger als 2 Ziffern zuvor empfangen
        }
        else return 8;                                                                         //ungültiges Zeichen oder zuviele Ziffern, Fehler

    }
    while (1);


//Checksummen vergleichen
    csumCompareInt    = (uint8_t)atoi(csumCompare);


    if (csumCompareInt==csum)return 0; //Packet korrekt empfangen
    else                    return 9; //Checksummenfehler

}

//------------------------------------------------------------------------------
void packet_action(void)
// Führt für ein Paket in der globalen Struktur "packet" eine der CommandNr.
// entsprechende Operation durch. -> Command Handling
//------------------------------------------------------------------------------
{

    switch (packet.packetCmdNr)
    {
    case CMDNR_SEND_TRACE:


        if (trace.pos >1) //Min 2 Traces müssen vorhanden sein
        {

            char sendPacketBuf[15]; //2*5 Zeichen (16Bit Zahlen als Char) + ',' +'\0'
            uint8_t csum = 0;

            set_dest2( packet.args[1], "0"); //Arg1 enthält die FunkZieladresse des Traceempfängers (Kasse), Arg0 ist egal


            XBEE_SEND_STRING("<8|");
            csum += calc_csum("8|");

            for (int i=0;i<trace.pos;i++)
            {
                sprintf(sendPacketBuf,"%u|%u|", trace.lampIDs[i], trace.times[i] ); //Die Uhr läuft mit (1800/255) Hz
                csum += calc_csum(sendPacketBuf);

                XBEE_SEND_STRING(sendPacketBuf);
            }

            sprintf(sendPacketBuf,"%d>", csum);

            XBEE_SEND_STRING(sendPacketBuf);

            trace.pos = 0;
        }

        break;

    case CMDNR_SET_AD:
        //Paket soll nur für Werbeschilder bearbeitet werden, Preisschilder ignorieren es.
        if (sign.signType==SIGN_TYPE_TROLLEY)
        {
            //Trace speichern, wenn noch Speicher für einen weiteren "Wegpunkt" verfügbar ist
            if (trace.pos < (TRACE_LAMP_CNT-1))
            {
                uint16_t aktLampID = (uint16_t)atoi(packet.args[0]); //Arg0 = LampenID


                if (trace.pos == 0)
                {
                    trace.TimeNow = 0;
                }
                //Nur "Neue" Traces, d.h. LampenID im Paket != LampenID des vorherigen Packets
                if (trace.pos==0 || (aktLampID != (trace.lampIDs)[trace.pos-1]))
                {
                    trace.times[trace.pos]   = trace.TimeNow;
                    trace.lampIDs[trace.pos] = aktLampID;
                    trace.pos++;
                }


            }

            //Anzuzeigende Felder im Paket mit dem Inhalt des Display Memory vergleichen
            if (    (strcmp(sign.displayMemory[0], packet.args[1]) == 0)
                    && (strcmp(sign.displayMemory[1], packet.args[2]) == 0)
                    && (strcmp(sign.displayMemory[2], packet.args[3]) == 0)
                    && (strcmp(sign.displayMemory[3], packet.args[4]) == 0))
            {
                return; //Mem aktuell
            }
            else //Mem neubeschreiben
            {
                sign.displayRefreshFlag = 1;
                strcpy(sign.displayMemory[0], packet.args[1]); //Text1
                strcpy(sign.displayMemory[1], packet.args[2]); //Text2
                strcpy(sign.displayMemory[2], packet.args[3]); //Text3
                strcpy(sign.displayMemory[3], packet.args[4]); //Text4
            }
        }

        break;

    case CMDNR_SHOW_ID:                     //Statusinformationen anzeigen und dannach ein Neuzeichnen des Displays über das Flag veranlassen
        show_status(0);
        _delay_ms(5000);
        sign.displayRefreshFlag = 1;

        break;

    case CMDNR_SET_PRICE:

        //Check, ob Preis für dieses Schild bestimmt ist
        if ((sign.signType==SIGN_TYPE_PRICE) && ((uint16_t)atoi(packet.args[0])) == sign.signUniqueID)
        {
            // Display Memory nochaktuell?
            if ( (strcmp(sign.displayMemory[0], packet.args[1]) == 0) && (strcmp(sign.displayMemory[1], packet.args[2]) == 0) )
            {

                return; //Der Display Memory ist noch aktuell
            }
            else //Display Memory neuschreiben
            {
                sign.displayRefreshFlag = 1;
                strcpy(sign.displayMemory[0], packet.args[1]); // Name
                strcpy(sign.displayMemory[1], packet.args[2]); // Preis,
                strcat(sign.displayMemory[1], " ");
                strcat(sign.displayMemory[1], Eurostring);
                memset(sign.displayMemory[2],' ', 20);
                sign.displayMemory[2][20]='\0';  // Zeile 3 leer
                memset(sign.displayMemory[3],' ', 20);
                sign.displayMemory[3][20]='\0';  // Zeile 4 leer
            }

        }

        break;
    }
}

//------------------------------------------------------------------------------
uint8_t calc_csum(char* data)
// Berechnet eine 8Bit Checksumme, in dem alle Datenbytes aufaddiert werden
//------------------------------------------------------------------------------
{
    uint8_t csum=0, i=0;

    while (data[i]!=0)
    {
        csum+=data[i++];
    }
    return csum;
}

//------------------------------------------------------------------------------
void initTraceCounter(int8_t run)
// Initialisiert den HardwareTimer, der genutzt wird um relative Timestamps
// für die Traces zu erzeugen
//------------------------------------------------------------------------------
{
    trace.pos=0;      // init
    trace.TimeNow=0;

    if (run)
    {
        TIMSK |= 1; //enable Overflow Interrupt
        TCCR0 = 5;  // clk=MCK/1024
    }
    else
    {
        TIMSK &= ~((char)1); //disable Overflow Interrupt
        TCCR0 = 0; // Timer stopped
    }
}

//------------------------------------------------------------------------------
SIGNAL (SIG_OVERFLOW0)
// ISR des Timers, zählt Systemzeit hoch, die als Timestamp für Traces genutzt
// wird.
//------------------------------------------------------------------------------
{
    trace.TimeNow++; // ++Updaterate ist (MCK=1843200Hz/1024)/255(Ticks bis Overflow) = 1800Hz/255 = ca. 7.058823 Hz
}
