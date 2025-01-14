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
using System.Runtime.InteropServices;


using System.Data;
using System.Xml;
using StoreServer.GUI;
using System.Windows.Forms;
/* TODO: Program.cs
 * 
 * */

namespace StoreServer
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private static Thread thread;
        private static Thread guiThread;
        private static Process process;
        private static Assembly assembly;
        private static string baseDirectory;
        private static string exePath;
        private static ClientManager userManager;
        private static ClientHandler clientHandler;
        private static RadioManager radioManager;
        private static MultiTextWriter multiConOut;
        private static FormConsole formConsole;
        private static ConsoleHandler consoleHandler;

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
        public static ConsoleHandler ConsoleHandler { get { return consoleHandler; } }


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

            Console.Title = "Store Server";
            Console.SetOut(multiConOut = new MultiTextWriter(new FileLogger("Logs\\last.log", false)));

            guiThread = new Thread(new ThreadStart(GuiThread));
            guiThread.Start();

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

            consoleHandler = new ConsoleHandler();

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
                ConsoleVisibility(true);
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            try
            {
                Console.WriteLine("Server: shuting down...");
                closing = true;
                Thread.Sleep(500);

                httpService.Abort();

                Console.WriteLine("Server: Closed");
                //Console.WriteLine("Server: Pres any key to exit");
                //Console.ReadKey();
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        public static void Close()
        {
            closing = true;
            Set();
        }

        public static void ConsoleVisibility(bool visible)
        {
            //Sometimes System.Windows.Forms.Application.ExecutablePath works for the caption depending on the system you are running under.          
            IntPtr hWnd = FindWindow(null, Console.Title);

            if (hWnd != IntPtr.Zero)
            {
                if (!visible)
                    //Hide the window                   
                    ShowWindow(hWnd, 0); // 0 = SW_HIDE        
                else
                    //Show window again                   
                    ShowWindow(hWnd, 1); //1 = SW_SHOWNORMA          
            }
        }

        public static void GuiThread()
        {
            while (!closing)
            {
                Program.ConsoleVisibility(false);
                formConsole = new FormConsole();
                multiConOut.Add(formConsole.Out);
                Application.Run(formConsole);
                multiConOut.Remove(formConsole.Out);
                Program.ConsoleVisibility(true);
                Close();
             }
        }



    }

}
