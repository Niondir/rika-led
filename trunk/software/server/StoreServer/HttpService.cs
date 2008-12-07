using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using CookComputing.XmlRpc;
using StoreServer.WebService;
using StoreServer.Data;

namespace StoreServer
{
    public class HttpService
    {
        private MyXmlRpcListenerService service;
        private HttpServiceThread serviceThread;

        public HttpService(string url, MyXmlRpcListenerService service)
        {
            this.service = service;

            serviceThread = new HttpServiceThread(url);
        }

        /// <summary>
        /// Proceed one http request if avalible. Otherwise do nothing.
        /// </summary>
        public void Slice()
        {
            Queue<HttpListenerContext> workingQueue = serviceThread.Queue;

            while (workingQueue.Count > 0)
            {
                HttpListenerContext context = workingQueue.Dequeue();
                service.ProcessRequest(context);
               
                Debug.WriteLine("Contex Info: \n" +
                    "Request.UserAgent: " + context.Request.UserAgent + "\n" +
                    "Request.HttpMethod: " + context.Request.HttpMethod + "\n" +
                    "Request.Url: " + context.Request.Url);
            }
        }

        public void Abort()
        {
            serviceThread.Abort();
        
        }

        public class HttpServiceThread
        {
            private HttpListener listener;
            private Thread thread;

            private Queue<HttpListenerContext> queue;
            private Queue<HttpListenerContext> workingQueue;

            
            public HttpServiceThread(string url)
            {
                queue = new Queue<HttpListenerContext>();
                workingQueue = new Queue<HttpListenerContext>();

                Debug.WriteLine("HttpServiceThread: Started");
                listener = new HttpListener();
                listener.Prefixes.Add(url);
                listener.Start();
                Console.WriteLine("HttpServiceThread: Started Service at " + url);

                thread = new Thread(new ThreadStart(this.HttpServiceMain));
                thread.Name = "HttpServiceThread";
                thread.Start();
            }

            public void Abort()
            {
                thread.Abort();
                listener.Abort();
            }

            public Queue<HttpListenerContext> Queue
            {
                get
                {
                    if (workingQueue.Count != 0)
                    {
                        throw new Exception("HttpService: [ERROR] Hey, someone didn't handled our queue!");
                        //Console.WriteLine("HttpService: [ERROR] Hey, someone didn't handled our queue!");
                        //return workingQueue; 
                    }

                    lock (queue)
                    {
                        // Swap Queue with workingQueue
                        Queue<HttpListenerContext> temp = workingQueue;
                        workingQueue = queue;
                        queue = temp;
                    }

                    return workingQueue;
                }
            }

            public void HttpServiceMain()
            {

                while (!Program.Closing)
                {
                    // Every time we receive an Context we save it to our thread save queue
                    HttpListenerContext context = listener.GetContext();
                    queue.Enqueue(context);
                    Program.Set();
                }

                Debug.WriteLine("HttpServiceThread: Closing");
            }

        }
    }
}
