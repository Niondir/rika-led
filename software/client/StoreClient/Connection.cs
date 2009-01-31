using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.Interface;
using CommunicationAPI.DataTypes;
using CookComputing.XmlRpc;

namespace StoreClient
{
    public class ConnectionChangedEventArgs : EventArgs
    {
        public bool Connected { get; set; }
        public ConnectionChangedEventArgs(bool connected)
        {
            Connected = connected;
        }

    }
    class Connection
    {
        private IRemoteFunctions remote;
        private SessionData session;
        static public UserData user;
        static private Connection con;
        
        public event EventHandler<ConnectionChangedEventArgs> LoginChanged;

        private Connection()
        {
            remote = XmlRpcProxyGen.Create<IRemoteFunctions>();
        }

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

        static public Connection GetInstance()
        {
            if (con == null)
                con = new Connection();
            return con;
        }

        internal void Login(string username, string password)
        {
            user = new UserData(username, password);
            try
            {
                session = remote.Login(user);
                if (LoginChanged != null)
                    LoginChanged(this, new ConnectionChangedEventArgs(true));
            }
            catch(Exception ex)
            {
                FormMain.HandleException(ex);
            }
        }


        internal void Logout()
        {
            remote.Logout(session);
        }

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
    }
}
