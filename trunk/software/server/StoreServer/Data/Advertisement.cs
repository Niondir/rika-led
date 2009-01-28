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

        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
       

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

        public AdvertisementData Data
        {
            get
            {
                return new AdvertisementData(this.Id, this.region.Data, this.Name, this.text, this.StartDate, this.StopDate, this.StartTime, this.StopTime);
            }
        }

        public Advertisement(AdvertisementData advertisement) {
            this.id = advertisement.Id;
            this.region = new Region(advertisement.Region);
            this.text = advertisement.Text;
            this.Name = advertisement.Name;
            this.StartDate = advertisement.StartDate;
            this.StopDate = advertisement.StopDate;
            this.StartTime = advertisement.StartTime;
            this.StopTime = advertisement.StopTime;
        }

        public void Save(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO led_advertisements (regions_id, line1, line2, line3, line4, name, startDate, stopDate, startTime, stopTime ) VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
            command.Parameters.AddWithValue("regions_id", this.region.Id);
            command.Parameters.AddWithValue("line1", this.text[0]);
            command.Parameters.AddWithValue("line2", this.text[1]);
            command.Parameters.AddWithValue("line3", this.text[2]);
            command.Parameters.AddWithValue("line4", this.text[3]);
            command.Parameters.AddWithValue("name", this.Name);
            command.Parameters.AddWithValue("startDate", this.StartDate);
            command.Parameters.AddWithValue("stopDate", this.StopDate);
            command.Parameters.AddWithValue("startTime", this.StartTime);
            command.Parameters.AddWithValue("stopTime", this.StopTime);

            command.ExecuteNonQuery();
        }

        public static List<Advertisement> Load(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();

            command.CommandText = "SELECT id, regions_id, line1, line2, line3, line4, name, startDate, stopDate, startTime, stopTime FROM led_advertisements";
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
                string name = reader.GetString(6);
                AdvertisementData aData = new AdvertisementData((int)reader.GetInt64(0), region, name, adText, reader.GetDate(7), reader.GetDate(8), reader.GetDate(9), reader.GetDate(10));

                
                Advertisement ad = new Advertisement(aData);

                if (ad.StopDate < DateTime.Now)
                {
                    // Delete if invalid, or just don't send back?
                    ad.Delete(connection);
                }
                else
                {
                    ads.Add(ad);
                }
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
