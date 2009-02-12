using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI;
using CommunicationAPI.DataTypes;
using System.Data.Odbc;

namespace StoreServer.Data
{
    /// <summary>
    /// The role of one user with it's rights
    /// </summary>
    public class Role
    {
        private int flags;
        private string name;

        /// <summary>
        /// Name of the role
        /// </summary>
        public string Name { 
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Access flags of the role
        /// </summary>
        public int Flags
        {
            get { return flags; }
            set { flags = value; }
        }

        /// <summary>
        /// The daatobject, to receive the CommunicationAPI data type
        /// </summary>
        public RoleData Data
        {
            get
            {
                RoleData r = new RoleData(name);
                r.Flags = flags;
                return r;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        public Role(RoleData role)
        {
            this.name = role.Name;
            this.flags = role.Flags;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flags"></param>
        public void SetFlags(AccessFlags flags)
        {
            this.flags = (int)flags;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flags"></param>
        public void AddFlags(AccessFlags flags)
        {
            this.flags |= (int)flags;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flags"></param>
        public void DeleteFlags(AccessFlags flags)
        {
            this.flags &= ~(int)flags;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool HasFlags(AccessFlags flags)
        {
            return (this.flags & (int)flags) == (int)flags;
        }

        /// <summary>
        /// Save to database
        /// </summary>
        /// <param name="connection"></param>
        public void Save(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO led_roles (name, flags) VALUES(?, ?)";
            command.Parameters.AddWithValue("name", this.name);
            command.Parameters.AddWithValue("flags", this.flags);

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Update in database
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="data"></param>
        public void Update(OdbcConnection connection, RoleData data)
        {
            OdbcCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE led_roles SET name = ?, flags = ? WHERE name = ?";
            command.Parameters.AddWithValue("name", data.Name);
            command.Parameters.AddWithValue("flags", data.Flags);
            command.Parameters.AddWithValue("where_name", this.name);

            if (command.ExecuteNonQuery() <= 0)
            {
                throw new Exception("Nothing changed");
            }

        }

        /// <summary>
        /// Load from database
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static List<Role> Load(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();

            command.CommandText = "SELECT name, flags FROM led_roles";
            OdbcDataReader reader = command.ExecuteReader();

            List<Role> roles = new List<Role>();

            while (reader.Read())
            {
                RoleData rData = new RoleData(reader.GetString(0), reader.GetInt32(1));
                roles.Add(new Role(rData));
            }

            return roles;
        }

        /// <summary>
        /// Delete in database
        /// </summary>
        /// <param name="connection"></param>
        public void Delete(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM led_roles WHERE name = ?";

            command.Parameters.AddWithValue("name", this.name);
            command.ExecuteNonQuery();
            command.Parameters.Clear();

            command.CommandText = "DELETE FROM led_users WHERE roles_name = ?";
            command.Parameters.AddWithValue("roles_name", this.name);
            command.ExecuteNonQuery();
            
        }
    }
}
