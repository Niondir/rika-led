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

        public RoleData Role
        {
            get { return user.Role; }
        }

        public Client(IPEndPoint ipEndPoint, UserData user)
        {
            this.user = user;
            this.ipEndPoint = ipEndPoint;
            this.session = SessionData.NewSession;

            /// Valid logindata?
            this.authed = this.CheckAccount();
            
            UpdateAccessFlags();
            
            Program.UserManager.AddClient(this);
        }

        private bool CheckAccount()
        {
            // TODO: user im Datamanager suchen
            // TODO: get role from db

            user.Role.AddFlags(AccessFlags.Authenticated);
            return (user.Username == "gast" && user.Password.CheckPassword("gast"));
        }

        public bool CheckAccess(SessionData session, IPEndPoint remoteEndPoint, AccessFlags flags)
        {
            if (!this.Authed)
                return false;

            if (user.Role.HasFlags(flags))
            {
                return true;
            }

            return false;
        }

        public void Logout()
        {
            this.authed = false;
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
            if (this.authed)
            {
                // TODO: Receive role from Database
                user.Role.AddFlags(AccessFlags.Authenticated);
            }
            else
            {
                user.Role.SetFlags(AccessFlags.None);
            }
        }

        

        // TODO: untested!
        public void Save(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();

            command.CommandText = "SELECT name FROM led_roles WHERE name = @role";
            command.Parameters.AddWithValue("role", user.Role.Name);
            OdbcDataReader reader = command.ExecuteReader();

            bool roleOK = reader.HasRows;

            while (reader.Read())
            {
                user.Role.SetFlags((AccessFlags)reader.GetInt32(1));
            }

            reader.Close();

            if (roleOK)
            {
                command.CommandText = "INSERT INTO led_users (roles_id, login, password) VALUES(@role, @login, @password)";
                command.Parameters.AddWithValue("@login", user.Username);
                command.Parameters.AddWithValue("@password", user.Password);
                command.ExecuteNonQuery();
            }
        }
    }
}
