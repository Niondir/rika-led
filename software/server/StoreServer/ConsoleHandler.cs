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
                    case "address":
                        Program.RadioManager.Destination = int.Parse(tokens[1]);
                        break;
                    case "sendtrace":
                        Program.RadioManager.Send(new SendTracePacket(bool.Parse(tokens[1])));
                        break;
                    case "settext":
                        Sign s = new Sign(new SignData());
                        s.Id = int.Parse(tokens[1]);
                        //s.Text = tokens[2];
                        Program.RadioManager.Send(new SetTextPacket(s));
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
    }
}
