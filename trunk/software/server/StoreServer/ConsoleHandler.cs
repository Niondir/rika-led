using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CommunicationAPI.DataTypes;
using StoreServer.Radio;
using StoreServer.Data;

namespace StoreServer
{
    /// <summary>
    /// Handles the console input as commands
    /// </summary>
    public class ConsoleHandler
    {
        /// <summary>
        /// The input queue
        /// </summary>
        private Queue<string> queue;
        private Thread thread;

        /// <summary>
        /// 
        /// </summary>
        public ConsoleHandler()
        {
            queue = new Queue<string>();

            thread = new Thread(new ThreadStart(ConsoleListen));
            thread.Name = "Console Listener Thread";
            thread.Start();
        }

        /// <summary>
        /// The main thread
        /// </summary>
        public void ConsoleListen()
        {
            while (!Program.Closing)
            {
                if (Console.KeyAvailable)
                {
                    string input = Console.ReadLine();

                    lock (queue)
                    {
                        queue.Enqueue(input);
                        Program.Set();
                    }
                }
                else
                {
                    // Stop using ~50% cpu ;)
                    Thread.Sleep(50);
                }
                
            }

            Debug.WriteLine("Console Listener Thread: Closing");
        }

        /// <summary>
        /// Do a slice of work
        /// </summary>
        public void Slice() {
            if (queue.Count > 0)
            {
                string msg = String.Empty;
                lock (queue)
                {
                    msg = queue.Dequeue();
                }

                HandleCommand(msg);
            }
        }

        /// <summary>
        /// Handle a string as command
        /// </summary>
        /// <param name="msg"></param>
        public void HandleCommand(string msg)
        {
            string[] tokens = msg.ToLower().Split(new char[] { ' ' });
            HandleCommand(tokens);
        }

        private SessionData session;
        private void HandleCommand(string[] tokens) {
            string command = tokens[0];

            try
            {
                switch (command)
                {
                    case "port":
                        Program.RadioManager.PortName = tokens[1];
                        break;
                    case "sendtimer":
                        Program.RadioManager.SendTimer.Change(int.Parse(tokens[1]), int.Parse(tokens[1]));
                        break;
                    case "login":
                        try
                        {
                            if (tokens.Length > 1)
                                session = Program.ClientHandler.Login(new UserData(tokens[1], tokens[2]));
                            else
                                session = Program.ClientHandler.Login(new UserData("gast", "gast"));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "adduser":
                        UserData user = new UserData("holger", "hogerson");
                        user.Role = new RoleData("foo");

                        Program.ClientHandler.AddUser(session, user);
                        break;
                    case "addregion":
                        Program.ClientHandler.AddRegion(session, new RegionData("1", "foobar"));
                        break;
                    case "addproduct":
                        Program.ClientHandler.AddProduct(session, new ProductData(new SignData(int.Parse(tokens[1]), new RegionData("1234", "testregion")), tokens[2], double.Parse(tokens[3])));
                        break;
                    case "address":
                        Program.RadioManager.Destination = tokens[1];
                        break;
                    case "sendtrace":
                        Program.RadioManager.Send(new SendTracePacket(tokens[1], bool.Parse(tokens[2])));
                        break;
                    case "csum":
                        Console.WriteLine(GetChecksum(tokens[1]));
                        break;
                    case "settext":
                        RegionData region = new RegionData("1", "fooRegion");
                        SignData sign = new SignData(int.Parse(tokens[1]), region);
                        Product product = new Product(new ProductData(sign, tokens[2], 16.22));
                        //s.Text = tokens[2];
                        Program.RadioManager.Send(new SetPricePacket(product));
                        break;
                    case "p":
                        SendPacket(tokens);
                        break;
                    default:
                        Console.WriteLine("Unknown Command");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SendPacket(string[] args)
        {
            //packet <name> <param>
            switch (args[1])
            {
                case "sendtrace":
                    Program.RadioManager.Send(new SendTracePacket(args[2], bool.Parse(args[3])));
                    break;
                case "clr":
                    Program.RadioManager.Send(new ResetLampBufferPacket(args[2]));
                    break;
                case "setlampid":
                    Program.RadioManager.Send(new SetLampIdPacket(args[2], args[3]));
                    break;
                case "showid":
                    Program.RadioManager.Send(new DisplayIdPacket("FFFF"));
                    break;
                default:
                    Console.WriteLine("Unknown Packet");
                    break;
            }
        }

        private string GetChecksum(string str)
        {
            byte chkSum = 0;
            foreach (char c in str)
            {
                chkSum += (byte)c;
            }

            return chkSum.ToString();
        }
    }
}
