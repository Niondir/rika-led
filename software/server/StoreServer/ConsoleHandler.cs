using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CommunicationAPI.DataTypes;
using StoreServer.Radio;
using StoreServer.Data;

namespace StoreServer
{
    public class ConsoleHandler
    {
        private Queue<string> queue;
        private Thread thread;

        public ConsoleHandler()
        {
            queue = new Queue<string>();

            thread = new Thread(new ThreadStart(ConsoleListen));
            thread.Name = "Console Listener Thread";
            thread.Start();
        }

        public void ConsoleListen()
        {
            while (!Program.Closing)
            {
                string input = Console.ReadLine();

                lock (queue)
                {
                    queue.Enqueue(input);
                    Program.Set();
                }
            }

            Debug.WriteLine("Console Listener Thread: Closing");
        }

        public void Slice() {
            if (queue.Count > 0)
            {
                string msg = String.Empty;
                lock (queue)
                {
                    msg = queue.Dequeue();
                }

                string[] tokens = msg.ToLower().Split(new char[] {' '});
                HandleCommand(tokens);
            }
        }


        private SessionData session;
        private void HandleCommand(string[] tokens) {
            string command = tokens[0];

            try
            {
                switch (command)
                {
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
                        Program.ClientHandler.AddRegion(session, new RegionData(1, "foobar"));
                        break;
                    case "addproduct":
                        Program.ClientHandler.AddProduct(session, new ProductData(new SignData(int.Parse(tokens[1]), new RegionData(1234, "testregion")), tokens[2], double.Parse(tokens[3])));
                        break;
                    case "address":
                        Program.RadioManager.Destination = tokens[1];
                        break;
                    case "sendtrace":
                        Program.RadioManager.Send(new SendTracePacket(bool.Parse(tokens[1])));
                        break;
                    case "settext":
                        RegionData region = new RegionData(1, "fooRegion");
                        SignData sign = new SignData(int.Parse(tokens[1]), region);
                        Product product = new Product(new ProductData(sign, tokens[2], 16.22));
                        //s.Text = tokens[2];
                        Program.RadioManager.Send(new SetPricePacket(product));
                        break;
                    case "packet":
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
                    Program.RadioManager.Send(new SendTracePacket(bool.Parse(args[2])));
                    break;
                case "resetlampbuffer":
                    Program.RadioManager.Send(new ResetLampBufferPacket());
                    break;
                case "setsignmode":
                    if (args[2] == "ad")
                        Program.RadioManager.Send(new SetSignModePacket(SignMode.Ad));
                    else if (args[2] == "price")
                        Program.RadioManager.Send(new SetSignModePacket(SignMode.Price));
                    break;
                case "setlampid":
                    Program.RadioManager.Send(new SetLampIdPacket(args[2]));
                    break;
            }
        }
    }
}
