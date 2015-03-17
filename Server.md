# Einleitung #
Aufteilung in 2 Module: Datenbankkommunikation, Funkverbindung

# Klassen #
  * SendLogic
  * RadioCommunication
  * NetworkCommunication
  * Client
  * ClientHandler
  * _Packet_
  * MessagePump
  * NetworkEvents
  * PacketHandler
  * PacketHandlers
  * DataTable
  * Listener

## SendLogic ##
Was wird wann wie oft gesendet.

## RadioCommunication ##
Verwaltet die Communikation über das Funkmodul.

  * Wird nur zum senden verwendet.

### Funktionen ###
  * Send(Packet p)

## NetworkCommunication ##
  * Verwaltet mehrere Clients über TCP/IP Verbindungen.
  * Verwaltet eine MessageQueue, ruft NetworkEvents auf

### Funktionen ###
  * OnReceive()

## Client ##
  * Informationen über einen Client
  * Netzwerkstatus


## ClientHandler ##
  * Statischer Zugriff auf Clients möglich
  * Suche nach Clients in allen vorhandenen

### Funktionen ###
  * Send(Packet p)

## MessagePump ##
Arbeitet die ankommenden Netzwerknachrichten ab

## NetworkEvents ##
Handler für alle netzwerk events

### Funktionen ###
  * Ein static Event für jede Aktion die durch empfangene Pakete ausgeführt wird


## PacketHandler ##
### Felder ###
  * int packetID
  * int length
  * OnPacketReceive OnReceive

## PacketHandlers ##
Verwaltet PacketHandler, für jedes Packet wird ein PacketHandler registriert. Siehe: [Kommunikation](Kommunikation.md).

Die Callbacks rufen Events in NetworkEvents auf.

### Funktionen ###
  * Register(int packetID, int length, OnPacketReceive onReceive)
  * GetHandler(int packetID)
  * alle OnReceive funktionen

## Listener ##

### Funktionen ###
  * OnAccept
  * 




# grober Aufbau #
## Client Handler ##
  * Netzweckschnitstelle zum Client (TCP/IP, Xml-RPC, ISS)
  * Benutzerverwaltung: Login, Berechtigungen
  * Weiterleitung der Daten von und an Data Manager
## Data Manager ##
  * Anbindung an Datenbank
  * Schnitstelle für Datenbankanfragen
  * Bereitstellung abstrakter Datenobjekte

## Funk Manager ##
  * Ansteuerung des Funksenders (Serielle Schnittstelle)
  * Verwaltung der Daten in den Lampen