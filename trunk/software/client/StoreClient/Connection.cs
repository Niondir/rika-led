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
            catch(Exception ex)
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

        internal void Add(ProductData productData)
        {
            remote.AddProduct(session, productData);
        }

        internal void Add(AdvertisementData advertisementData)
        {
            remote.AddAdvertisement(session, advertisementData);
        }
        #endregion

        internal ProductData[] GetProducts()
        {
            return remote.GetProducts(session);
        }

        internal ProductData[] GetProducts(string regionID)
        {
            return remote.GetProductsByRegion(session, regionID);
        }

        internal void DeleteProduct(int id)
        {
            remote.DeleteProduct(session, new ProductData(new SignData(id, new RegionData("0", "")), "", 0));
        }

        internal void EditProduct(int id, ProductData newValue)
        {
            remote.EditProduct(session,
                                new ProductData(new SignData(id, new RegionData("0", "")), "", 0),
                                newValue);
        }

        internal void DeleteRegion(string id)
        {
            remote.DeleteRegion(session, new RegionData(id, ""));
            
        }

        internal UserData[] GetUsers()
        {
            return remote.GetUsers(session);
        }

        internal RoleData[] GetRoles()
        {
            return remote.GetRoles(session);
        }

        internal void EditRole(RoleData oldRole, RoleData newRole)
        {
            remote.EditRole(session, oldRole, newRole);
            
        }

        internal AdvertisementData[] GetAds()
        {
            AdvertisementData[] ads = new AdvertisementData[4];

            RegionData r = new RegionData("1", "test");

            ads[0] = new AdvertisementData(1, r, "eins", new string[] { "zeile1", "zeile2", "zeile3", "zeile4" }, DateTime.Now, DateTime.Now + new TimeSpan(1,0,0,0), DateTime.Now, DateTime.Now + new TimeSpan(1, 1, 1));
            ads[1] = new AdvertisementData(2, r, "zwei", new string[] { "zeile1", "zeile2", "zeile3", "zeile4" }, DateTime.Now, DateTime.Now + new TimeSpan(1, 0, 0, 0), DateTime.Now, DateTime.Now + new TimeSpan(1, 1, 1));
            ads[2] = new AdvertisementData(3, r, "drei", new string[] { "zeile1", "zeile2", "zeile3", "zeile4" }, DateTime.Now, DateTime.Now + new TimeSpan(1, 0, 0, 0), DateTime.Now, DateTime.Now + new TimeSpan(1, 1, 1));
            ads[3] = new AdvertisementData(4, r, "vier", new string[] { "zeile1", "zeile2", "zeile3", "zeile4" }, DateTime.Now, DateTime.Now + new TimeSpan(1, 0, 0, 0), DateTime.Now, DateTime.Now + new TimeSpan(1, 1, 1));

            //return ads;
            return remote.GetAdvertisement(session);
        }


        internal void EditAd(int p, AdvertisementData advertisementData)
        {
            remote.DeleteAdvertisement(session, new AdvertisementData(p, new RegionData("", ""), "", new string[] { "", "", "", "" }, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now));
            remote.AddAdvertisement(session, advertisementData);
        }

        internal void EditUser(UserData oldUser, UserData newUser)
        {
            remote.DeleteUser(session, oldUser);
            remote.AddUser(session, newUser);
        }

        internal void Add(UserData newUser)
        {
            remote.AddUser(session, newUser);
        }

        internal void EditRegion(RegionData oldData, RegionData newData)
        {
            remote.EditRegion(session, oldData, newData);
        }

        internal void Add(RoleData role)
        {
            remote.AddRole(session, role);
        }

        internal void DeleteRole(RoleData roleData)
        {
            remote.DeleteRole(session, roleData);
        }

        internal void DeleteUser(UserData userData)
        {
            remote.DeleteUser(session, userData);
        }

        internal void DeleteAd(AdvertisementData advertisementData)
        {
            remote.DeleteAdvertisement(session, advertisementData);
        }

        internal TraceData[] GetTraces()
        {
            return remote.GetTraces(session);
        }

        internal TraceData[] GetTraces(DateTime start, DateTime stop)
        {
            
            start = start.Date;
            if (stop != stop.Date)
                stop = stop.Date + TimeSpan.FromDays(1);

            return remote.GetTracesByTimeSpan(session, start, stop);
        }

        internal void ShowID()
        {
            RegionData region = new RegionData();
            region.Id = "ffff";
            region.Name = "";
            remote.ShowSignId(session, region);
        }

        internal void SetLampId(string oldID, string newID)
        {
            remote.SetLampId(session, oldID, newID);
        }
    }
}
