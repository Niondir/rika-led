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
        void AddUser(Session session, User value);

        [XmlRpcMethod]
        void AddLamp(Session session, Lamp value);

        [XmlRpcMethod]
        void AddRegion(Session session, Region value);

        [XmlRpcMethod]
        void AddProduct(Session session, Product value);

        [XmlRpcMethod]
        void AddSign(Session session, Sign value);

        [XmlRpcMethod]
        void AddAdvertisement(Session session, Advertisement value);

        [XmlRpcMethod]
        void AddTrace(Session session, Trace value);

        #endregion

        #region Delete()

        [XmlRpcMethod]
        void DeleteUser(Session session, User value);

        [XmlRpcMethod]
        void DeleteLamp(Session session, Lamp value);

        [XmlRpcMethod]
        void DeleteRegion(Session session, Region value);

        [XmlRpcMethod]
        void DeleteProduct(Session session, Product value);

        [XmlRpcMethod]
        void DeleteSign(Session session, Sign value);

        [XmlRpcMethod]
        void DeleteAdvertisement(Session session, Advertisement value);

        [XmlRpcMethod]
        void DeleteTrace(Session session, Trace value);

        #endregion

        #region Edit()

        [XmlRpcMethod]
        void EditUser(Session session, User oldValue, User newValue);

        [XmlRpcMethod]
        void EditRegion(Session session, Region oldValue, Region newValue);

        [XmlRpcMethod]
        void EditProduct(Session session, Product oldValue, Product newValue);

        [XmlRpcMethod]
        void EditSign(Session session, Sign oldValue, Sign newValue);
        
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
