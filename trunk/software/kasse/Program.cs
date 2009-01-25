using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunicationAPI.DataTypes;

namespace Kasse
{
    public class Program
    {
        private static RadioReceiver radioReveiver;

        private static bool closing = false;
        public static bool Closing { get { return closing; } }

        public static void Main(string[] args)
        {
            Console.WriteLine("Hallo Welt");

            Config config = Config.Load();

            // Beim Server anmelden

            Connection.GetInstance().Login(config.Username, config.Password);

            radioReveiver = new RadioReceiver(config.ComPort);

            Console.ReadKey();
            closing = true;

            // Traces empfangen und an Server schicken

        }
    }
}
