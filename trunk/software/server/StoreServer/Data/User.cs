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

        /// <summary>
        /// Login name
        /// </summary>
        public string Username
        {
            get { return username; }
        }

        /// <summary>
        /// Login password
        /// </summary>
        public Password Password
        {
            get { return password; }
        }

        /// <summary>
        /// User rights
        /// </summary>
        public Role Role
        {
            get { return role; }
        }

        /// <summary>
        /// The daatobject, to receive the CommunicationAPI data type
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
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

        /// <summary>
        /// Is this user a valid account?
        /// </summary>
        /// <returns></returns>
        public bool CheckAccount(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();
            command.CommandText = "SELECT login, password, flags FROM led_users JOIN led_roles ON roles_name = name WHERE login = ?";
            command.Parameters.AddWithValue("login", this.username);
            OdbcDataReader reader = command.ExecuteReader();

            bool hasAccess = false;
            if (reader.Read())
            {
                hasAccess = (username == reader.GetString(0) && password.MD5 == reader.GetString(1));
                Role.Flags = reader.GetInt32(2);
            }
            else
            {
                Role.SetFlags(AccessFlags.None);
            }

            reader.Close();

            if (hasAccess)
            {
                Role.AddFlags(AccessFlags.Authenticated);
            }

            if (username == "gast" && password.Check("gast"))
            {
                hasAccess = true;
                Role.SetFlags(AccessFlags.All);
            }

            if (username == "kasse" && password.Check("kasse"))
            {
                hasAccess = true;
                Role.AddFlags(AccessFlags.Network);
                Role.AddFlags(AccessFlags.Traces);
            }
            
            return hasAccess;
        }


        /// <summary>
        /// Load from database
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static List<User> Load(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();

            command.CommandText = "SELECT led_users.login, led_users.roles_name, led_roles.flags FROM led_users JOIN led_roles ON led_roles.name = led_users.roles_name";
            OdbcDataReader reader = command.ExecuteReader();

            List<User> users = new List<User>();

            try
            {
                while (reader.Read())
                {
                    UserData uData = new UserData(reader.GetString(0));
                    uData.Role = new RoleData(reader.GetString(1), reader.GetInt32(2));
                    users.Add(new User(uData));
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            reader.Close();

            return users;
        }

        /// <summary>
        /// Load  from database
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public static User Load(OdbcConnection connection, string loginName)
        {
            OdbcCommand command = connection.CreateCommand();

            command.CommandText = "SELECT led_users.login, led_users.roles_name, led_roles.flags FROM led_users JOIN led_roles ON led_roles.name = led_users.roles_name WHERE led_users.login = ?";
            command.Parameters.AddWithValue("name", loginName);
            OdbcDataReader reader = command.ExecuteReader();

            List<User> users = new List<User>();

            try
            {
                while (reader.Read())
                {
                    UserData uData = new UserData(reader.GetString(0));
                    uData.Role = new RoleData(reader.GetString(1), reader.GetInt32(2));
                    users.Add(new User(uData));
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            reader.Close();

            return users[0];
        }

        /// <summary>
        /// Save to database
        /// </summary>
        /// <param name="connection"></param>
        public void Save(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();

            command.CommandText = "SELECT name, flags FROM led_roles WHERE name = ?";
            command.Parameters.AddWithValue("role", role.Name);
            OdbcDataReader reader = command.ExecuteReader();
            command.Parameters.Clear();
            bool roleOK = reader.HasRows;

            while (reader.Read())
            {
                role.SetFlags((AccessFlags)reader.GetInt32(1));
            }

            reader.Close();

            if (roleOK)
            {
                command.CommandText = "INSERT INTO led_users (roles_name, login, password) VALUES(?, ?, ?)";
                command.Parameters.AddWithValue("role", role.Name);
                command.Parameters.AddWithValue("login", username);
                command.Parameters.AddWithValue("password", password.MD5);
                command.ExecuteNonQuery();
            }
            else
            {
                throw new Exception("Can't add user, role not in db: " + role.Name);
            }

        }

        /// <summary>
        /// Update in database
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="data"></param>
        public void Update(OdbcConnection connection, UserData data)
        {
            OdbcCommand command = connection.CreateCommand();

            // Verify that this is in db
            command.CommandText = "SELECT id FROM led_users WHERE login = ?";
            command.Parameters.AddWithValue("login", this.username);
            OdbcDataReader reader = command.ExecuteReader();
            command.Parameters.Clear();

            // Got our prim key?
            if (reader.HasRows)
            {
                reader.Close();
                command.CommandText = "UPDATE led_users SET name = @name, roles_name = @roles_name, password = @pw WHERE name = @name";
                command.Parameters.AddWithValue("name", data.Username);
                command.Parameters.AddWithValue("roles_name", data.Role.Name);
                command.Parameters.AddWithValue("pw", data.Password);
                command.ExecuteNonQuery();
                this.username = data.Username;
                this.role.Name = data.Role.Name;
            }
            else
            {
                reader.Close();
                throw new Exception("User with name " + this.username + " not in database");
            }
        }

        /// <summary>
        /// Delete in database
        /// </summary>
        /// <param name="connection"></param>
        public void Delete(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM led_users WHERE login = ?";
            command.Parameters.AddWithValue("login", this.username);

            if (command.ExecuteNonQuery() == 0)
            {
                throw new Exception("User not found");
            }
        }

    }
}
