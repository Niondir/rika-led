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

        internal ProductData[] GetProducts()
        {
            return remote.GetProducts(session);
        }

        internal void DeleteProduct(int id)
        {
            remote.DeleteProduct(session, new ProductData(new SignData(id, new RegionData(0, "")), "", 0));
        }

        internal void EditProduct(int id, ProductData newValue)
        {
            remote.EditProduct(session,
                                new ProductData(new SignData(id, new RegionData(0, "")), "", 0),
                                newValue);
        }
    }
}
