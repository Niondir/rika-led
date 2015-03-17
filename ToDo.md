## Allgemeines ##
  * Doku

## Client ##
  * Alle Interfaces der API implementieren
  * Alle Fehler abfangen
  * Traces sinnvoll darstellen

## Kasse ##
  * Default Account am server anmelden
  * Empfangen der Traces über Funk (kommt 3 mal an!)
  * Senden der Traces an Server

## Server ##
  * Alle Interfaces der API implementieren
  * LampenID berücksichtigen
  * Verifizierung aller externen Daten
  * ResetLampPacket (ClearCache)
  * DeleteSignPacket (auch wenn es nicht da ist) <x|signID>
  * setAddvertisement <cmdId|zeile1|zeile2|zeile3|zeile4>
  * Checksumme für die Packete
  * Escapen der Sonderzeichen < > | \ mit \
  * Timing des sendens (logik)

## Lampe ##
  * LampenID reuasfinden (am Anfang)
  * LampenID mit schicken
  * Reset (ClearCache)
  * Delete Sign (auch wenn es nicht da ist) <x|signID>
  * Packete mit cmdId
  * setAddvertisement <cmdId|zeile1|zeile2|zeile3|zeile4>
  * Escape Sequenz
  * Verifizierung aller externen Daten
  * Checksumme für die Packete
  * ShowDefaultText

### Features ###
  * Scrooling Text (mehr als 4 Zeilen)

## Schild ##
  * Modi: Wagen + Preis
  * Pakete mit cmdId
  * Verifizierung aller externen Daten
  * Trace speichern
  * SendTrace
  * ShowDefaultText
  * Beim hochfahren: DefaultText anzeigen
  * Escape Sequenzen können
  * Checksum implementieren


# Checksum #
  * <sign|cmd|params>
  * <lamp|sign|cmd|params>

  * Checksum über:
  * sign|cmd|params