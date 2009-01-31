using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using CookComputing.XmlRpc;
using CommunicationAPI.Interface;
using CommunicationAPI.DataTypes;
using CommunicationAPI;
using StoreServer.Data;
using StoreServer.Radio;

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
            Client client = new Client(this.RemoteEndPoint, user, this.DataManager.Connection);

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

        public void EditAdvertisement(SessionData session, AdvertisementData oldValue, AdvertisementData newValue)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

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

        public RoleData[] GetRoles(SessionData session)
        {
            Debug.WriteLine("GetRoles()");
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

        public UserData[] GetUsers(SessionData session)
        {
            Debug.WriteLine("GetUsers()");
            this.ValidateRequest(session, AccessFlags.Authenticated);

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

        public ProductData[] GetProductsByRegion(SessionData session, string regionId)
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
            Debug.WriteLine("GetAdvertisement()");
            this.ValidateRequest(session, AccessFlags.Authenticated);

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

        public void ShowSignId(RegionData region)
        {
            try
            {
                Program.RadioManager.Send(new DisplayIdPacket(region.Id));
            }
            catch (Exception ex)
            {
                throw new XmlRpcFaultException((int)ErrorCodes.PacketSendError, ex.Message);
            }
        }

        public void SetLampId(string oldId, string newId)
        {
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

        public System.Security.Cryptography.X509Certificates.X509CertificateCollection ClientCertificates
        {
            get { throw new NotImplementedException(); }
        }

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

        public CookieContainer CookieContainer
        {
            get { throw new NotImplementedException(); }
        }

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

        public WebHeaderCollection Headers
        {
            get { throw new NotImplementedException(); }
        }

        public Guid Id
        {
            get { throw new NotImplementedException(); }
        }

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

        public event XmlRpcRequestEventHandler RequestEvent;

        public CookieCollection ResponseCookies
        {
            get { throw new NotImplementedException(); }
        }

        public event XmlRpcResponseEventHandler ResponseEvent;

        public WebHeaderCollection ResponseHeaders
        {
            get { throw new NotImplementedException(); }
        }

        public string[] SystemListMethods()
        {
            throw new NotImplementedException();
        }

        public string SystemMethodHelp(string MethodName)
        {
            throw new NotImplementedException();
        }

        public object[] SystemMethodSignature(string MethodName)
        {
            throw new NotImplementedException();
        }

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
