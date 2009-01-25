using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI;
using CommunicationAPI.DataTypes;
using System.Data.Odbc;

namespace StoreServer.Data
{
    public class Role
    {
        private int flags;
        private string name;

        public string Name { 
            get { return name; }
            set { name = value; }
        }

        public int Flags
        {
            get { return flags; }
            set { flags = value; }
        }

        public RoleData Data
        {
            get
            {
                RoleData r = new RoleData(name);
                r.Flags = flags;
                return r;
            }
        }

        public Role(RoleData role)
        {
            this.name = role.Name;
            this.flags = role.Flags;
        }

        public void SetFlags(AccessFlags flags)
        {
            this.flags = (int)flags;
        }

        public void AddFlags(AccessFlags flags)
        {
            this.flags |= (int)flags;
        }

        public void DeleteFlags(AccessFlags flags)
        {
            this.flags &= ~(int)flags;
        }

        public bool HasFlags(AccessFlags flags)
        {
            return (this.flags & (int)flags) == (int)flags;
        }

        public void Save(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO led_roles (name, flags) VALUES(?, ?)";
            command.Parameters.AddWithValue("name", this.name);
            command.Parameters.AddWithValue("flags", this.flags);

            command.ExecuteNonQuery();
        }

        public void Update(OdbcConnection connection, RoleData data)
        {
            OdbcCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE led_roles SET name = @name, flags = @flags WHERE name = @name";
            command.Parameters.AddWithValue("name", data.Name);
            command.Parameters.AddWithValue("flags", data.Flags);

            if (command.ExecuteNonQuery() == 0)
            {
                throw new Exception("Nothing changed");
            }

        }

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

        public void Delete(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM led_roles WHERE name = ?";

            command.Parameters.AddWithValue("name", this.name);
            command.ExecuteNonQuery();
            command.Parameters.Clear();

            command.CommandText = "DELETE FROM led_users WHERE roles_name = ?";
            command.Parameters.AddWithValue("roles_name", this.name);
        }
    }
}
