using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using StoreServer.WebService;
using StoreServer.Data;
using StoreServer.Radio;


using System.Data;
using System.Xml;
/* TODO: Program.cs
 * 
 * 2. Console (GUI)
 * Advertisement mit Timestamps & beschreibung
 * Ip adresse einstellbar
 * 
 * 
 * sign id 0 bedeutet Einkaufswagen!
 * */

namespace StoreServer
{
    class Program
    {
        private static Thread thread;
        private static Process process;
        private static Assembly assembly;
        private static string baseDirectory;
        private static string exePath;
        private static ClientManager userManager;
        private static ClientHandler clientHandler;
        private static RadioManager radioManager;
        private static MultiTextWriter multiConOut;

        private static bool unix = false;
        public static bool Unix { get { return unix; } }


        /// <summary>
        /// if true, the programm is shutting down, used for threads to stop
        /// </summary>
        private static bool closing = false;
        public static bool Closing { get { return closing; } }

        public static Assembly Assembly { get { return assembly; } set { assembly = value; } }
        public static Process Process { get { return process; } }
        public static Thread Thread { get { return thread; } }
        public static ClientManager UserManager { get { return userManager; } }
        public static ClientHandler ClientHandler { get { return clientHandler; } }
        public static RadioManager RadioManager { get { return radioManager; } }
        public static MultiTextWriter MultiConsoleOut { get { return multiConOut; } }

        public static string ExePath
        {
            get
            {
                if (exePath == null)
                {
                    exePath = Assembly.Location;
                }

                return exePath;
            }
        }

        public static string BaseDirectory
        {
            get
            {
                if (baseDirectory == null)
                {
                    try
                    {
                        baseDirectory = ExePath;

                        if (baseDirectory.Length > 0)
                            baseDirectory = Path.GetDirectoryName(baseDirectory);
                    }
                    catch
                    {
                        baseDirectory = "";
                    }
                }
                return baseDirectory;
            }
        }

        private static AutoResetEvent signal = new AutoResetEvent( true );
        public static void Set() { signal.Set(); }


        static void Main(string[] args)
        {
            thread = Thread.CurrentThread;
			process = Process.GetCurrentProcess();
			assembly = Assembly.GetEntryAssembly();

            if( thread != null )
				thread.Name = "Core Thread";

            if( BaseDirectory.Length > 0 )
				Directory.SetCurrentDirectory( BaseDirectory );

            Version ver = assembly.GetName().Version;

            Console.SetOut(multiConOut = new MultiTextWriter(Console.Out, new FileLogger("Logs\\last.log", false)));

            // Added to help future code support on forums, as a 'check' people can ask for to it see if they recompiled core or not
            Console.WriteLine("StoreServer - [http://code.google.com/p/rika-led/]");
            Console.WriteLine("Version {0}.{1}, Build {2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision);
			Console.WriteLine("Server: Running on .NET Framework Version {0}.{1}.{2}", Environment.Version.Major, Environment.Version.Minor, Environment.Version.Build);
           
            int platform = (int)Environment.OSVersion.Platform;
            if ((platform == 4) || (platform == 128))
            { // MS 4, MONO 128
                unix = true;
                Console.WriteLine("Server: Unix environment detected");
            }

            Config config = new Config();

            try
            {
                config = Config.Load();
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
                return;
            }

            DataManager dataManager = new DataManager(config);

            clientHandler = new ClientHandler(dataManager);
            HttpService httpService = new HttpService("http://127.0.0.1:11000/", clientHandler);


            radioManager = new RadioManager(config.ComPort, dataManager);
            userManager = new ClientManager();

            ConsoleHandler consoleHandler = new ConsoleHandler();

            try
            {

                while (signal.WaitOne())
                {
                    if (closing) break;
                    httpService.Slice();
                    consoleHandler.Slice();
                    Console.WriteLine("--- MainLoop ---");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            // TODO: Loop for async actions, linke console input

            Console.WriteLine("Server: shuting down...");
            closing = true;
            Thread.Sleep(500);
            Console.WriteLine("Server: Pres any key to exit");
            Console.ReadKey();

            httpService.Abort();
        }

        public static void Close()
        {
            closing = true;
            Set();
        }

        



    }

}
