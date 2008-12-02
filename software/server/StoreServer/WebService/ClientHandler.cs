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

        private void ValidateRequest(Session session, AccessFlags accessFlags)
        {
            Client client = Program.UserManager.GetClient(session, this.RemoteEndPoint);
            throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Keine Berechtigung");
        }

        #region IRemoteFunctions Member

        public Session Login(User user)
        {
            Client client = new Client(this.RemoteEndPoint, user);

            if (client.Authed)
            {
                return client.Session;
            }
            else
            {
                throw new XmlRpcFaultException(101, "Invalid logindata");
            }
        }

        public void Logout(Session session)
        {
            Client client = Program.UserManager.GetClient(session, this.RemoteEndPoint);
            
            if (client != null)
                client.Logout();
        }

        

        public void AddUser(Session session, User value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            

            Program.DataManager.AddUser(value);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void AddLamp(Session session, Lamp value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void AddRegion(Session session, Region value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void AddProduct(Session session, Product value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void AddSign(Session session, Sign value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void AddAdvertisement(Session session, Advertisement value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void AddTrace(Session session, Trace value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void DeleteUser(Session session, User value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void DeleteLamp(Session session, Lamp value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void DeleteRegion(Session session, Region value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void DeleteProduct(Session session, Product value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void DeleteSign(Session session, Sign value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void DeleteAdvertisement(Session session, Advertisement value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void DeleteTrace(Session session, Trace value)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void EditUser(Session session, User oldValue, User newValue)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void EditRegion(Session session, Region oldValue, Region newValue)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void EditProduct(Session session, Product oldValue, Product newValue)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void EditSign(Session session, Sign oldValue, Sign newValue)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public User GetUser(Session session, string loginName)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public Lamp[] GetLamps(Session session)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public Region[] GetRegions(Session session)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public Product[] GetProducts(Session session)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public Sign[] GetSigns(Session session)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public Trace[] GetTraces(Session session, DateTime from, DateTime to)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public Advertisement[] GetAdvertisement(Session session)
        {
            this.ValidateRequest(session, AccessFlags.Authenticated);

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        #endregion
    }
}
