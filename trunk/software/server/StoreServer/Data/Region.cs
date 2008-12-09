using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;
using CommunicationAPI.DataTypes;

namespace StoreServer.Data
{
    public class Region
    {
        private int id;
        private string name;

        public Region(RegionData region)
        {
            this.id = region.Id;
            this.name = region.Name;
        }

        public void Save(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO led_regions (id, name) VALUES(@id, @name)";
            command.Parameters.AddWithValue("@id", this.id);
            command.Parameters.AddWithValue("@name", this.name);
            command.ExecuteNonQuery();
        }
    }
}
