using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using System.Linq;
using System.Text;
using CommunicationAPI.DataTypes;
using CommunicationAPI.Radio;

namespace Kasse
{
    public class RadioReceiver
    {
        private SerialPort serialPort;
        private Queue<SerialPacket> rcvQueue;
        private Thread rcvThread;

        public Queue<SerialPacket> ReceiveQueue
        {
            get { return rcvQueue; }
        }

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

                    if (reader.Command == LampCommand.TracePacket) {
                        TracePacket tracePacket = new TracePacket(reader);
                        if (tracePacket.IsValid)
                        {
                            lock (rcvQueue)
                            {
                                rcvQueue.Enqueue(tracePacket);
                                Program.Set();
                            }
                        }
                        else
                            Console.WriteLine("Got invalid packet");
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
