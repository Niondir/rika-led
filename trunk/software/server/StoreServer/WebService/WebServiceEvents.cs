using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.DataTypes;

namespace StoreServer.WebService
{
    public delegate void OnLoginEventHandler(LoginEventArgs e);

    public class LoginEventArgs
    {
        public SessionData session;

        public SessionData Session
        {
            get { return session; }
            set { session = value; }
        }

        public LoginEventArgs(SessionData session)
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
