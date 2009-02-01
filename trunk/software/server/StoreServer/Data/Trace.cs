using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.DataTypes;
using System.Data.Odbc;

namespace StoreServer.Data
{
    public class Trace
    {
        private int id;
        private DateTime timestamp;
        private List<LocationData> waypoints = new List<LocationData>();

        public TraceData Data
        {
            get
            {
                TraceData t = new TraceData(waypoints);
                t.Id = id;
                t.Timestamp = timestamp;

                return t;
            }
        }

        public Trace(TraceData trace)
        {
            this.waypoints = trace.Locations;
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
            foreach (LocationData l in waypoints)
            {
                l.Time = timestamp - (maxTs - l.RelativeTimestamp);
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
            this.id = (int)command.ExecuteScalar();
            Console.WriteLine("Added trace with ID " + id);


            foreach (LocationData l in waypoints) {
                command.CommandText = "INSERT INTO `led_waypoints` (lamps_id, traces_id, time) VALUES (?, ?, ?)";
                command.Parameters.AddWithValue("lamps_id", id);
                command.Parameters.AddWithValue("traces_id", l.LampId);
                command.Parameters.AddWithValue("time", l.RelativeTimestamp);
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
            command.CommandText = "SELECT traces_id, lamps_id, time, timestamp FROM led_waypoints JOIN led_traces ON led_traces.id = traces_id";
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
                loc.LampId = (int)reader.GetInt64(1);
                loc.RelativeTimestamp = (int)reader.GetInt64(2);
                loc.Time = t.timestamp - new TimeSpan(0, 0, loc.RelativeTimestamp);

                t.waypoints.Add(loc);


                traces.Add(t);
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
