using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using CookComputing.XmlRpc;
using StoreServer.WebService;

namespace StoreServer
{
    public class HttpService
    {
        public HttpService()
        {

        }

        public class HttpServiceThread
        {
            private HttpListener listener;

            public HttpServiceThread()
            {
            }

            int i = 0;
            public void HttpServiceMain()
            {
                Debug.WriteLine("HttpServiceThread: Started");

                listener = new HttpListener();
                listener.Prefixes.Add("http://127.0.0.1:11000/");
                listener.Start();
                Console.WriteLine("HttpServiceThread: Started Service at http://127.0.0.1:11000/");

                while (!Program.Closing)
                {
                    /*
                    Console.WriteLine("HttpServiceThread: {0}", i);
                    i++;
                    Thread.Sleep(500); */

                    HttpListenerContext context = listener.GetContext();
                    XmlRpcListenerService svc = new ClientHandler();
                    svc.ProcessRequest(context);

                    Debug.WriteLine("Contex Info: \n"+
                    "Request.UserAgent: " + context.Request.UserAgent + "\n" +
                    "Request.HttpMethod: " + context.Request.HttpMethod + "\n" +
                    "Request.Url: " + context.Request.Url);
                }
                Debug.WriteLine("HttpServiceThread: Closing");
            }
        }
    }
}
