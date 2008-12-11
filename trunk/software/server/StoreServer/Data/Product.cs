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

        public static ProductData[] Load(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();

            command.CommandText = "SELECT id, regions_id, name, price FROM led_products";
            OdbcDataReader reader = command.ExecuteReader();

            List<ProductData> products = new List<ProductData>();

            while (reader.Read())
            {
                RegionData region = new RegionData(reader.GetInt32(1));
                // TODO: Veryfy region !??!? Or get the name??

                SignData sign = new SignData(reader.GetInt32(0), region);
                products.Add(new ProductData(sign, reader.GetString(2), reader.GetDouble(3)));
            }

            return products.ToArray();
        }
    }
}
