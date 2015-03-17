# Wagenschild & Preisschild #
## Gemeinsame Funktionen ##
  * string ReceiveMessage()
  * ParseMessage(string)
  * UpdateText(string)
  * Reset(string defaultMessage)

# Wagenschild #
## Zusätzliche Funktionen ##
  * SaveWaypoint()
  * SendWaypoints()



### ReceiveMessage() ###
Wird in eine Endlosschleife ausgeführt, solange keine neuen Preis/Werbeinformationen für das jeweilige Schild gesendet werden. Es wird also permanent in den empfangenen Paketen das ID Feld ausgelesen und mit der eigenen ID verglichen. Bei einer Übereinstimmung wird ParseMessage aufgerufen.

### ParseMessage(string) ###
Setzt aus einzelnen Paketen eine komplette Nachricht zusammen und ruft bei vollständigem Empfang die jeweilige Funktion auf.

### UpdateText(string) ###
Schreibt die Preis/Werbeinformation ins EEPROM, so daß auch bei einem Neustart nach einem Stromausfall die letzte Anzeige wiederhergestellt werden kann.

### Reset(string defaultMessage) ###
Wird der (an der Rückseite versteckte, nur mit einer Stecknadel auslösbare) Resetknopf betätigt, löscht das Schild die letzte Information aus dem EEPROM und zeigt stattdessen eine Defaultnachricht an.

### defaultMessage() ###
Gibt die default Werbung zurück.

### SaveWaypoints() ###
Wenn die Nachricht von einer neuen Lampe ist: Speichern der LampenID und Timestamp im RAM.
Ansonsen: Nichts

### SendWaypoints() ###
  1. Funk aktivieren
  1. Senden und Trace löschen
  1. Funk deaktivieren
  1. Default Werbung anzeigen