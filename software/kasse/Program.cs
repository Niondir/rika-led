using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CommunicationAPI.DataTypes;

namespace Kasse
{
    /* TODO:
     * Invalid argumnt count :(
     * 
     * */

    public class Program
    {
        private static RadioReceiver radioReveiver;

        private static bool closing = false;
        public static bool Closing { get { return closing; } }

        private static AutoResetEvent signal = new AutoResetEvent(true);
        public static void Set() { signal.Set(); }

        private static Config config;
        public static Config Config { get { return config; } }



        public static void Main(string[] args)
        {
            config = Config.Load();

            // Beim Server anmelden

            Connection.GetInstance().Login(config.Username, config.Password);

            radioReveiver = new RadioReceiver(Config.ComPort);

            while (signal.WaitOne())
            {
                Console.WriteLine("Waiting for next Packet");
                lock (radioReveiver.ReceiveQueue) {
                    while (radioReveiver.ReceiveQueue.Count > 0)
                    {
                        SerialPacket p = radioReveiver.ReceiveQueue.Dequeue();
                        if (p is TracePacket)
                        {
                            Console.WriteLine("Sending trace to server");
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
