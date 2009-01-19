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

        public Client(IPEndPoint ipEndPoint, UserData user)
        {
            this.user = new User(user);
            this.ipEndPoint = ipEndPoint;
            this.session = Session.NewSession();

            /// Valid logindata?
            this.authed = this.user.CheckAccount();
            
            UpdateAccessFlags();
            
            Program.UserManager.AddClient(this);
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
            if (!session.Alive)
            {
                Logout();
                return false;
            }

            session.Refresh();
            return true;
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
