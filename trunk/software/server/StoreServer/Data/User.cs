using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;
using CommunicationAPI.DataTypes;

namespace StoreServer.Data
{
    /// <summary>
    /// Repräsentiert einen Useraccount
    /// </summary>
    public class User
    {
        private UserData user;

        public UserData UserData
        {
            get { return user; }
        }

        public User(UserData user)
        {
            this.user = user;
        }

        /// <summary>
        /// Load user from Database, search for the username
        /// </summary>
        /// <param name="username"></param>
        /// <param name="connection"></param>
        public User(string username, OdbcConnection connection)
        {
            this.user = user;
        }

        public void Save(OdbcConnection connection)
        {

        }

        public void Update(OdbcConnection connection, UserData data)
        {

        }

    }
}
