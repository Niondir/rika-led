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
        private SessionData session;
        private UserData user;
        private IPEndPoint ipEndPoint;
        private AccessFlags accessFlags;

        private bool authed = false;

        public SessionData Session
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

        public Client(IPEndPoint ipEndPoint, UserData user)
        {
            this.user = user;
            this.ipEndPoint = ipEndPoint;
            this.session = SessionData.NewSession;

            /// Valid logindata?
            this.authed = this.CheckAccount();
            Program.UserManager.AddClient(this);
        }

        private bool CheckAccount()
        {
            // TODO: user im Datamanager suchen
            return (user.Username == "gast" && user.Password.CheckPassword("gast"));
        }

        public bool CheckAccess(SessionData session, IPEndPoint remoteEndPoint, AccessFlags flags)
        {
            Client client = this;

            if (!client.Authed)
                return false;

            if ((client.AccessFlags & flags) == flags)
            {
                return true;
            }

            return false;
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
            OdbcCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO led_users (roles_id, login, password) VALUES(@roles_id, @login, @password)";
            command.Parameters.AddWithValue("@roles_id", user.Role.Name);
            command.Parameters.AddWithValue("@login", user.Username);
            command.Parameters.AddWithValue("@password", user.Password);

            // TODO: Verify that the role exists

            command.ExecuteNonQuery();
        }
    }
}
