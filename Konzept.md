# [Datenbank](Datenbank.md) #
  * Software: MySQL
  * Zugriff über [Server](Server.md): odbc

# Funktionen #

## [Client](Client.md) ##
  * Login beim DB Server
  * Traces anzeigen
  * Preisschilder anzeigen, ändern, erstellen
  * Lampen: anzeigen, ändern, erstellen
  * Zonen: erstellen, Lampen zuweisen

## Funk [Server](Server.md) ##
  * Aufträge von DB Server empfangen
  * Aufträge wiederholt senden (so gut es geht sicherstellen das der gewünschte Zustand erreicht wird)
  * Traces von der Kasse empfangen und weiterleiten an DB Server
  * Auftragsliste persistent speichern

## Datenbank Server ##
  * Beliebig viele Clients
  * Beliebig viele Funk Server (als client)
  * Rechteverwaltung
  * DB: Preise (read / write)
  * DB: Preisschild registrieren
  * DB: Benutzerkonten (read / write)
  * DB: Werbeaufträge (read / write)
  * DB: Traces von Funkserver
  * Aufträge an Funkserver senden
  * Aufträge bei Funkserver löschen

## Preisschild ##
### Auf Licht Befehl ###
  * Preis anzeigen

## Einkaufswagen ##
  * Trace speichern
### Auf Licht Befehl ###
  * Trace senden (funk) + reset
  * Anzeige schalten

## [Lampe](Lampe.md) ##
  * Besitzt beliebig viele LED's

# Daten #
## Preisschild ##
  * Preis
  * Warenbezeichnung
  * ID (fest)

## Wagen ##
  * ID (fest)
  * Werbetext
  * Geordnete Liste der Lampen ID's (trace) + Timestamp

## Lampe ##
  * ID

## Server ##