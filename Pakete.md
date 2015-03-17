#pakete server->lampe  bzw lampe->schilder

# Introduction #

Add your content here.


# Details #

Checksumme:
Checksummen immer über alle Zeichen bis zur csum, inklusive "|", ohne "<". Checksummen werden auf einem unsigned char (8Bit) berechnet und übertragen. Daraus folgen max. 3 Ascii Ziffern für die Checksumme.

-sendtrace
  * <1|anaus|kassen id|csum>
  * anaus=1 -> lampe schickt "sendtrace" an Schilder weiter
  * Werbeschilder antworten mit einem Funkpaket an die "Kassen ID" der Form
  * <LampenID,relative Zeit,.....,LampenID, relative Zeit>
  * Die Frequenz der realtiven Uhr beträgt 1800/255 Hz

-tracepacket (beinhaltet einen trace)
  * <8|{timestamp|lampid}|csum>
  * alles in {} wiederholt sich beliebig oft

-set ad
  * <2|lampid|text1|text2|text3|text4|csum>

-reset lamp buffers
  * <3|csum>

//toggle ad / pricetag
//<4|mode|csum> mode=0: ad  mode=1: pricetag
//Ersetzt durch Jumper an der Hardware, später event. durch "Dip Schalter" realisiert.

-changelampid (für traces)
  * <5|neuelampenid|csum>

-show SchildID(schild)
  * <6|csum>
  * Das Schild zeigt seine (eindeutige) ID/Adresse an mit der die Preisschilder in CMD7 angesprochen werden.

-set pricetag
  * <7|schildID|produktname|preis|csum>