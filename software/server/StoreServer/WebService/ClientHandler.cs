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

        public ClientHandler() : base(new DataManager())
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



            throw new XmlRpcFaultException(1, "Not implemented");
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

            throw new XmlRpcFaultException(1, "Not implemented");
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

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void AddAdvertisement(SessionData session, AdvertisementData value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void AddTrace(SessionData session, TraceData value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void DeleteRole(SessionData session, RoleData value)
        {
            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void DeleteUser(SessionData session, UserData value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void DeleteLamp(SessionData session, LampData value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void DeleteRegion(SessionData session, RegionData value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void DeleteProduct(SessionData session, ProductData value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void DeleteAdvertisement(SessionData session, AdvertisementData value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void DeleteTrace(SessionData session, TraceData value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void EditRole(SessionData session, RoleData oldValue, RoleData newValue)
        {
            throw new XmlRpcFaultException(1, "Not implemented");
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

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void EditProduct(SessionData session, ProductData oldValue, ProductData newValue)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
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
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public ProductData[] GetProducts(SessionData session)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
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
