using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;
using CommunicationAPI.DataTypes;
using CommunicationAPI;

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

        public void Update(OdbcConnection connection, UserData data)
        {

        }

    }
}
