using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using CommunicationAPI;
using CommunicationAPI.DataTypes;
using CommunicationAPI.Interface;
using CookComputing.XmlRpc;
using StoreServer.Data;
using StoreServer.Radio;

namespace StoreServer.WebService
{
    /// <summary>
    /// Handle all clients connected to the server
    /// </summary>
    public class ClientHandler : MyXmlRpcListenerService, IRemoteFunctions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataManager"></param>
        public ClientHandler(DataManager dataManager)
            : base(dataManager)
        {

        }


        /// <summary>
        /// Boolean check if the user has access
        /// </summary>
        /// <param name="session"></param>
        /// <param name="accessFlags"></param>
        /// <returns></returns>
        private bool HasAccess(SessionData session, AccessFlags accessFlags)
        {
            Client client = Program.UserManager.GetClient(session, this.RemoteEndPoint);

            if (client == null)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Login first");
            }
            else if (!client.CheckAccess(session, this.RemoteEndPoint, accessFlags))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Check the accessflag an throw exception if not allowed
        /// </summary>
        /// <param name="session"></param>
        /// <param name="accessFlags"></param>
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

        /// <summary>
        /// Login to the server
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public SessionData Login(UserData user)
        {
            Console.WriteLine("Login()");

            Client client = new Client(this.RemoteEndPoint, user, this.DataManager.Connection);

            if (client.Authed)
            {
                Console.WriteLine("user logged in: " + user.Username);
                return client.Session.Data;
            }
            else
            {
                throw new XmlRpcFaultException((int)ErrorCodes.InvalidLogin, "Invalid logindata");
            }
        }

        /// <summary>
        /// Logout from the server
        /// </summary>
        /// <param name="session"></param>
        public void Logout(SessionData session)
        {
            Console.WriteLine("Logout()");

            Client client = Program.UserManager.GetClient(session, this.RemoteEndPoint);

            if (client != null)
                client.Logout();
        }

