using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace StoreServer
{
    public class ConsoleHandler
    {
        private Queue<string> queue;


        public ConsoleHandler()
        {
            queue = new Queue<string>();
            ThreadPool.QueueUserWorkItem(new WaitCallback(ConsoleListen));
        }

        public void ConsoleListen(Object stateInfo)
        {
            string input = Console.ReadLine();

            lock (queue)
            {
                queue.Enqueue(input);
                Program.Set();
            }

            ThreadPool.QueueUserWorkItem(new WaitCallback(ConsoleListen));

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
                switch (msg)
                {
                    case "foo": 
                        Console.WriteLine("bar");
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
}
