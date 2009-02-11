using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.Interface;
using CommunicationAPI.DataTypes;
using CookComputing.XmlRpc;


namespace StoreClient
{
    /// <summary>
    /// EventArgs für die Änderung des Verbindungsstatuses
    /// Insbesondere kann durch Connected festegestellt werden, wie der aktuelle Verbindungsstatus ist
    /// </summary>
    public class ConnectionChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Zeigt den aktuellen Verbindungsstatus an
        /// </summary>
        public bool Connected { get; set; }

        /// <summary>
        /// Erstellt eine ConnectionChangedEventArgs mit aktuellem Verbinsungsstatus
        /// </summary>
        /// <param name="connected">aktueller Verbindungsstatus</param>
        public ConnectionChangedEventArgs(bool connected)
        {
            Connected = connected;
        }

    }

    /// <summary>
    /// Abstraktion des Verbinsungsinterface. Ermöglicht die Kommunikation mit dem MyStore Server
    /// Die Singleton Implementierung ermöglicht die Instanziierung nur durch die statische GetInstance() Methode
    /// </summary>
    class Connection
    {
        
        // Instanz des xml-rpc Interfaces
        private IRemoteFunctions remote;
        private SessionData session;
        static public UserData user;
        static private Connection con;

        /// <summary>
        /// Bietet eine Instanz einer Verbindung zum Server.
        /// Die Instanzen sind auf eine einzige Verbindung reduziert, da jeweils nur eine Verwaltet werden kann
        /// </summary>
        /// <returns>Verbingsinstanz</returns>
        static public Connection GetInstance()
        {
            if (con == null)
                con = new Connection();
            return con;
        }

        /// <summary>
        /// Wird gefeuert, sobald sich der Status der Verbindung geändert hat.
        /// Also nach erfolgreichem Aufbau und Abbau der Verbindungen
        /// </summary>
        public event EventHandler<ConnectionChangedEventArgs> LoginChanged;

        private Connection()
        {
            remote = XmlRpcProxyGen.Create<IRemoteFunctions>();
        }

        /// <summary>
        /// Einstellungsmöglichkeit der Server Adresse.
        /// Für eine erfolgreiche Änderung muss der Login neue durchgeführt werden
        /// </summary>
        public string ServerURL
        {
            set
            {
                remote.Url = value;
            }
            get
            {
                return remote.Url;
            }
        }
        
        #region ADD methods
        /// <summary>
        /// Veranlasst den Server, eine neue Region anzulegen.
        /// Wirft xml-rpc Exception
        /// </summary>
        /// <param name="regionData">Die zu erstellende Region</param>
        internal void Add(RegionData regionData)
        {
            remote.AddRegion(session, regionData);
        }

        /// <summary>
        /// Veranlasst den Server ein neues Produkt anzulegen
        /// </summary>
        /// <param name="productData">Das zu erstellende Produkt</param>
        internal void Add(ProductData productData)
        {
            remote.AddProduct(session, productData);
        }

        /// <summary>
        /// Veranlasst den Server eine neue Werbung anzulegen
        /// </summary>
        /// <param name="productData">Die zu erstellende Werbung</param>
        internal void Add(AdvertisementData advertisementData)
        {
            remote.AddAdvertisement(session, advertisementData);
        }

        /// <summary>
        /// Veranlasst den Server, einen neuen Benutzer in die Datenbank aufzunehmen
        /// </summary>
        /// <param name="newUser">Benutzerdaten</param>
        internal void Add(UserData newUser)
        {
            remote.AddUser(session, newUser);
        }

        /// <summary>
        /// Veranlasst den Server, eine neue Benutzergruppe anzulegen.
        /// </summary>
        /// <param name="role">Neue Benutzergruppe</param>
        internal void Add(RoleData role)
        {
            remote.AddRole(session, role);
        }
        #endregion

        #region GET methods

        /// <summary>
        /// Holt die Liste aller Regionen vom Server und gibt diese zurück
        /// </summary>
        /// <returns>Liste aller Regionen auf dem Server</returns>
        internal RegionData[] GetRegions()
        {
            try
            {
                RegionData[] regions = remote.GetRegions(session);
                return regions;
            }
            catch (Exception ex)
            {
                FormMain.HandleException(ex);
                return null;
            }
        }

        /// <summary>
        /// Gibt alle im Server befindlichen Produkte zurück
        /// </summary>
        /// <returns>Alle Produkte</returns>
        internal ProductData[] GetProducts()
        {
            return remote.GetProducts(session);
        }

        /// <summary>
        /// Gibt alle Produkte zurück, die einer bestimmten Region angehören
        /// </summary>
        /// <param name="regionID">Region, nach der gefiltert werden soll</param>
        /// <returns>Alle Produkte der Gruppe regionID</returns>
        internal ProductData[] GetProducts(string regionID)
        {
            return remote.GetProductsByRegion(session, regionID);
        }

        /// <summary>
        /// Gibt alle Benutzer zurück, die auf dem Server registriert sind
        /// </summary>
        /// <returns>Liste der Benutzer</returns>
        internal UserData[] GetUsers()
        {
            return remote.GetUsers(session);
        }

        /// <summary>
        /// Gibt alle Benutzergruppen zurück, die auf dem Server registriert sind
        /// </summary>
        /// <returns>Liste der Benutzergruppen</returns>
        internal RoleData[] GetRoles()
        {
            return remote.GetRoles(session);
        }

        /// <summary>
        /// Gibt alle Werbeeinträge zurück, die auf dem Server registriert sind
        /// </summary>
        /// <returns>Liste der Werbeeinträge</returns>
        internal AdvertisementData[] GetAds()
        {
            return remote.GetAdvertisement(session);
        }

        /// <summary>
        /// Gibt alle Kundenlaufwege zurück, die innerhalb eines Zeitfensters aufgenommen wurden
        /// </summary>
        /// <param name="start">Anfangszeit der Aufnahmen</param>
        /// <param name="stop">Endzeit der Aufnahmen</param>
        /// <returns>Liste Kundenlaufwege</returns>
        internal TraceData[] GetTraces(DateTime start, DateTime stop)
        {

            start = start.Date;
            if (stop != stop.Date)
                stop = stop.Date + TimeSpan.FromDays(1);

            return remote.GetTracesByTimeSpan(session, start, stop);
        }

        /// <summary>
        /// Gibt alle Kundenlaufwege zurück, die auf dem Server registriert sind
        /// </summary>
        /// <returns>Liste der Laufwege</returns>
        internal TraceData[] GetTraces()
        {
            return remote.GetTraces(session);
        }
        #endregion

        #region DELETE methods
        /// <summary>
        /// Entfernt ein Produkt aus der Datenbank
        /// </summary>
        /// <param name="id">Produkt ID des zu löschenden Produkts</param>
        internal void DeleteProduct(int id)
        {
            remote.DeleteProduct(session, new ProductData(new SignData(id, new RegionData("0", "")), "", 0));
        }

        /// <summary>
        /// Entfernt eine Region aus der Datenbank
        /// Achtung: Alle beinhaltenden Produkte werden mitgelöscht
        /// </summary>
        /// <param name="id">ID der Region</param>
        internal void DeleteRegion(string id)
        {
            remote.DeleteRegion(session, new RegionData(id, ""));

        }

        /// <summary>
        /// Entfernt eine Benutzergruppe vom Server
        /// </summary>
        /// <param name="roleData">Benutzergruppe, die entfernt werden soll</param>
        internal void DeleteRole(RoleData roleData)
        {
            remote.DeleteRole(session, roleData);
        }

        /// <summary>
        /// Entfernt einen bestehenden Benutzer vom Server
        /// </summary>
        /// <param name="userData">Benutzer, der entfernt werden soll</param>
        internal void DeleteUser(UserData userData)
        {
            remote.DeleteUser(session, userData);
        }

        /// <summary>
        /// Entfernt einen Wertbeeintrag vom Server
        /// </summary>
        /// <param name="advertisementData">Werbeeintrag, dediziert durch die Identifikationsnummer</param>
        internal void DeleteAd(AdvertisementData advertisementData)
        {
            remote.DeleteAdvertisement(session, advertisementData);
        }
        #endregion

        #region EDIT methods

        /// <summary>
        /// Weist einem Produkt neue Parameter zu.
        /// Alle alten Parameter werden überschrieben
        /// </summary>
        /// <param name="id">Produkt ID des zu ändernden Produkts</param>
        /// <param name="newValue">neue Attribute</param>
        internal void EditProduct(int id, ProductData newValue)
        {
            remote.EditProduct(session,
                                new ProductData(new SignData(id, new RegionData("0", "")), "", 0),
                                newValue);
        }

        /// <summary>
        /// Weist eine Benutzergruppe neue Attribute zu
        /// Alle alten Attribute werden überschrieben
        /// </summary>
        /// <param name="oldRole">Die zu ändernde Benutzergruppe. Nur der Name muss für eine Bearbeitung übereinstimmtn</param>
        /// <param name="newRole">Neue Daten, die der Rolle zugewiesen werden</param>
        internal void EditRole(RoleData oldRole, RoleData newRole)
        {
            remote.EditRole(session, oldRole, newRole);
        }

        /// <summary>
        /// Bearbeitet einen bestehenden Werbeeintrag auf dem Server
        /// </summary>
        /// <param name="p">Identifikationsnummer des Werbeeintrags</param>
        /// <param name="advertisementData">neu Parametriesierte Werbung</param>
        internal void EditAd(int adID, AdvertisementData advertisementData)
        {
            remote.DeleteAdvertisement(session, new AdvertisementData(adID, new RegionData("", ""), "", new string[] { "", "", "", "" }, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now));
            remote.AddAdvertisement(session, advertisementData);
        }

        /// <summary>
        /// Weist einem vorhandenen Benutzer neue Parameter zu
        /// </summary>
        /// <param name="oldUser">Benutzer, der geändert werden soll. Nur der Name ist entscheidend</param>
        /// <param name="newUser">Neue Parameter des Benutzer</param>
        internal void EditUser(UserData oldUser, UserData newUser)
        {
            remote.DeleteUser(session, oldUser);
            remote.AddUser(session, newUser);
        }

        /// <summary>
        /// Weist einer bestehenden Region neue Attribute zu
        /// Die alten Attribute werden alle überschrieben
        /// </summary>
        /// <param name="oldData">Die zu ändernde Region. Nur die Identifikationsnummer ist entscheidend</param>
        /// <param name="newData">Neue Daten zu der Region</param>
        internal void EditRegion(RegionData oldData, RegionData newData)
        {
            remote.EditRegion(session, oldData, newData);
        }
        #endregion

        #region MISC
        /// <summary>
        /// Meldet sich als Client mit Benutzernamen und Passwort bei dem Server an.
        /// </summary>
        /// <param name="username">Benutzername, der Angemeldet werden soll</param>
        /// <param name="password">Passwort zu dem Benutzernamen</param>
        internal void Login(string username, string password)
        {
            user = new UserData(username, password);
            try
            {
                session = remote.Login(user);
                user = remote.GetUser(session, username);
                if (LoginChanged != null)
                    LoginChanged(this, new ConnectionChangedEventArgs(true));
            }
            catch (Exception ex)
            {
                FormMain.HandleException(ex);
            }
        }

        /// <summary>
        /// Meldet den aktuellen Benutzer wieder ab
        /// </summary>
        internal void Logout()
        {
            remote.Logout(session);
        }

        /// <summary>
        /// Veranlasst den Server, das Show ID Commando auszusenden. Hierbei zeigen alle erreichbaren Schilder auf dem Display ihre ID an.
        /// </summary>
        internal void ShowID()
        {
            RegionData region = new RegionData();
            region.Id = "ffff";
            region.Name = "";
            remote.ShowSignId(session, region);
        }

        /// <summary>
        /// Weist einem Lampencontroller
        /// </summary>
        /// <param name="oldID">aktuelle ID des Lampencontrollers</param>
        /// <param name="newID">neue ID des Lampencontrollers</param>
        internal void SetLampId(string oldID, string newID)
        {
            remote.SetLampId(session, oldID, newID);
        }
        #endregion
    }
}
