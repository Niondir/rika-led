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
        private string username;
        private Password password;
        private Role role;

        public string Username
        {
            get { return username; }
        }

        public Password Password
        {
            get { return password; }
        }

        public Role Role
        {
            get { return role; }
        }

        public UserData Data
        {
            get
            {
                UserData u = new UserData(username);
                u.Password = this.password.Data;
                u.Role = role.Data;
                return u;
            }
        }

        public User(UserData user)
        {
            this.username = user.Username;
            this.password = new Password(user.Password);
            this.role = new Role(user.Role);
        }

        /// <summary>
        /// Load user from Database, search for the username
        /// </summary>
        /// <param name="username"></param>
        /// <param name="connection"></param>
        public User(string username, OdbcConnection connection)
        {
            this.username = username;
        }

        public bool CheckAccount()
        {
            // TODO: user im Datamanager suchen
            // TODO: get role from db

            Role.AddFlags(AccessFlags.Authenticated);
            return (username == "gast" && password.Check("gast"));
        }


        public void Save(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();

            command.CommandText = "SELECT name FROM led_roles WHERE name = @role";
            command.Parameters.AddWithValue("role", role.Name);
            OdbcDataReader reader = command.ExecuteReader();

            bool roleOK = reader.HasRows;

            while (reader.Read())
            {
                role.SetFlags((AccessFlags)reader.GetInt32(1));
            }

            reader.Close();

            if (roleOK)
            {
                command.CommandText = "INSERT INTO led_users (roles_id, login, password) VALUES(@role, @login, @password)";
                command.Parameters.AddWithValue("@login", username);
                command.Parameters.AddWithValue("@password", password);
                command.ExecuteNonQuery();
            }

        }

        public void Update(OdbcConnection connection, UserData data)
        {

        }

    }
}
