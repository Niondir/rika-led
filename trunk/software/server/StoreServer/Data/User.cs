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

        public User(UserData user)
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
