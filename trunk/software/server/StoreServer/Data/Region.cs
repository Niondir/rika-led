using System;
using System.Collections.Generic;
using System.Data.Odbc;
using CommunicationAPI.DataTypes;

namespace StoreServer.Data
{
    /// <summary>
    /// One region is one lampcontroller in the supermarket
    /// </summary>
    public class Region
    {
        private string id;
        private string name;

        /// <summary>
        /// ID of the region = LampId
        /// </summary>
        public string Id
        {
            get { return id.ToLower(); }
            set { id = value.ToLower(); }
        }

        /// <summary>
        /// Name of the Region
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// The daatobject, to receive the CommunicationAPI data type
        /// </summary>
        public RegionData Data
        {
            get
            {
                return new RegionData(Id, name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="region"></param>
        public Region(RegionData region)
        {
            this.Id = region.Id.ToLower();
            this.name = region.Name;
        }

        /// <summary>
        /// Save to database
        /// </summary>
        /// <param name="connection"></param>
        public void Save(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO led_regions (id, name) VALUES(?, ?)";
            command.Parameters.AddWithValue("id", this.Id);
            command.Parameters.AddWithValue("name", this.name);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Load from Database
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="data"></param>
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
                command.Parameters.AddWithValue("old_id", this.Id);
                command.ExecuteNonQuery();
                this.name = data.Name;

                if (data.Id != this.id)
                {
                    command.Parameters.Clear();
                    command.CommandText = "UPDATE led_products SET regions_id = ? WHERE regions_id = ?";
                    command.Parameters.AddWithValue("new_id", data.Id.ToLower());
                    command.Parameters.AddWithValue("old_id", this.id.ToLower());
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                reader.Close();
                throw new Exception("Region id " + this.Id + " not in database");
            }
        }

        /// <summary>
        /// Load from Database
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Delete in Database
        /// </summary>
        /// <param name="connection"></param>
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
