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
        private bool authed;
        private AccessFlags accessFlags;

        public Session Session
        {
            get { return session; }
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

        public Client(Session session, IPEndPoint ipEndPoint)
        {
            authed = false;
            this.session = session;
            this.ipEndPoint = ipEndPoint;

            UpdateAccessFlags();
        }

        /// <summary>
        /// Updates the AccessFlags from the Database
        /// </summary>
        public void UpdateAccessFlags() {
            accessFlags = AccessFlags.Authenticated;
        }

        public void RefreshSession()
        {
            this.session.Timestamp = DateTime.Now.Ticks;
        }

        public void Save(OdbcConnection connection)
        {

        }
    }
}
