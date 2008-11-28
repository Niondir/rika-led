using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using CookComputing.XmlRpc;
using CommunicationAPI.Interface;
using CommunicationAPI.DataTypes;
using CommunicationAPI;

namespace StoreServer.WebService
{
    public class ClientHandler : MyXmlRpcListenerService, IRemoteFunctions
    {
        
        #region IRemoteFunctions Member

        public Session Login(User user)
        {
            Session session = Program.UserManager.Login(user, this.RemoteEndPoint);
            
            if (session.Valid)
                return session;
            else
                throw new XmlRpcFaultException(101, "Invalid Login Data");
        }

        public void Logout(Session session)
        {
            Program.UserManager.Logout(this.RemoteEndPoint);

            throw new XmlRpcFaultException((int)ErrorCodes.NotImplemented, "Not implemented");
        }

        public void AddUser(Session session, User value)
        {
            if (!Program.UserManager.CheckAccess(session, AccessFlags.Authenticated))
            {
                throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Keine Berechtigung");
            }

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void AddLamp(Session session, Lamp value)
        {
            if (!Program.UserManager.CheckAccess(session, AccessFlags.Authenticated))
            {
                throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Keine Berechtigung");
            }

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void AddRegion(Session session, Region value)
        {
            if (!Program.UserManager.CheckAccess(session, AccessFlags.Authenticated))
            {
                throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Keine Berechtigung");
            }

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void AddProduct(Session session, Product value)
        {
            if (!Program.UserManager.CheckAccess(session, AccessFlags.Authenticated))
            {
                throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Keine Berechtigung");
            }

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void AddSign(Session session, Sign value)
        {
            if (!Program.UserManager.CheckAccess(session, AccessFlags.Authenticated))
            {
                throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Keine Berechtigung");
            }

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void AddAdvertisement(Session session, Advertisement value)
        {
            if (!Program.UserManager.CheckAccess(session, AccessFlags.Authenticated))
            {
                throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Keine Berechtigung");
            }

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void AddTrace(Session session, Trace value)
        {
            if (!Program.UserManager.CheckAccess(session, AccessFlags.Authenticated))
            {
                throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Keine Berechtigung");
            }

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void DeleteUser(Session session, User value)
        {
            if (!Program.UserManager.CheckAccess(session, AccessFlags.Authenticated))
            {
                throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Keine Berechtigung");
            }

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void DeleteLamp(Session session, Lamp value)
        {
            if (!Program.UserManager.CheckAccess(session, AccessFlags.Authenticated))
            {
                throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Keine Berechtigung");
            }

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void DeleteRegion(Session session, Region value)
        {
            if (!Program.UserManager.CheckAccess(session, AccessFlags.Authenticated))
            {
                throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Keine Berechtigung");
            }

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void DeleteProduct(Session session, Product value)
        {
            if (!Program.UserManager.CheckAccess(session, AccessFlags.Authenticated))
            {
                throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Keine Berechtigung");
            }

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void DeleteSign(Session session, Sign value)
        {
            if (!Program.UserManager.CheckAccess(session, AccessFlags.Authenticated))
            {
                throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Keine Berechtigung");
            }

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void DeleteAdvertisement(Session session, Advertisement value)
        {
            if (!Program.UserManager.CheckAccess(session, AccessFlags.Authenticated))
            {
                throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Keine Berechtigung");
            }

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void DeleteTrace(Session session, Trace value)
        {
            if (!Program.UserManager.CheckAccess(session, AccessFlags.Authenticated))
            {
                throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Keine Berechtigung");
            }

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void EditUser(Session session, User oldValue, User newValue)
        {
            if (!Program.UserManager.CheckAccess(session, AccessFlags.Authenticated))
            {
                throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Keine Berechtigung");
            }

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void EditRegion(Session session, Region oldValue, Region newValue)
        {
            if (!Program.UserManager.CheckAccess(session, AccessFlags.Authenticated))
            {
                throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Keine Berechtigung");
            }

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void EditProduct(Session session, Product oldValue, Product newValue)
        {
            if (!Program.UserManager.CheckAccess(session, AccessFlags.Authenticated))
            {
                throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Keine Berechtigung");
            }

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public void EditSign(Session session, Sign oldValue, Sign newValue)
        {
            if (!Program.UserManager.CheckAccess(session, AccessFlags.Authenticated))
            {
                throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Keine Berechtigung");
            }

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public User GetUser(Session session, string loginName)
        {
            if (!Program.UserManager.CheckAccess(session, AccessFlags.Authenticated))
            {
                throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Keine Berechtigung");
            }

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public Lamp[] GetLamps(Session session)
        {
            if (!Program.UserManager.CheckAccess(session, AccessFlags.Authenticated))
            {
                throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Keine Berechtigung");
            }

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public Region[] GetRegions(Session session)
        {
            if (!Program.UserManager.CheckAccess(session, AccessFlags.Authenticated))
            {
                throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Keine Berechtigung");
            }

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public Product[] GetProducts(Session session)
        {
            if (!Program.UserManager.CheckAccess(session, AccessFlags.Authenticated))
            {
                throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Keine Berechtigung");
            }

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public Sign[] GetSigns(Session session)
        {
            if (!Program.UserManager.CheckAccess(session, AccessFlags.Authenticated))
            {
                throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Keine Berechtigung");
            }

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public Trace[] GetTraces(Session session, DateTime from, DateTime to)
        {
            if (!Program.UserManager.CheckAccess(session, AccessFlags.Authenticated))
            {
                throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Keine Berechtigung");
            }

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        public Advertisement[] GetAdvertisement(Session session)
        {
            if (!Program.UserManager.CheckAccess(session, AccessFlags.Authenticated))
            {
                throw new XmlRpcFaultException((int)ErrorCodes.AccessDenined, "Keine Berechtigung");
            }

            throw new XmlRpcFaultException(1, "Not implemented");
        }

        #endregion
    }
}
