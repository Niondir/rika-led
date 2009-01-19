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
            // TODO: Not implemented: Load Role
            throw new Exception("Not implemented");
        }

        public void Delete(OdbcConnection connection)
        {
            // TODO: Not implemented: Delete Role
            throw new Exception("Not implemented");
        }
    }
}