        /// <summary>
        /// Add a new role
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        public void AddRole(SessionData session, RoleData value)
        {
            Console.WriteLine("AddRole()");
            this.ValidateRequest(session, AccessFlags.User);

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

        /// <summary>
        /// Add a new user
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        public void AddUser(SessionData session, UserData value)
        {
            Console.WriteLine("AddUser()");
            this.ValidateRequest(session, AccessFlags.User);

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

        /// <summary>
        /// Add a new region
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        public void AddRegion(SessionData session, RegionData value)
        {
            Console.WriteLine("AddRegion()");
            this.ValidateRequest(session, AccessFlags.Regions);

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

        /// <summary>
        /// Add a new product
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        public void AddProduct(SessionData session, ProductData value)
        {
            Console.WriteLine("AddProduct()");
            this.ValidateRequest(session, AccessFlags.Product);

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

        /// <summary>
        /// Add a new advertisement
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        public void AddAdvertisement(SessionData session, AdvertisementData value)
        {
            Console.WriteLine("AddAdvertisement()");
            this.ValidateRequest(session, AccessFlags.Ads);

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

        /// <summary>
        /// Add a new trace
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        public void AddTrace(SessionData session, TraceData value)
        {
            Console.WriteLine("AddTrace()");
            this.ValidateRequest(session, AccessFlags.Traces);

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

        /// <summary>
        /// Delete a role
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        public void DeleteRole(SessionData session, RoleData value)
        {
            Console.WriteLine("DeleteRole()");
            this.ValidateRequest(session, AccessFlags.User);

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

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        public void DeleteUser(SessionData session, UserData value)
        {
            Console.WriteLine("DeleteUser()");
            this.ValidateRequest(session, AccessFlags.User);

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

        /// <summary>
        /// Delete a region
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        public void DeleteRegion(SessionData session, RegionData value)
        {
            Console.WriteLine("DeleteRegion()");
            this.ValidateRequest(session, AccessFlags.Regions);

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

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        public void DeleteProduct(SessionData session, ProductData value)
        {
            Console.WriteLine("DeleteProduct()");
            this.ValidateRequest(session, AccessFlags.Product);

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

        /// <summary>
        /// Delete an advertisement
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        public void DeleteAdvertisement(SessionData session, AdvertisementData value)
        {
            Console.WriteLine("DeleteAdvertisement()");
            this.ValidateRequest(session, AccessFlags.Ads);

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

        /// <summary>
        /// Delete a trace
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        public void DeleteTrace(SessionData session, TraceData value)
        {
            Console.WriteLine("DeleteTrace()");
            this.ValidateRequest(session, AccessFlags.Traces);

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

        /// <summary>
        /// Edit a role
        /// </summary>
        /// <param name="session"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public void EditRole(SessionData session, RoleData oldValue, RoleData newValue)
        {
            Console.WriteLine("EditRole()");
            this.ValidateRequest(session, AccessFlags.User);

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

        /// <summary>
        /// Edit an user
        /// </summary>
        /// <param name="session"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public void EditUser(SessionData session, UserData oldValue, UserData newValue)
        {
            Console.WriteLine("EditUser()");
            this.ValidateRequest(session, AccessFlags.User);

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

        /// <summary>
        /// Edit a region
        /// </summary>
        /// <param name="session"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public void EditRegion(SessionData session, RegionData oldValue, RegionData newValue)
        {
            Console.WriteLine("EditRegion()");
            this.ValidateRequest(session, AccessFlags.Regions);

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

        /// <summary>
        /// Edit a product
        /// </summary>
        /// <param name="session"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public void EditProduct(SessionData session, ProductData oldValue, ProductData newValue)
        {
            Console.WriteLine("EditProduct()");
            this.ValidateRequest(session, AccessFlags.Product);

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

        /// <summary>
        /// Edit an advertisement
        /// </summary>
        /// <param name="session"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public void EditAdvertisement(SessionData session, AdvertisementData oldValue, AdvertisementData newValue)
        {
            Console.WriteLine("EditAdvertisement()");
            this.ValidateRequest(session, AccessFlags.Ads);

            try
            {
                Advertisement ad = new Advertisement(oldValue);
                ad.Update(this.DataManager.Connection, newValue);
            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.DBReadError, ex.Message);
            }
        }

        /// <summary>
        /// Get all roles
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public RoleData[] GetRoles(SessionData session)
        {
            Console.WriteLine("GetRoles()");
            this.ValidateRequest(session, AccessFlags.Authenticated);

            List<RoleData> roles = new List<RoleData>();
            try
            {
                int i = 0;
                foreach (Role r in Role.Load(this.DataManager.Connection))
                {
                    i++;
                    roles.Add(r.Data);
                    Debug.WriteLine("Role name " + i + ": " + r.Name);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new XmlRpcFaultException((int)ErrorCodes.DBReadError, ex.Message);
            }

            return roles.ToArray();
        }

        /// <summary>
        /// Get details from a user
        /// </summary>
        /// <param name="session"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public UserData GetUser(SessionData session, string loginName)
        {
            Console.WriteLine("GetUser()");
            UserData user;
            try
            {
                user = User.Load(this.DataManager.Connection, loginName).Data;

            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.DBReadError, ex.Message);
            }

            return user;
        }

        /// <summary>
        /// Get all Users
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public UserData[] GetUsers(SessionData session)
        {
            Console.WriteLine("GetUsers()");
            this.ValidateRequest(session, AccessFlags.User);

            List<UserData> users = new List<UserData>();
            try
            {
                int i = 0;
                foreach (User u in User.Load(this.DataManager.Connection))
                {
                    i++;
                    users.Add(u.Data);
                    Debug.WriteLine("User name " + i + ": " + u.Username);
                }

            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.DBReadError, ex.Message);
            }

            return users.ToArray();
        }

        /// <summary>
        /// Get all regions
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public RegionData[] GetRegions(SessionData session)
        {
            Console.WriteLine("GetRegions()");


            if (!(HasAccess(session, AccessFlags.Regions) ||
            HasAccess(session, AccessFlags.Product) ||
            HasAccess(session, AccessFlags.Ads)))
            {
                throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Keine Berechtigung");
            }

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

        /// <summary>
        /// Gt all products
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public ProductData[] GetProducts(SessionData session)
        {
            Console.WriteLine("GetProducts()");
            this.ValidateRequest(session, AccessFlags.Product);

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

        /// <summary>
        /// Get all products in a region
        /// </summary>
        /// <param name="session"></param>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public ProductData[] GetProductsByRegion(SessionData session, string regionId)
        {
            Console.WriteLine("GetProductsByRegion()");
            this.ValidateRequest(session, AccessFlags.Product);

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

        /// <summary>
        /// Get all traces betewwn from an to
        /// </summary>
        /// <param name="session"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public TraceData[] GetTracesByTimeSpan(SessionData session, DateTime from, DateTime to)
        {
            Console.WriteLine("GetTraces()");
            this.ValidateRequest(session, AccessFlags.Traces);

            List<TraceData> traces = new List<TraceData>();
            //try
            //{
                int i = 0;
                foreach (Trace t in Trace.Load(this.DataManager.Connection, from, to))
                {
                    i++;
                    traces.Add(t.Data);
                }
            //}
            /*catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.DBReadError, ex.Message);
            }*/

            return traces.ToArray();
        }

        /// <summary>
        /// Get all traces
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public TraceData[] GetTraces(SessionData session)
        {
            Console.WriteLine("GetTraces()");
            this.ValidateRequest(session, AccessFlags.Traces);

            List<TraceData> traces = new List<TraceData>();
            try
            {
                int i = 0;
                foreach (Trace t in Trace.Load(this.DataManager.Connection))
                {
                    i++;
                    traces.Add(t.Data);
                }
            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.DBReadError, ex.Message);
            }

            return traces.ToArray();
        }

        /// <summary>
        /// Get all ads
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public AdvertisementData[] GetAdvertisement(SessionData session)
        {
            Console.WriteLine("GetAdvertisement()");
            this.ValidateRequest(session, AccessFlags.Ads);

            List<AdvertisementData> ads = new List<AdvertisementData>();
            try
            {
                int i = 0;
                foreach (Advertisement ad in Advertisement.Load(this.DataManager.Connection))
                {
                    i++;
                    ads.Add(ad.Data);
                    Debug.WriteLine("Ad name " + i + ": " + ad.Name);
                }
            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.DBReadError, ex.Message);
            }

            return ads.ToArray();
        }

        /// <summary>
        /// Send a show sign id packet
        /// </summary>
        /// <param name="session"></param>
        /// <param name="region"></param>
        public void ShowSignId(SessionData session, RegionData region)
        {
            Console.WriteLine("ShowSignId()");
            this.ValidateRequest(session, AccessFlags.Network);

            try
            {
                Program.RadioManager.Send(new DisplayIdPacket(region.Id));
            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.PacketSendError, ex.Message);
            }
        }

        /// <summary>
        /// Change the lamp id
        /// </summary>
        /// <param name="session"></param>
        /// <param name="oldId"></param>
        /// <param name="newId"></param>
        public void SetLampId(SessionData session, string oldId, string newId)
        {
            Console.WriteLine("SetLampId()");
            this.ValidateRequest(session, AccessFlags.Network);

            try
            {
                Program.RadioManager.Send(new SetLampIdPacket(oldId, newId));
            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.PacketSendError, ex.Message);
            }
        }

        #endregion

        #region IXmlRpcProxy Member

        /// <summary>
        /// Have to be implemented, but not used
        /// </summary>
        public System.Security.Cryptography.X509Certificates.X509CertificateCollection ClientCertificates
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Have to be implemented, but not used
        /// </summary>
        public string ConnectionGroupName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Have to be implemented, but not used
        /// </summary>
        public CookieContainer CookieContainer
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Have to be implemented, but not used
        /// </summary>
        public ICredentials Credentials
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Have to be implemented, but not used
        /// </summary>
        public bool EnableCompression
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Have to be implemented, but not used
        /// </summary>
        public bool Expect100Continue
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Have to be implemented, but not used
        /// </summary>
        public WebHeaderCollection Headers
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Have to be implemented, but not used
        /// </summary>
        public Guid Id
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Have to be implemented, but not used
        /// </summary>
        public int Indentation
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Have to be implemented, but not used
        /// </summary>
        public bool KeepAlive
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Have to be implemented, but not used
        /// </summary>
        public XmlRpcNonStandard NonStandard
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Have to be implemented, but not used
        /// </summary>
        public bool PreAuthenticate
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Have to be implemented, but not used
        /// </summary>
        public Version ProtocolVersion
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Have to be implemented, but not used
        /// </summary>
        public IWebProxy Proxy
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Must be implemeted from MyXmlRpcListenerService
        /// </summary>
        public event XmlRpcRequestEventHandler RequestEvent;

        /// <summary>
        /// Have to be implemented, but not used
        /// </summary>
        public CookieCollection ResponseCookies
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Must be implemeted from MyXmlRpcListenerService
        /// </summary>
        public event XmlRpcResponseEventHandler ResponseEvent;

        /// <summary>
        /// Have to be implemented, but not used
        /// </summary>
        public WebHeaderCollection ResponseHeaders
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Have to be implemented, but not used
        /// </summary>
        public string[] SystemListMethods()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Have to be implemented, but not used
        /// </summary>
        public string SystemMethodHelp(string MethodName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Have to be implemented, but not used
        /// </summary>
        public object[] SystemMethodSignature(string MethodName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Have to be implemented, but not used
        /// </summary>
        public int Timeout
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Have to be implemented, but not used
        /// </summary>
        public string Url
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Have to be implemented, but not used
        /// </summary>
        public bool UseEmptyParamsTag
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Have to be implemented, but not used
        /// </summary>
        public bool UseIndentation
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Have to be implemented, but not used
        /// </summary>
        public bool UseIntTag
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Have to be implemented, but not used
        /// </summary>
        public bool UseStringTag
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Have to be implemented, but not used
        /// </summary>
        public string UserAgent
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Have to be implemented, but not used
        /// </summary>
        public Encoding XmlEncoding
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Have to be implemented, but not used
        /// </summary>
        public string XmlRpcMethod
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion


    }
}
