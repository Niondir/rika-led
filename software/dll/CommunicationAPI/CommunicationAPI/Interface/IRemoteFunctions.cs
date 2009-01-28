using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CookComputing.XmlRpc;
using CommunicationAPI.DataTypes;

namespace CommunicationAPI.Interface
{
    [XmlRpcUrl("http://127.0.0.1:11000/")]
    public interface IRemoteFunctions : IXmlRpcProxy
    {
        [XmlRpcMethod]
        SessionData Login(UserData user);
        
        [XmlRpcMethod]
        void Logout(SessionData session);

        #region Add()

        [XmlRpcMethod]
        void AddRole(SessionData session, RoleData value);

        [XmlRpcMethod]
        void AddUser(SessionData session, UserData value);

        [XmlRpcMethod]
        void AddRegion(SessionData session, RegionData value);

        [XmlRpcMethod]
        void AddProduct(SessionData session, ProductData value);

        [XmlRpcMethod]
        void AddAdvertisement(SessionData session, AdvertisementData value);

        [XmlRpcMethod]
        void AddTrace(SessionData session, TraceData value);

        #endregion

        #region Delete()

        [XmlRpcMethod]
        void DeleteRole(SessionData session, RoleData value);

        [XmlRpcMethod]
        void DeleteUser(SessionData session, UserData value);

        [XmlRpcMethod]
        void DeleteRegion(SessionData session, RegionData value);

        [XmlRpcMethod]
        void DeleteProduct(SessionData session, ProductData value);

        [XmlRpcMethod]
        void DeleteAdvertisement(SessionData session, AdvertisementData value);

        [XmlRpcMethod]
        void DeleteTrace(SessionData session, TraceData value);

        #endregion

        #region Edit()

        [XmlRpcMethod]
        void EditRole(SessionData session, RoleData oldValue, RoleData newValue);

        [XmlRpcMethod]
        void EditUser(SessionData session, UserData oldValue, UserData newValue);

        [XmlRpcMethod]
        void EditRegion(SessionData session, RegionData oldValue, RegionData newValue);

        [XmlRpcMethod]
        void EditProduct(SessionData session, ProductData oldValue, ProductData newValue);

        #endregion

        #region Get()

        [XmlRpcMethod]
        RoleData[] GetRoles(SessionData session);

        [XmlRpcMethod]
        UserData GetUser(SessionData session, string loginName);

        [XmlRpcMethod]
        UserData[] GetUsers(SessionData session);

        [XmlRpcMethod]
        RegionData[] GetRegions(SessionData session);

        [XmlRpcMethod]
        ProductData[] GetProductsByRegion(SessionData session, string regionId);
        
        [XmlRpcMethod]
        ProductData[] GetProducts(SessionData session);

        [XmlRpcMethod]
        SignData[] GetSigns(SessionData session, RegionData region);

        [XmlRpcMethod]
        TraceData[] GetTraces(SessionData session, DateTime from, DateTime to);

        [XmlRpcMethod]
        AdvertisementData[] GetAdvertisement(SessionData session);
        
        #endregion

#region Misc
        [XmlRpcMethod]
        void ShowSignId(RegionData region);

        [XmlRpcMethod]
        void SetLampId(string oldId, string newId);

#endregion


    }
}
