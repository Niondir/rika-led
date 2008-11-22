using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CookComputing.XmlRpc;
using CommunicationAPI.DataTypes;

namespace CommunicationAPI.Interface
{
    [XmlRpcUrl("http://127.0.0.1:11000/")]
    public interface IRemoteFunctions
    {
        [XmlRpcMethod]
        Session Login(User user);
        
        [XmlRpcMethod]
        void Logout();

        #region Add()

        [XmlRpcMethod]
        void Add(Session session, User value);

        [XmlRpcMethod]
        void Add(Session session, Lamp value);

        [XmlRpcMethod]
        void Add(Session session, Region value);

        [XmlRpcMethod]
        void Add(Session session, Product value);

        [XmlRpcMethod]
        void Add(Session session, Sign value);

        [XmlRpcMethod]
        void Add(Session session, Advertisement value);

        [XmlRpcMethod]
        void Add(Session session, Trace value);

        #endregion

        #region Delete()

        [XmlRpcMethod]
        void Delete(Session session, User value);

        [XmlRpcMethod]
        void Delete(Session session, Lamp value);

        [XmlRpcMethod]
        void Delete(Session session, Region value);

        [XmlRpcMethod]
        void Delete(Session session, Product value);

        [XmlRpcMethod]
        void Delete(Session session, Sign value);

        [XmlRpcMethod]
        void Delete(Session session, Advertisement value);

        [XmlRpcMethod]
        void Delete(Session session, Trace value);

        #endregion

        #region Edit()

        [XmlRpcMethod]
        void Edit(Session session, User oldValue, User newValue);

        [XmlRpcMethod]
        void Edit(Session session, Region oldValue, Region newValue);

        [XmlRpcMethod]
        void Edit(Session session, Product oldValue, Product newValue);

        [XmlRpcMethod]
        void Edit(Session session, Sign oldValue, Sign newValue);
        
        #endregion

        #region Get()

        [XmlRpcMethod]
        User GetUser(string loginName);

        [XmlRpcMethod]
        Lamp[] GetLamps();

        [XmlRpcMethod]
        Region[] GetRegions();

        [XmlRpcMethod]
        Product[] GetProducts();

        [XmlRpcMethod]
        Sign[] GetSigns();

        [XmlRpcMethod]
        Trace[] GetTraces(DateTime from, DateTime to);

        [XmlRpcMethod]
        Advertisement[] GetAdvertisement();
        
        #endregion




    }
}
