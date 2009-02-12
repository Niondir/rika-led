using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using CookComputing.XmlRpc;
using StoreServer.Data;

namespace StoreServer.WebService
{

    /// <summary>
    /// Handle the XML-RPC part of the webservice
    /// </summary>
    public abstract class MyXmlRpcListenerService : XmlRpcListenerService
    {
        private HttpListenerContext currentContext;
        private DataManager dataManager;

        /// <summary>
        /// 
        /// </summary>
        public HttpListenerContext CurrentContext
        {
            get { return currentContext; }
            set { currentContext = value; }
        }

        /// <summary>
        /// Given by the constructor
        /// </summary>
        public DataManager DataManager
        {
            get { return dataManager; }
            set { dataManager = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public IPEndPoint RemoteEndPoint
        {
            get 
            {
                if (currentContext != null)
                    return currentContext.Request.RemoteEndPoint;
                else
                {
#if DEBUG
                    return new IPEndPoint(2130706433, 9999);
#else
                    return null;
#endif
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataManager">To get access to the database</param>
        public MyXmlRpcListenerService(DataManager dataManager)
        {
            this.dataManager = dataManager;   
        }

        /// <summary>
        /// Handle one xml-rpc request from a client
        /// </summary>
        /// <param name="RequestContext"></param>
        public override void ProcessRequest(HttpListenerContext RequestContext)
        {
            this.currentContext = RequestContext;
            base.ProcessRequest(RequestContext);
            this.currentContext = null;
        }

    }
}
