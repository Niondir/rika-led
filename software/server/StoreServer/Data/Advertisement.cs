using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.DataTypes;
using System.Data.Odbc;

namespace StoreServer.Data
{
    /// <summary>
    /// One advertisement
    /// </summary>
    public class Advertisement
    {
        private int id;
        private Region region;
        private string[] text;

        /// <summary>
        /// Name of the ad
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime StopDate { get; set; }
        /// <summary>
        /// Daily start time
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// Dayli stop time
        /// </summary>
        public DateTime StopTime { get; set; }
       
        /// <summary>
        /// Database id of the ad, for internal use only
        /// </summary>
        public int Id
        {
            get { return id; }
        }

        /// <summary>
        /// Region of the ad
        /// </summary>
        public Region Region
        {
            get { return region; }
        }

        /// <summary>
        /// Advertisement text in 4 rows
        /// </summary>
        public string[] Text
        {
            get { return text; }
        }

        /// <summary>
        /// The daatobject, to receive the CommunicationAPI data type
        /// </summary>
        public AdvertisementData Data
        {
            get
            {
                return new AdvertisementData(this.Id, this.region.Data, this.Name, this.text, this.StartDate, this.StopDate, this.StartTime, this.StopTime);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="advertisement"></param>
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

        /// <summary>
        /// Save to database
        /// </summary>
        /// <param name="connection"></param>
        public void Save(OdbcConnection connection)
        {
            if (text.Length != 4)
            {
                throw new Exception("Advertisement text need 4 rows!");
            }
            
            OdbcCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO led_advertisements (regions_id, line1, line2, line3, line4, name, startDate, stopDate, startTime, stopTime) VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
            command.Parameters.AddWithValue("regions_id", this.region.Id);

            try
            {
                command.Parameters.AddWithValue("line1", this.text[0]);
                command.Parameters.AddWithValue("line2", this.text[1]);
                command.Parameters.AddWithValue("line3", this.text[2]);
                command.Parameters.AddWithValue("line4", this.text[3]);
            }
            catch (Exception ex)
            {
                throw new Exception("Problem with the text values: " + ex);
            }
            command.Parameters.AddWithValue("name", this.Name);
            command.Parameters.AddWithValue("startDate", this.StartDate);
            command.Parameters.AddWithValue("stopDate", this.StopDate);
            command.Parameters.AddWithValue("startTime", this.StartTime);
            command.Parameters.AddWithValue("stopTime", this.StopTime);

            if (command.ExecuteNonQuery() == 0)
            {
                throw new Exception("No advertise added for unknown reason");
            }
        }

        /// <summary>
        /// Load from Database
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
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

                DateTime startDate = reader.GetDateTime(7);
                DateTime stopDate = reader.GetDateTime(8);
                DateTime startTime = reader.GetDateTime(9);
                DateTime stopTime = reader.GetDateTime(10);

                AdvertisementData aData = new AdvertisementData((int)reader.GetInt64(0), region, name, adText, startDate, stopDate, startTime, stopTime);

                
                Advertisement ad = new Advertisement(aData);

                ads.Add(ad);
            }

            return ads;
        }

        /// <summary>
        /// Deletei n database
        /// </summary>
        /// <param name="connection"></param>
        public void Delete(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM led_advertisements WHERE id = ?";
            command.Parameters.AddWithValue("id", this.id);

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Update in database
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="data"></param>
        public void Update(OdbcConnection connection, AdvertisementData data)
        {
            OdbcCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE led_advertisements SET regions_id = ?, line1 = ?, line2 = ?, line3 = ?, line4 = ?, name = ?, startDate = ?, stopDate = ?, startTime = ? stopTime = ? WHERE id = ?";
            command.Parameters.AddWithValue("regions_id", data.Region.Id);
            command.Parameters.AddWithValue("line1", data.Text[0]);
            command.Parameters.AddWithValue("line2", data.Text[1]);
            command.Parameters.AddWithValue("line3", data.Text[2]);
            command.Parameters.AddWithValue("line4", data.Text[3]);
            command.Parameters.AddWithValue("name", data.Name);
            command.Parameters.AddWithValue("startDate", data.StartDate);
            command.Parameters.AddWithValue("stopDate", data.StopDate);
            command.Parameters.AddWithValue("startTime", data.StartTime);
            command.Parameters.AddWithValue("stopTime", data.StopTime);
            command.Parameters.AddWithValue("where_id", this.id);
        }
    }
}
