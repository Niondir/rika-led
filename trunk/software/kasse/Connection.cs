using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.Interface;
using CommunicationAPI.DataTypes;
using CookComputing.XmlRpc;

namespace Kasse
{
    public class ConnectionChangedEventArgs : EventArgs
    {
        public bool Connected { get; set; }
        public ConnectionChangedEventArgs(bool connected)
        {
            Connected = connected;
        }

    }
    
    public class Connection
    {
        private IRemoteFunctions remote;
        private SessionData session;
        private bool connected = false;
        static private UserData user;
        static private Connection instance;

        public bool Connected { get { return connected; } }
        
        public event EventHandler<ConnectionChangedEventArgs> LoginChanged;

        private Connection()
        {
            remote = XmlRpcProxyGen.Create<IRemoteFunctions>();
            
            IRemoteFunctions proxy = (IRemoteFunctions)XmlRpcProxyGen.Create(typeof(IRemoteFunctions));
            //proxy.Url = "Hier die URL";
        }

        static public Connection GetInstance()
        {
            if (instance == null)
                instance = new Connection();
            return instance;
        }

        internal void Login(string username, string password)
        {
            user = new UserData(username, password);
            try
            {
                session = remote.Login(user);
                connected = true;
                if (LoginChanged != null)
                    LoginChanged(this, new ConnectionChangedEventArgs(true));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                connected = false;
            }
        }

        internal void Logout()
        {
            remote.Logout(session);
            connected = false;
            
        }

        internal void Add(TraceData traceData)
        {
            remote.AddTrace(session, traceData);
        }
    }
}
