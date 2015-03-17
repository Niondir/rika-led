# DataTypes.dll #

  * Exception bearbeitung
  * Interfaces zur Kommunikation via XML-RPC
  * Strukturen für die Datenübertragung

## Structs ##

### Session ###

### User ###

### Lamp ###

### Region ###

### Product ###

### Sign ###

### Trace ###

### Advertisement ###

## Interfaces ##

## Add(Session session, value) ##
  * **Hinzufügen von Daten**
  * Speichern der Daten in der Datenbank
  * Mitteilung an den [RadioManager](RadioManager.md)
  * Wirft Exeption bei Fehlern

## Delete(Session session, value) ##
  * **Entfernen von Daten**
  * Löschen der Daten aus der Datenbank
  * Mitteilung an den [RadioManager](RadioManager.md)

## Edit(Session session, oldValue, newValue) ##
  * **Editieren von Daten**
  * Editiert Daten in der Datenbank
  * Mitteilung an den [RadioManager](RadioManager.md)

## Get...() ##