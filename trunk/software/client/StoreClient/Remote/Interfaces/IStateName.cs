using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using CookComputing.XmlRpc;


namespace StoreClient.Remote.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRpcUrl("http://127.0.0.1:11000/")]
    public interface IStateName : IXmlRpcProxy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stateNumber"></param>
        /// <returns></returns>
        [XmlRpcMethod("client.getStateName")]
        string[] GetStateName(int stateNumber);
    }
}
