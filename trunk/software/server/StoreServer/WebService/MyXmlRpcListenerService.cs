using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using CookComputing.XmlRpc;

namespace StoreServer.WebService
{
    public abstract class MyXmlRpcListenerService : XmlRpcListenerService
    {
        private HttpListenerContext currentContext;

        public HttpListenerContext CurrentContext
        {
            get { return currentContext; }
            set { currentContext = value; }
        }

        public IPEndPoint RemoteEndPoint
        {
            get { return currentContext.Request.RemoteEndPoint; }
        }

        public override void ProcessRequest(HttpListenerContext RequestContext)
        {
            this.currentContext = RequestContext;
            base.ProcessRequest(RequestContext);
            this.currentContext = null;
        }

    }
}
