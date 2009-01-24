using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;
using CommunicationAPI.DataTypes;

namespace StoreServer.Data
{
    public class Product
    {
        private Sign sign;
        private string name;
        private double price;

        public Sign Sign
        {
            get { return sign; }
            set { sign = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public ProductData Data
        {
            get
            {
                return new ProductData(this.sign.Data, this.name, this.price);
            }
        }

        public Product(ProductData product)
        {
            this.sign = new Sign(product.Sign);
            this.name = product.Name;
            this.price = product.Price;
        }

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
                command.CommandText = "UPDATE led_products SET regions_id = @region_id, name = @name price = @price WHERE id = @id";
                command.Parameters.AddWithValue("@region_id", data.Sign.Region.Id);
                command.Parameters.AddWithValue("@name", data.Name);
                command.Parameters.AddWithValue("@price", data.Price);
                command.Parameters.AddWithValue("@id", this.sign.Id);
                command.ExecuteNonQuery();
                this.Sign.Region.Id = data.Sign.Region.Id;
                this.name = data.Name;
                this.Price = data.Price;
                reader.Close();
            }
            else
            {
                reader.Close();
                throw new Exception("Product for sign " + this.sign.Id + " not in database");
            }
        }

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
                RegionData region = new RegionData(reader.GetInt32(1), "Region not found!");
                //TODO: Veryfy region !??!? Or get the name??

                foreach (RegionData r in regions)
                {
                    if (r.Id == region.Id)
                    {
                        region.Name = r.Name;
                    }
                }

                /* TODO: Product table: key as int not unsigned! */
                SignData sign = new SignData((int)reader.GetInt64(0), region);
                ProductData pData = new ProductData(sign, reader.GetString(2), reader.GetDouble(3));
                products.Add(new Product(pData));
            }

            return products;
        }

        public void Delete(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM led_products WHERE id = ?";
            command.Parameters.AddWithValue("id", this.sign.Id);

            command.ExecuteNonQuery();
        }
    }
}
