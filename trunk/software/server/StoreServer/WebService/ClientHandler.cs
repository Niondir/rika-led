using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using CookComputing.XmlRpc;
using CommunicationAPI.Interface;
using CommunicationAPI.DataTypes;
using CommunicationAPI;
using StoreServer.Data;

namespace StoreServer.WebService
{
    public class ClientHandler : MyXmlRpcListenerService, IRemoteFunctions
    {

        public ClientHandler(DataManager dataManager)
            : base(dataManager)
        {

        }

        private void ValidateRequest(SessionData session, AccessFlags accessFlags)
        {
            Client client = Program.UserManager.GetClient(session, this.RemoteEndPoint);

            if (client == null)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Login first");
            }
            else if (!client.CheckAccess(session, this.RemoteEndPoint, accessFlags))
            {
                throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Keine Berechtigung");
            }
        }

        #region IRemoteFunctions Member

        public SessionData Login(UserData user)
        {
            Client client = new Client(this.RemoteEndPoint, user);

            if (client.Authed)
            {
                Debug.WriteLine("user logged in: " + user.Username);
                return client.Session.Data;
            }
            else
            {
                throw new XmlRpcFaultException((int)ErrorCodes.InvalidLogin, "Invalid logindata");
            }
        }

        public void Logout(SessionData session)
        {
            Client client = Program.UserManager.GetClient(session, this.RemoteEndPoint);

            if (client != null)
                client.Logout();
        }

        public void AddRole(SessionData session, RoleData value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            Role role = new Role(value);
            try
            {
                role.Save(this.DataManager.Connection);
            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.DBWriteError, ex.Message);
            }
        }

        public void AddUser(SessionData session, UserData value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            User acc = new User(value);
            try
            {
                acc.Save(this.DataManager.Connection);
            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.DBWriteError, ex.Message);
            }
        }

        public void AddLamp(SessionData session, LampData value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            Lamp lamp = new Lamp(value);

            try
            {
                lamp.Save(this.DataManager.Connection);
            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.DBWriteError, ex.Message);
            }
        }

        public void AddRegion(SessionData session, RegionData value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            Region region = new Region(value);

            try
            {
                region.Save(this.DataManager.Connection);
            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.DBWriteError, ex.Message);
            }
        }

        public void AddProduct(SessionData session, ProductData value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            Product product = new Product(value);

            try
            {
                product.Save(this.DataManager.Connection);
            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.DBWriteError, ex.Message);
            }
        }

        public void AddAdvertisement(SessionData session, AdvertisementData value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            Advertisement advertisement = new Advertisement(value);

            try
            {
                advertisement.Save(this.DataManager.Connection);
            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.DBWriteError, ex.Message);
            }
        }

        public void AddTrace(SessionData session, TraceData value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            Trace trace = new Trace(value);

            try
            {
                trace.Save(this.DataManager.Connection);
            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.DBWriteError, ex.Message);
            }
        }

        public void DeleteRole(SessionData session, RoleData value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            Role role = new Role(value);

            try
            {
                role.Delete(this.DataManager.Connection);
            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.DBWriteError, ex.Message);
            }
        }

        public void DeleteUser(SessionData session, UserData value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            User user = new User(value);

            try
            {
                user.Delete(this.DataManager.Connection);
            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.DBWriteError, ex.Message);
            }
        }

        public void DeleteLamp(SessionData session, LampData value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            Lamp lamp = new Lamp(value);

            try
            {
                lamp.Delete(this.DataManager.Connection);
            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.DBWriteError, ex.Message);
            }
        }

        public void DeleteRegion(SessionData session, RegionData value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            Region region = new Region(value);

            try
            {
                region.Delete(this.DataManager.Connection);
            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.DBWriteError, ex.Message);
            }
        }

        public void DeleteProduct(SessionData session, ProductData value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            Product product = new Product(value);

            try
            {
                product.Delete(this.DataManager.Connection);
            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.DBWriteError, ex.Message);
            }
        }

        public void DeleteAdvertisement(SessionData session, AdvertisementData value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            Advertisement advertisement = new Advertisement(value);

            try
            {
                advertisement.Delete(this.DataManager.Connection);
            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.DBWriteError, ex.Message);
            }
        }

        public void DeleteTrace(SessionData session, TraceData value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            Trace trace = new Trace(value);

            try
            {
                trace.Delete(this.DataManager.Connection);
            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.DBWriteError, ex.Message);
            }
        }

        public void EditRole(SessionData session, RoleData oldValue, RoleData newValue)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            try
            {
                Role role = new Role(oldValue);
                role.Update(this.DataManager.Connection, newValue);
            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.DBReadError, ex.Message);
            }
        }

        public void EditUser(SessionData session, UserData oldValue, UserData newValue)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            try
            {
                User user = new User(oldValue);
                user.Update(this.DataManager.Connection, newValue);
            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.DBReadError, ex.Message);
            }
        }

        public void EditRegion(SessionData session, RegionData oldValue, RegionData newValue)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            try
            {
                Region region = new Region(oldValue);
                region.Update(this.DataManager.Connection, newValue);
            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.DBReadError, ex.Message);
            }
        }

        public void EditProduct(SessionData session, ProductData oldValue, ProductData newValue)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            try
            {
                Product product = new Product(oldValue);
                product.Update(this.DataManager.Connection, newValue);
            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.DBReadError, ex.Message);
            }
        }

        public RoleData GetRole(SessionData session, string roleName)
        {
            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public UserData GetUser(SessionData session, string loginName)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            try
            {
                User user = new User(loginName, this.DataManager.Connection);
                return user.Data;
            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.DBReadError, ex.Message);
            }
        }

        public LampData[] GetLamps(SessionData session)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public RegionData[] GetRegions(SessionData session)
        {
            Debug.WriteLine("GetRegions()");
            this.ValidateRequest(session, AccessFlags.Authenticated);
            RegionData[] regions;
            try
            {
                regions = Region.Load(this.DataManager.Connection);

                for (int i = 0; i < regions.Length; i++)
                {
                    Debug.WriteLine("Region name " + i + ": " + regions[i].Name);
                }
            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.DBReadError, ex.Message);
            }

            return regions;
        }

        public ProductData[] GetProducts(SessionData session)
        {
            Debug.WriteLine("GetProducts()");
            this.ValidateRequest(session, AccessFlags.Authenticated);

            List<ProductData> products = new List<ProductData>();
            try
            {
                int i = 0;
                foreach (Product p in Product.Load(this.DataManager.Connection))
                {
                    i++;
                    products.Add(p.Data);
                    Debug.WriteLine("Produkt name " + i + ": " + p.Name);
                }

            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.DBReadError, ex.Message);
            }

            return products.ToArray();
        }

        public ProductData[] GetProductsByRegion(SessionData session, int regionId)
        {
            Debug.WriteLine("GetProductsByRegion()");
            this.ValidateRequest(session, AccessFlags.Authenticated);

            List<ProductData> products = new List<ProductData>();
            try
            {
                int i = 0;
                foreach (Product p in Product.Load(this.DataManager.Connection, regionId))
                {
                    i++;
                    products.Add(p.Data);
                    Debug.WriteLine("Produkt name " + i + ": " + p.Name);
                }

            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.DBReadError, ex.Message);
            }

            return products.ToArray();
        }

        public SignData[] GetSigns(SessionData session, RegionData region)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public TraceData[] GetTraces(SessionData session, DateTime from, DateTime to)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public AdvertisementData[] GetAdvertisement(SessionData session)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        #endregion
    }
}
