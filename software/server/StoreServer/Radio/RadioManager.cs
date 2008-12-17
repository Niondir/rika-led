using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using StoreServer.Data;

/* TODO:
 * - Keinen ebeneffekte wenn die Verbindung nicht auf gebaut werden kann. Queue trotzdem pflegen.
 * - Wenn offline, jede Sekunde versuchen die Verbindung wieder auf zu nehmen
 * 
 * */

namespace StoreServer.Radio
{
    public class RadioManager
    {
        private SerialPort serialPort;
        private Queue<SerialPacket> sendQueue;
        private Thread sendThread;
        private Timer sendTimer;

        private DataManager dataManager;

        private string destination;

        public string Destination
        {
            get { return destination; }
            set
            {
                Send(new AddressPacket(value));
                destination = value;
            }
        }

        public RadioManager(string portName, DataManager dataManager)
        {
            sendQueue = new Queue<SerialPacket>();
            serialPort = new SerialPort(portName, 9600);

            this.dataManager = dataManager;

            sendTimer = new Timer(sendTimerCallback, null, 0, 5000);            

            try
            {
                serialPort.Open();
                Debug.WriteLine("Opened serial port on " + portName);
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            sendThread = new Thread(new ThreadStart(sendLoop));
            sendThread.Name = "SerialSendThread";
            sendThread.Start();
        }

        public void Send(SerialPacket packet)
        {
            lock (sendQueue)
            {
                sendQueue.Enqueue(packet);
            }
        }

        private void sendTimerCallback(object target)
        {
            List<Product> products = Product.Load(dataManager.Connection);
            Debug.WriteLine("--- Sending ---");
            foreach (Product p in products)
            {
                Debug.WriteLine("Product: " + p.Name + " " + p.Price.ToString() + " to " + p.Sign.Region.Name + " (" + p.Sign.Region.Id + ")");
                SetTextPacket packet = new SetTextPacket(p);
                Send(packet);
            }
        }

        /// <summary>
        /// Thread to send SerialPackets as fast as possible
        /// </summary>
        private void sendLoop()
        {
            while (!Program.Closing)
            {
                if (sendQueue.Count > 0)
                {
                    lock (sendQueue)
                    {
                        if (serialPort.IsOpen)
                        {
                            SerialPacket p = sendQueue.Dequeue();
                            Debug.WriteLine("RadioManager: <online> sending: " + p.ToString());
                            p.Send(serialPort);
                        }
#if DEBUG
                        else 
                        {
                            SerialPacket p = sendQueue.Dequeue();
                            Debug.WriteLine("RadioManager: <offline> sending: " + p.ToString());
                            
                        }
#endif
                    }
                }
            }

            Debug.WriteLine("Leaving SerialSend Thread");
        }
    }
}
