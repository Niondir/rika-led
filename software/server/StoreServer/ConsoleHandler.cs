using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

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
            //ThreadPool.QueueUserWorkItem(new WaitCallback(ConsoleListen));
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
            

            //ThreadPool.QueueUserWorkItem(new WaitCallback(ConsoleListen));
        }

        public void Slice() {
            if (queue.Count > 0)
            {
                string msg = String.Empty;
                lock (queue)
                {
                    msg = queue.Dequeue();
                }

                //Console.WriteLine("Sended: " + msg + " in Thread: " + Thread.CurrentThread.Name);

                string[] tokens = msg.Split(new char[] {' '});
                HandleCommand(tokens);

                

            }
        }

        private void HandleCommand(string[] tokens) {
            string command = tokens[0];

            switch (command)
            {
                case "login":
                    try
                    {
                        Program.ClientHandler.Login(new CommunicationAPI.DataTypes.UserData(tokens[1], tokens[2]));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "bar":
                    Console.WriteLine("baz");
                    break;
                default:
                    Console.WriteLine("Unknown Command");
                    break;
            }
        }
    }
}
