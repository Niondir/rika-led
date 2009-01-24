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
            // TODO: Not implemented: Save Role
            throw new Exception("Not implemented");
        }

        public void Update(OdbcConnection connection, RoleData data)
        {
            // TODO: Not implemented: Update Role
            throw new Exception("Not implemented");
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

            // TODO: delete all users with this role

            command.Parameters.AddWithValue("name", this.name);

            command.ExecuteNonQuery();
        }
    }
}
