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

        /// <summary>
        /// The session of the client
        /// </summary>
        public Session Session
        {
            get { return session; }
        }

        /// <summary>
        /// Ip the client is connected from
        /// </summary>
        public IPAddress IP
        {
            get { return ipEndPoint.Address; }
        }

        /// <summary>
        /// Is the client authenticated?
        /// </summary>
        public bool Authed
        {
            get { return authed; }
            set { authed = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipEndPoint"></param>
        /// <param name="user"></param>
        /// <param name="connection"></param>
        public Client(IPEndPoint ipEndPoint, UserData user, OdbcConnection connection)
        {
            this.user = new User(user);
            this.ipEndPoint = ipEndPoint;
            this.session = Session.NewSession();

            // Valid logindata?
            this.authed = this.user.CheckAccount(connection);
           
            Program.UserManager.AddClient(this);
        }

        /// <summary>
        /// Check if this request has access
        /// </summary>
        /// <param name="session"></param>
        /// <param name="remoteEndPoint"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        public void Logout()
        {
            this.authed = false;
        }

        /// <summary>
        /// Session still alive?
        /// </summary>
        /// <returns></returns>
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
        /// Not needed, done on login check
        /// </summary>
        /*public void UpdateAccessFlags() {
            if (this.authed)
            {
                // TODO: Receive role from Database
                user.Role.AddFlags(AccessFlags.Authenticated);
            }
            else
            {
                user.Role.SetFlags(AccessFlags.None);
            }
        } */

        public void Save(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();

            command.CommandText = "SELECT name FROM led_roles WHERE name = ?";
            command.Parameters.AddWithValue("role", user.Role.Name);
            OdbcDataReader reader = command.ExecuteReader();
            command.Parameters.Clear();
            bool roleOK = reader.HasRows;

            while (reader.Read())
            {
                user.Role.SetFlags((AccessFlags)reader.GetInt32(1));
            }

            reader.Close();

            if (roleOK)
            {
                command.CommandText = "INSERT INTO led_users (roles_name, login, password) VALUES(?, ?, ?)";
                command.Parameters.AddWithValue("role", user.Role.Name);
                command.Parameters.AddWithValue("login", user.Username);
                command.Parameters.AddWithValue("password", user.Password);
                command.ExecuteNonQuery();
            }
        }
    }
}
