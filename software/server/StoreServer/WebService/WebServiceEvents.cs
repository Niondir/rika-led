using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.DataTypes;

namespace StoreServer.WebService
{
    public delegate void OnLoginEventHandler(LoginEventArgs e);

    public class LoginEventArgs
    {
        private Session session;

        public Session Session
        {
            get { return session; }
            set { session = value; }
        }

        public LoginEventArgs(Session session)
        {
            this.session = session;
        }
    }

    public class WebServiceEvents
    {
        public static event OnLoginEventHandler Login;

        public static void InvokeLogin(LoginEventArgs e)
        {
            if (Login != null)
                Login(e);
        }
    }
}
