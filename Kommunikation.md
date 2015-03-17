# [Server](Server.md) -> [Lampe](Lampe.md) -> [Schild](Schild.md) #

## Kommunikationsstack ##
### Lampenschicht ###
  * Header: LampenID, Checksumme
### Anwendungsschicht ###
  * Header: Timestamp, Checksumme, Length
  * Body: CommandNr (Neue Daten oder z.B. Sleep für x Minuten), Daten (Anzeigetext)
### Paketschicht ###
  * Header: Startbyte, SenderID, ReceiverID, Length, Sequenznummer, Checksumme
### optical adjustment layer ###
  * Optional zur korrektur des flackerns
  * Invertiertes byte schicken
### uart Schicht ###
  * Interpretation des bitstromes
### physikalische Schicht ###
  * Lichtimpulse senden

# [Client](Client.md) < - > [Server](Server.md) #
## Packets ##
Bestehend aus Binärdaten: ID, Nutzdaten

### RequestLamps ###

### RequestSigns ###

### LoginClient ###
  * ID: 0x01
  * Username
  * Password

### AccountLoginAck ###
  * ID: 0x02
  * ServerInfo

### AccountLoginRej ###
  * ID: 0x03
  * Reason