using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;
using CommunicationAPI.DataTypes;

namespace StoreServer.Data
{
    public class Region
    {
        private string id;
        private string name;

        public string Id
        {
            get { return id.ToLower(); }
            set { id = value.ToLower(); }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public RegionData Data
        {
            get
            {
                return new RegionData(Id, name);
            }
        }

        public Region(RegionData region)
        {
            this.Id = region.Id;
            this.name = region.Name.ToLower();
        }

        public void Save(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO led_regions (id, name) VALUES(?, ?)";
            command.Parameters.AddWithValue("id", this.Id);
            command.Parameters.AddWithValue("name", this.name);
            command.ExecuteNonQuery();
        }

        public void Update(OdbcConnection connection, RegionData data)
        {
            OdbcCommand command = connection.CreateCommand();

            // Verify that this is in db
            command.CommandText = "SELECT id FROM led_regions WHERE id = ?";
            command.Parameters.AddWithValue("id", this.Id);
            OdbcDataReader reader = command.ExecuteReader();
            command.Parameters.Clear();
            

            // Got our region id
            if (reader.HasRows)
            {
                reader.Close();
                command.CommandText = "UPDATE led_regions SET name = ?, id = ? WHERE id = ?";
                command.Parameters.AddWithValue("name", data.Name);
                command.Parameters.AddWithValue("new_id", data.Id);
                command.Parameters.AddWithValue("id", this.Id);
                command.ExecuteNonQuery();
                this.name = data.Name;
            }
            else
            {
                reader.Close();
                throw new Exception("Region id " + this.Id + " not in database");
            }
        }

        public static RegionData[] Load(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();

            command.CommandText = "SELECT id, name FROM led_regions";
            OdbcDataReader reader = command.ExecuteReader();

            List<RegionData> regions = new List<RegionData>();

            while (reader.Read())
            {
                regions.Add(new RegionData(reader.GetString(0), reader.GetString(1)));
            }

            return regions.ToArray();
        }

        public void Delete(OdbcConnection connection)
        {
            // Delete all products in this region
            List<Product> products = Product.Load(connection, this.Id);
            foreach (Product p in products)
            {
                p.Delete(connection);
            }

            OdbcCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM led_regions WHERE id = ?";
            command.Parameters.AddWithValue("id", this.Id);

            command.ExecuteNonQuery();
        }
    }
}
