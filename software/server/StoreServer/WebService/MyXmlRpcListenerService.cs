using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using CookComputing.XmlRpc;
using StoreServer.Data;

namespace StoreServer.WebService
{
    public abstract class MyXmlRpcListenerService : XmlRpcListenerService
    {
        private HttpListenerContext currentContext;
        private DataManager dataManager;

        public HttpListenerContext CurrentContext
        {
            get { return currentContext; }
            set { currentContext = value; }
        }

        public DataManager DataManager
        {
            get { return dataManager; }
            set { dataManager = value; }
        }

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

        public MyXmlRpcListenerService(DataManager dataManager)
        {
            this.dataManager = dataManager;   
        }

        public override void ProcessRequest(HttpListenerContext RequestContext)
        {
            this.currentContext = RequestContext;
            base.ProcessRequest(RequestContext);
            this.currentContext = null;
        }

    }
}
