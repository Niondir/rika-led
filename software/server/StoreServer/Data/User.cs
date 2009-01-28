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

        /// <summary>
        /// Is this user a valid account?
        /// </summary>
        /// <returns></returns>
        public bool CheckAccount(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();
            command.CommandText = "SELECT login, password, flags FROM led_users JOIN led_roles ON roles_name = name WHERE login = @login";
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
            
#if DEBUG
            Role.SetFlags(AccessFlags.Authenticated);
            return (username == "gast" && password.Check("gast"));
#else
            return hasAccess;
#endif
        }


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

        public void Save(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();

            command.CommandText = "SELECT name FROM led_roles WHERE name = ?";
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
            else
            {
                throw new Exception("Can't add user, role not in db: " + role.Name);
            }

        }

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
