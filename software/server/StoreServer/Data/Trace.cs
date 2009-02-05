using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.DataTypes;
using System.Data.Odbc;

namespace StoreServer.Data
{
    public class Trace
    {
        private const float TIME_FACTOR = (255.0f / 1800.0f);
        private int id;
        private DateTime timestamp;
        private List<LocationData> waypoints = new List<LocationData>();

        public TraceData Data
        {
            get
            {
                TraceData t = new TraceData(waypoints.ToArray());
                t.Id = id;
                t.Timestamp = timestamp;

                return t;
            }
        }

        public Trace(TraceData trace)
        {
            if (trace.Locations != null)
            {
                this.waypoints.AddRange(trace.Locations);
            }
            this.timestamp = trace.Timestamp;
        }

        public void CalcTimestamps()
        {
            int maxTs = 0;

            // Get Max Ts
            foreach (LocationData l in waypoints)
            {
                if (l.RelativeTimestamp > maxTs) maxTs = l.RelativeTimestamp;
            }

            // Calc dateTimes
            for (int i = 0; i < waypoints.Count; i++)
            {
                LocationData loc = new LocationData();
                loc.LampId = waypoints[i].LampId;
                loc.RelativeTimestamp = waypoints[i].RelativeTimestamp;
                loc.Time = timestamp - new TimeSpan(0, 0, (maxTs - waypoints[i].RelativeTimestamp));
                loc.RegionName = waypoints[i].RegionName;


                waypoints[i] = loc;
            }
        }

        public void Save(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO `led_traces` (id, timestamp) VALUES (default, ?)";
            command.Parameters.AddWithValue("time", DateTime.Now);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            command.CommandText = "SELECT last_insert_id()";
            Console.WriteLine("convert");
            OdbcDataReader reader = command.ExecuteReader();
            if (reader.Read())
                this.id = reader.GetInt32(0);
            Console.WriteLine("Added trace with ID " + id);

            reader.Close();
            foreach (LocationData l in waypoints) {
                command.CommandText = "INSERT INTO `led_waypoints` (regions_id, traces_id, time, regions_name) VALUES (?, ?, ?, ?)";
                command.Parameters.AddWithValue("regions_id", l.LampId);
                command.Parameters.AddWithValue("traces_id", id);
                Debug.WriteLine("Adding timestamp " + l.RelativeTimestamp + " * " + TIME_FACTOR);
                command.Parameters.AddWithValue("time", (int)(l.RelativeTimestamp * TIME_FACTOR));
                command.Parameters.AddWithValue("regions_name", l.RegionName);
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
        }


        public static List<Trace> Load(OdbcConnection connection, DateTime from, DateTime to)
        {
            List<Trace> traces = Trace.Load(connection);
            List<Trace> result = new List<Trace>();

            // Filter traces
            foreach (Trace t in traces)
            {
                if (t.timestamp > from && t.timestamp < to)
                {
                    result.Add(t);
                }
            }

            return result;
        }

        public static List<Trace> Load(OdbcConnection connection)
        {
            OdbcCommand command = connection.CreateCommand();
            command.CommandText = "SELECT traces_id, regions_id, time, timestamp, name FROM led_waypoints JOIN led_traces ON led_traces.id = traces_id JOIN led_regions ON led_regions.id = regions_id";
            OdbcDataReader reader = command.ExecuteReader();

            //List<Trace> traces = new List<Trace>();
            Dictionary<int, Trace> traces = new Dictionary<int, Trace>();

            while (reader.Read())
            {
                TraceData td = new TraceData();
                td.Id = (int)reader.GetInt64(0);
                td.Timestamp = reader.GetDateTime(3);

                // Get instance of the current Trace
                Trace t;
                if (traces.ContainsKey(td.Id))
                {
                    t = traces[td.Id];
                }
                else 
                {
                    t = new Trace(td);
                    traces[td.Id] = t;
                }

                // Add the current waypoint 
                LocationData loc = new LocationData();
                loc.LampId = reader.GetString(1);
                loc.RelativeTimestamp = (int)reader.GetInt64(2);
                loc.Time = t.timestamp - new TimeSpan(0, 0, loc.RelativeTimestamp);
                loc.RegionName = reader.GetString(4);
                if (loc.RegionName == null)
                {
                    loc.RegionName = "Not Found";
                }

                t.waypoints.Add(loc);
            }

            List<Trace> result = new List<Trace>();
            foreach (Trace t in traces.Values)
            {
                t.CalcTimestamps();
                result.Add(t);
            }

            reader.Close();

            return result;

        }

        public void Delete(OdbcConnection connection)
        {
            // TODO: Not implemented: Delete Trace
            throw new Exception("Not implemented");
        }
    }
}
