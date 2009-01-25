using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.DataTypes;
using System.Data.Odbc;

namespace StoreServer.Data
{
    public class Lamp
    {
        public Lamp(LampData lamp)
        {
            
        }

        public void Save(OdbcConnection connection)
        {
            // TODO: Not implemented: Save Lamp
            throw new Exception("Not implemented");
        }

        public static List<Lamp> Load(OdbcConnection connection)
        {
            // TODO: Not implemented: Load Lamp
            throw new Exception("Not implemented");
        }

        public void Delete(OdbcConnection connection)
        {
            // TODO: Not implemented: Delete Lamp
            throw new Exception("Not implemented");
        }
    }
}
