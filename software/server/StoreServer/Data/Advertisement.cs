using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.DataTypes;
using System.Data.Odbc;

namespace StoreServer.Data
{
    public class Advertisement
    {
        private int id;
        private Region region;
        private string[] text;

        public int Id
        {
            get { return id; }
        }

        public Region Region
        {
            get { return region; }
        }

        public string[] Text
        {
            get { return text; }
        }

        public Advertisement(AdvertisementData advertisement) {
            this.id = advertisement.Id;
            this.region = new Region(advertisement.Region);
            this.text = advertisement.Text;
        }

        public void Save(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO led_advertisements (regions_id, text) VALUES(?, ?, ?)";
            command.Parameters.AddWithValue("regions_id", this.region.Id);
            command.Parameters.AddWithValue("text", this.text);

            command.ExecuteNonQuery();
        }

        public static List<Advertisement> Load(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();

            command.CommandText = "SELECT id, regions_id, line1, line2, line3, line4 FROM led_advertisements";
            OdbcDataReader reader = command.ExecuteReader();

            List<Advertisement> ads = new List<Advertisement>();

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



                SignData sign = new SignData((int)reader.GetInt64(0), region);

                string[] adText = new string[] { reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5) };

                AdvertisementData aData = new AdvertisementData(reader.GetInt32(0), region, adText);
                ads.Add(new Advertisement(aData));
            }

            return ads;
        }

        public void Delete(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM led_advertisements WHERE id = ?";
            command.Parameters.AddWithValue("id", this.id);

            command.ExecuteNonQuery();
        }
    }
}
