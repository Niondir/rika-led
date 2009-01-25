using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CommunicationAPI.DataTypes;

namespace Kasse
{
    public class Program
    {
        private static RadioReceiver radioReveiver;

        private static bool closing = false;
        public static bool Closing { get { return closing; } }

        private static AutoResetEvent signal = new AutoResetEvent(true);
        public static void Set() { signal.Set(); }

        public static void Main(string[] args)
        {
            Console.WriteLine("Hallo Welt");

            Config config = Config.Load();

            // Beim Server anmelden

            Connection.GetInstance().Login(config.Username, config.Password);

            radioReveiver = new RadioReceiver(config.ComPort);

            while (signal.WaitOne())
            {
                lock (radioReveiver.ReceiveQueue) {
                    while (radioReveiver.ReceiveQueue.Count < 0)
                    {
                        SerialPacket p = radioReveiver.ReceiveQueue.Dequeue();
                        if (p is TracePacket)
                        {
                            (p as TracePacket).SendToServer();
                        }
                    }
                }
            }

            closing = true;

            // Traces empfangen und an Server schicken

        }
    }
}
