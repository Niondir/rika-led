using System;
using System.Collections.Generic;
using System.Text;
using CookComputing.XmlRpc;
using CommunicationAPI.Interface;
using CommunicationAPI.DataTypes;
using CommunicationAPI;

namespace StoreServer.WebService
{
    public class ClientHandler : XmlRpcListenerService, IRemoteFunctions
    {

        #region IRemoteFunctions Members

        // Need the tag?
        [XmlRpcMethod]
        public Session Login(User user)
        {
            Session session = new Session(user);
            LoginEventArgs ea = new LoginEventArgs(session);
            WebServiceEvents.InvokeLogin(ea);

            if (ea.Session.Valid)
                return ea.Session;
            else
                throw new XmlRpcFaultException(101, "Invalid Login Data");
        }

        public void Logout()
        {
            throw new XmlRpcFaultException(1, "Not Implemented");
        }

        public void Add(Session session, User value)
        {
            throw new XmlRpcFaultException(1, "Not Implemented");
        }

        public void Add(Session session, Lamp value)
        {
            throw new XmlRpcFaultException(1, "Not Implemented");
        }

        public void Add(Session session, Region value)
        {
            throw new XmlRpcFaultException(1, "Not Implemented");
        }

        public void Add(Session session, Product value)
        {
            throw new XmlRpcFaultException(1, "Not Implemented");
        }

        public void Add(Session session, Sign value)
        {
            throw new XmlRpcFaultException(1, "Not Implemented");
        }

        public void Add(Session session, Advertisement value)
        {
            throw new XmlRpcFaultException(1, "Not Implemented");
        }

        public void Add(Session session, Trace value)
        {
            throw new XmlRpcFaultException(1, "Not Implemented");
        }

        public void Delete(Session session, User value)
        {
            throw new XmlRpcFaultException(1, "Not Implemented");
        }

        public void Delete(Session session, Lamp value)
        {
            throw new XmlRpcFaultException(1, "Not Implemented");
        }

        public void Delete(Session session, Region value)
        {
            throw new XmlRpcFaultException(1, "Not Implemented");
        }

        public void Delete(Session session, Product value)
        {
            throw new XmlRpcFaultException(1, "Not Implemented");
        }

        public void Delete(Session session, Sign value)
        {
            throw new XmlRpcFaultException(1, "Not Implemented");
        }

        public void Delete(Session session, Advertisement value)
        {
            throw new XmlRpcFaultException(1, "Not Implemented");
        }

        public void Delete(Session session, Trace value)
        {
            throw new XmlRpcFaultException(1, "Not Implemented");
        }

        public void Edit(Session session, User oldValue, User newValue)
        {
            throw new XmlRpcFaultException(1, "Not Implemented");
        }

        public void Edit(Session session, Region oldValue, Region newValue)
        {
            throw new XmlRpcFaultException(1, "Not Implemented");
        }

        public void Edit(Session session, Product oldValue, Product newValue)
        {
            throw new XmlRpcFaultException(1, "Not Implemented");
        }

        public void Edit(Session session, Sign oldValue, Sign newValue)
        {
            throw new XmlRpcFaultException(1, "Not Implemented");
        }

        public User GetUser(string loginName)
        {
            throw new XmlRpcFaultException(1, "Not Implemented");
        }

        public Lamp[] GetLamps()
        {
            throw new XmlRpcFaultException(1, "Not Implemented");
        }

        public Region[] GetRegions()
        {
            throw new XmlRpcFaultException(1, "Not Implemented");
        }

        public Product[] GetProducts()
        {
            throw new XmlRpcFaultException(1, "Not Implemented");
        }

        public Sign[] GetSigns()
        {
            throw new XmlRpcFaultException(1, "Not Implemented");
        }

        public Trace[] GetTraces(DateTime from, DateTime to)
        {
            throw new XmlRpcFaultException(1, "Not Implemented");
        }

        public Advertisement[] GetAdvertisement()
        {
            throw new XmlRpcFaultException(1, "Not Implemented");
        }

        #endregion
    }
}
