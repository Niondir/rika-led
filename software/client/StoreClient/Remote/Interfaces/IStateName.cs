using System;
using System.Collections.Generic;
using System.Text;

using CookComputing.XmlRpc;

namespace StoreClient.Remote.Interfaces
{
    [XmlRpcUrl("http://127.0.0.1:11000/")]
    public interface IStateName : IXmlRpcProxy
    {
        [XmlRpcMethod("client.getStateName")]
        string GetStateName(int stateNumber);
    }
}
