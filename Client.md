# Basis #

Windows Forms SDI Anwendung in .NET


---


# Programmablauf #

  1. Main Form öffnen
  1. Wenn Konfigurationen vorhanden, übernehmen und weiter zu **3.**
  1. Einstellungs Dialog öffnen um Serveradressen, Benutzernamen und Kennwort einzugeben
  1. Zum Server verbinden
  1. Controls dem Userlevel anpassen (enable/disable)
  1. Daten vom Server laden (Regionen, Aktuelle Ads, ...) (<-- vllt auch erst nach Anfrage)
  1. Daten verändern
  1. Schließen

# Klassen #
  * Graphische Oberfläche und UI
    * `FormMain : Form` (Kontainer für komplettes UI (Ctrls), bis auf den Settings Dialog)
    * `FormSettings : Form` (Einstellungen für Serveradresse, Benutzer etc)
    * `CtrlZonesLamps : UserControl` (Zeigt eingetragene Zonen an und bietet Funktionalität zum manipulieren)
    * `CtrlShields : UserControl`
  * DataTable
  * Config
  * Communication
  * _Packet_

# DLL'S #
[DataTypes](DataTypes.md).dll