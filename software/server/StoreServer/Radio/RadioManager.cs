using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using StoreServer.Data;

/* TODO: RedioManager.cs
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

        private string destination = "-1";

        public string Destination
        {
            get { return destination; }
            set
            {
                if (destination != value)
                {
                    Send(new AddressPacket(value));
                    destination = value;
                }
            }
        }

        public RadioManager(string portName, DataManager dataManager)
        {
            sendQueue = new Queue<SerialPacket>();
            serialPort = new SerialPort(portName, 9600);

            this.dataManager = dataManager;

            Connect();

            sendThread = new Thread(new ThreadStart(sendLoop));
            sendThread.Name = "SerialSendThread";
            sendThread.Start();

            Console.WriteLine("Starting the send timer");
            sendTimer = new Timer(sendTimerCallback);
            sendTimer.Change(1000, 5000);
        }

        private void Connect()
        {
            try
            {
                serialPort.Open();
                Console.WriteLine("Opened serial port on " + serialPort.PortName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Add one packet to the send queue
        /// </summary>
        /// <param name="packet"></param>
        public void Send(SerialPacket packet)
        {
            lock (sendQueue)
            {
                sendQueue.Enqueue(packet);
            }
        }

        /// <summary>
        /// Everytime the server should send all infos from the database
        /// Have to send in "best order" for the lamps and signs.
        /// </summary>
        /// <param name="target"></param>
        private void sendTimerCallback(object target)
        {
            if (Program.Closing)
            {
                if (target is Timer)
                {
                    Debug.WriteLine("send timer disposed");
                    ((Timer)target).Dispose();
                    return;
                }
                    
            }

            // Alle Produkte raussuchen
            List<Product> products = Product.Load(dataManager.Connection);
            // Alle Webungen raussuchen
            List<Advertisement> ads = Advertisement.Load(dataManager.Connection);

            // Alle Produkte und Werbungen in packete packen
            Dictionary<string, Queue<LampPacket>> packetList = new Dictionary<string, Queue<LampPacket>>();
            List<string> targetList = new List<string>();

            foreach (Product p in products)
            {
                SetPricePacket packet = new SetPricePacket(p);

                if (packetList.ContainsKey(packet.TargetId)) 
                {
                     packetList[packet.TargetId].Enqueue(packet);
                }
                else 
                {
                    packetList[packet.TargetId] = new Queue<LampPacket>();
                    packetList[packet.TargetId].Enqueue(packet);
                }
            }

            foreach (Advertisement a in ads)
            {
                SetAdPacket packet = new SetAdPacket(a);
                
                if (packetList.ContainsKey(packet.TargetId)) {
                    packetList[packet.TargetId].Enqueue(packet);
                }
                else {
                    packetList[packet.TargetId] = new Queue<LampPacket>();
                    packetList[packet.TargetId].Enqueue(packet);
                }
            }


            Debug.WriteLine("--- Sending ---");
            Debug.WriteLine("Sending: " + ads.Count + " ads AND " + products.Count + " products");

            // Extra liste für die ziel lampen benötigt
            targetList.AddRange(packetList.Keys);

            // Alle lampen abwechselnd beliefern
            while (targetList.Count > 0) // solange noch ziele vorhanden
            {
                // Für jede Lampe ein paket
                foreach (string t in targetList)
                {
                    Debug.WriteLine("Send a packet to Lamp {0}", t);
                    Send(packetList[t].Dequeue());
                }

                // alle leeren Lampen aus der targetList entfernen
                foreach (string t in packetList.Keys)
                {
                    if (packetList[t].Count == 0)
                    {
                        Debug.WriteLine("Lamp {0} got all packets", t);
                        targetList.Remove(t);
                    }
                }
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

                            if (p is LampPacket)
                            {
                                if (((LampPacket)p).TargetId != destination)
                                {
                                    AddressPacket addressPacket = new AddressPacket(((LampPacket)p).TargetId);
                                    Debug.WriteLine("RadioManager: <online> sending: " + addressPacket.ToString());
                                    addressPacket.Send(serialPort);
                                }
                            }
                            
                            p.Send(serialPort);
                            Thread.Sleep(200);
                        }

                        else
                        {
#if DEBUG
                            SerialPacket p = sendQueue.Dequeue();
                            Debug.WriteLine("RadioManager: <offline> sending: " + p.ToString());
                            Thread.Sleep(200);
#else
                            //try to reconnect
                            Console.WriteLine("serial port is closed, try to reconnect");
                            Connect();
#endif
                        }

                    }
                }
            }

            Debug.WriteLine("Leaving SerialSend Thread");
        }
    }
}
