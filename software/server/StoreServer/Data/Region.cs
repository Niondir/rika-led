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

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Region(RegionData region)
        {
            this.id = region.Id;
            this.name = region.Name;
        }

        public void Save(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO led_regions (id, name) VALUES(?, ?)";
            command.Parameters.AddWithValue("id", this.id);
            command.Parameters.AddWithValue("name", this.name);
            command.ExecuteNonQuery();
        }

        public static RegionData[] Load(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();

            command.CommandText = "SELECT id, name FROM led_regions";
            OdbcDataReader reader = command.ExecuteReader();

            List<RegionData> regions = new List<RegionData>();

            while (reader.Read())
            {
                regions.Add(new RegionData(reader.GetInt32(0), reader.GetString(1)));
            }

            return regions.ToArray();
        }
    }
}
