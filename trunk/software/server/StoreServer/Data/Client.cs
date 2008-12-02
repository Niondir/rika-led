using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Data.Odbc;
using CommunicationAPI.DataTypes;
using CommunicationAPI;

namespace StoreServer.Data
{
    /// <summary>
    /// Represents one connected client
    /// </summary>
    public class Client
    {
        private Session session;
        private User user;
        private IPEndPoint ipEndPoint;
        private AccessFlags accessFlags;

        private bool authed = false;

        public Session Session
        {
            get { return session; }
        }

        public IPAddress IP
        {
            get { return ipEndPoint.Address; }
        }

        public bool Authed
        {
            get { return authed; }
            set { authed = value; }
        }

        public AccessFlags AccessFlags
        {
            get { return accessFlags; }
        }

        public Client(IPEndPoint ipEndPoint, User user)
        {
            this.user = user;
            this.ipEndPoint = ipEndPoint;
            this.session = Session.NewSession;

            /// Valid logindata?
            this.authed = this.CheckAccount();
            Program.UserManager.AddClient(this);
        }

        private bool CheckAccount()
        {
            // TODO: user im Datamanager suchen
            return (user.Username == "gast" && user.Password.CheckPassword("gast"));
        }

        public void Logout()
        {
            this.authed = false;
            Program.UserManager.RemoveClient(this);
        }

        public bool CheckSession()
        {
            DateTime sDate = new DateTime(this.session.Timestamp);
            if (DateTime.Now - sDate > TimeSpan.FromMinutes(10))
            {
                Logout();
                return false;
            }

            RefreshSession();
            return true;
        }

        public void RefreshSession()
        {
            this.session.Timestamp = DateTime.Now.Ticks;
        }

        /// <summary>
        /// Updates the AccessFlags from the Database
        /// </summary>
        public void UpdateAccessFlags() {
            accessFlags = AccessFlags.Authenticated;
        }

        

        public void Save(OdbcConnection connection)
        {

        }
    }
}
