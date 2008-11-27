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


        #region IRemoteFunctions Member

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
            throw new XmlRpcFaultException((int)ErrorCodes.NotImplemented, "Not implemented");
        }

        public void AddUser(Session session, User value)
        {
            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void AddLamp(Session session, Lamp value)
        {
            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void AddRegion(Session session, Region value)
        {
            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void AddProduct(Session session, Product value)
        {
            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void AddSign(Session session, Sign value)
        {
            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void AddAdvertisement(Session session, Advertisement value)
        {
            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void AddTrace(Session session, Trace value)
        {
            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void DeleteUser(Session session, User value)
        {
            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void DeleteLamp(Session session, Lamp value)
        {
            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void DeleteRegion(Session session, Region value)
        {
            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void DeleteProduct(Session session, Product value)
        {
            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void DeleteSign(Session session, Sign value)
        {
            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void DeleteAdvertisement(Session session, Advertisement value)
        {
            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void DeleteTrace(Session session, Trace value)
        {
            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void EditUser(Session session, User oldValue, User newValue)
        {
            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void EditRegion(Session session, Region oldValue, Region newValue)
        {
            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void EditProduct(Session session, Product oldValue, Product newValue)
        {
            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void EditSign(Session session, Sign oldValue, Sign newValue)
        {
            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public User GetUser(string loginName)
        {
            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public Lamp[] GetLamps()
        {
            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public Region[] GetRegions()
        {
            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public Product[] GetProducts()
        {
            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public Sign[] GetSigns()
        {
            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public Trace[] GetTraces(DateTime from, DateTime to)
        {
            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public Advertisement[] GetAdvertisement()
        {
            throw new XmlRpcFaultException(1, "Not implemented");
        }

        #endregion
    }
}
