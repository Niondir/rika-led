using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunicationAPI.DataTypes;

namespace Kasse
{
    public class TracePacket : SerialPacket
    {
        private TraceData trace;
        private bool valid = false;

        public bool IsValid
        {
            get { return valid; }
        }

        public TracePacket(PacketReader reader)
        {
            if (!reader.IsValidChkSum)
            {
                Console.WriteLine("Wooops, invalid chk sum: " + reader.PacketString);
                return;
            }

            if (reader.Args.Count % 2 == 0)
            {
                Console.WriteLine("Invalid argument count: " + reader.PacketString);
                return;
            }

            List<LocationData> locations = new List<LocationData>();

            LocationData loc = new LocationData();
            for (int i = 0; i < reader.Args.Count; i++)
            {
                if (i % 2 == 0)
                { // grade
                    loc.LampId = reader.Args[i];
                }
                else
                {
                    try
                    {
                        loc.RelativeTimestamp = int.Parse(reader.Args[i]);
                        locations.Add(loc); // struct will be copied
                    }
                    catch
                    {
                        Console.WriteLine("Invalid argument: " + reader.Args[i]);
                        break;
                    }
                }
            }


            trace = new TraceData(locations.ToArray());
            valid = true;
        }

        public void SendToServer()
        {
            if (valid)
            {
                Connection.GetInstance().Add(trace);
            }
            else
            {
                Console.WriteLine("Error: Can't send invalid packet to server");
            }
        }
    }
}
