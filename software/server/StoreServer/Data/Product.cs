using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;
using CommunicationAPI.DataTypes;

namespace StoreServer.Data
{
    /// <summary>
    /// One product in the supermarket
    /// </summary>
    public class Product
    {
        private Sign sign;
        private string name;
        private double price;

        /// <summary>
        /// The sign object where the price is displayed
        /// </summary>
        public Sign Sign
        {
            get { return sign; }
            set { sign = value; }
        }

        /// <summary>
        /// The name of the product
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// The price of the product
        /// </summary>
        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        /// <summary>
        /// The daatobject, to receive the CommunicationAPI data type
        /// </summary>
        public ProductData Data
        {
            get
            {
                return new ProductData(this.sign.Data, this.name, this.price);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        public Product(ProductData product)
        {
            this.sign = new Sign(product.Sign);
            this.name = product.Name;
            this.price = product.Price;
        }

        /// <summary>
        /// Save to database
        /// </summary>
        /// <param name="connection"></param>
        public void Save(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO led_products (id, regions_id, name, price) VALUES(?, ?, ?, ?)";
            command.Parameters.AddWithValue("id", this.sign.Id);
            command.Parameters.AddWithValue("regions_id", this.sign.Region.Id);
            command.Parameters.AddWithValue("name", this.name);
            command.Parameters.AddWithValue("price", this.price);

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Update in Database
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="data"></param>
        public void Update(OdbcConnection connection, ProductData data)
        {
            OdbcCommand command = connection.CreateCommand();
            
            // Verify that this is in db
            command.CommandText = "SELECT id FROM led_products WHERE id = ?";
            command.Parameters.AddWithValue("id", this.sign.Id);
            OdbcDataReader reader = command.ExecuteReader();
            command.Parameters.Clear();

            // Got our product id
            if (reader.HasRows)
            {
                reader.Close();
                command.CommandText = "UPDATE led_products SET regions_id = ?, name = ?, price = ?, id = ? WHERE id = ?";
                command.Parameters.AddWithValue("region_id", data.Sign.Region.Id);
                command.Parameters.AddWithValue("name", data.Name);
                command.Parameters.AddWithValue("price", data.Price);
                command.Parameters.AddWithValue("new_id", data.Sign.Id);
                command.Parameters.AddWithValue("old_id", this.sign.Id);
                command.ExecuteNonQuery();
                this.Sign.Region.Id = data.Sign.Region.Id;
                this.name = data.Name;
                this.Price = data.Price;
            }
            else
            {
                reader.Close();
                throw new Exception("Product for sign " + this.sign.Id + " not in database");
            }
        }

        /// <summary>
        /// Load from Database
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public static List<Product> Load(OdbcConnection connection, string regionId)
        {
            OdbcCommand command = connection.CreateCommand();

            command.CommandText = "SELECT id, regions_id, name, price FROM led_products WHERE regions_id = ?";
            command.Parameters.AddWithValue("region_id", regionId);
            OdbcDataReader reader = command.ExecuteReader();

            List<Product> products = new List<Product>();

            while (reader.Read())
            {
                RegionData region = new RegionData(reader.GetString(1), "Not requested!");
                SignData sign = new SignData((int)reader.GetInt64(0), region);
                ProductData pData = new ProductData(sign, reader.GetString(2), reader.GetDouble(3));
                products.Add(new Product(pData));
            }

            return products;
        }

        /// <summary>
        /// Load from Database
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static List<Product> Load(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();

            command.CommandText = "SELECT id, regions_id, name, price FROM led_products";
            OdbcDataReader reader = command.ExecuteReader();

            List<Product> products = new List<Product>();

            List<RegionData> regions = new List<RegionData>();
            regions.AddRange(Region.Load(connection));

            while (reader.Read())
            {
                RegionData region = new RegionData(reader.GetString(1), "Region not found!");
                //TODO: Veryfy region !??!? Or get the name??

                foreach (RegionData r in regions)
                {
                    if (r.Id == region.Id)
                    {
                        region.Name = r.Name;
                    }
                }

                /* TODO: Product table: key as int not unsigned!? */
                SignData sign = new SignData((int)reader.GetInt64(0), region);
                ProductData pData = new ProductData(sign, reader.GetString(2), reader.GetDouble(3));
                products.Add(new Product(pData));
            }

            return products;
        }

        /// <summary>
        /// Delete in Database
        /// </summary>
        /// <param name="connection"></param>
        public void Delete(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM led_products WHERE id = ?";
            command.Parameters.AddWithValue("id", this.sign.Id);

            command.ExecuteNonQuery();
        }
    }
}
