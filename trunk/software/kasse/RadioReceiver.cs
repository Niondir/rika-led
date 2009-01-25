using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using System.Linq;
using System.Text;
using CommunicationAPI.DataTypes;

namespace Kasse
{
    public class RadioReceiver
    {
        private SerialPort serialPort;
        private Queue<SerialPacket> rcvQueue;
        private Thread rcvThread;

        public RadioReceiver(string portName)
        {
            rcvQueue = new Queue<SerialPacket>();
            serialPort = new SerialPort(portName, 9600);

            Connect();

            rcvThread = new Thread(new ThreadStart(rcvLoop));
            rcvThread.Name = "SerialRcvThread";
            rcvThread.Start();
        }

        private void Connect()
        {
            try
            {
                serialPort.Open();
                Console.WriteLine("Opened serial port on " + serialPort.PortName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void rcvLoop()
        {
            while (!Program.Closing)
            {
                if (serialPort.IsOpen)
                {
                    string packetStr = serialPort.ReadTo(">");
                    Console.WriteLine("Got: " + packetStr);

                    PacketReader reader = new PacketReader(packetStr);
                    // Gibt nur eine art von Packeten die wir gleich hier in Traces umwandeln
                    if (reader.IsValidChkSum)
                    {
                        List<LocationData> locations = new List<LocationData>();

                        if (reader.Args.Count % 2 == 0) 
                        {
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
                                        loc.Timestamp = int.Parse(reader.Args[i]);
                                        locations.Add(loc); // struct will be copied
                                    }
                                    catch 
                                    {
                                        Console.WriteLine("Invalid argument: " + reader.Args[i]);
                                        break;
                                    }
                                }
                            }

                            TraceData trace = new TraceData(locations.ToArray());
                        }
                        else {
                            Console.WriteLine("Invalid argument count: " + reader.PacketString);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wooops, invaid chk sum: " + reader.PacketString);
                    }
                }
                else
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Serial port is closed, try to reconnect ...");
                    Console.WriteLine(rcvQueue.Count + " packets in queue");
                    Connect();
                }
            }
        }

    }
}
