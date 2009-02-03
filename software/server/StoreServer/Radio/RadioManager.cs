using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using StoreServer.Data;

/* TODO: RedioManager.cs
 * 
 * */

namespace StoreServer.Radio
{
    public class RadioManager
    {
        private const int SEND_DELAY = 100;

        private SerialPort serialPort;
        private Queue<SerialPacket> sendQueue;
        private Thread sendThread;
        private Timer sendTimer;
        public string PortName;



        public Timer SendTimer
        {
            get { return sendTimer; }
        }

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
            this.PortName = portName;
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
                serialPort.PortName = PortName;
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

            if (sendQueue.Count > 50)
            {
                // Avoid a overloading of the send queue
                return;
            }

            // Alle Produkte raussuchen
            List<Product> products = Product.Load(dataManager.Connection);
            // Alle Webungen raussuchen
            List<Advertisement> ads_tmp = Advertisement.Load(dataManager.Connection);

            // nur aktuelle Werbung Zeigen
            List<Advertisement> ads = new List<Advertisement>();
            foreach (Advertisement ad in ads_tmp)
            {
                if (ad.StartTime.TimeOfDay <= DateTime.Now.TimeOfDay && ad.StopTime.TimeOfDay >= DateTime.Now.TimeOfDay && ad.StartDate <= DateTime.Now && ad.StopDate >= DateTime.Now)
                {
                    ads.Add(ad);
                }
            }

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


            //Debug.WriteLine("--- Send Loop ---");
            Debug.WriteLine("Sending: " + ads.Count + " ads & " + products.Count + " products");

            // Extra liste für die ziel lampen benötigt
            targetList.AddRange(packetList.Keys);

            // Alle lampen abwechselnd beliefern
            while (targetList.Count > 0) // solange noch ziele vorhanden
            {
                // Für jede Lampe ein paket
                foreach (string t in targetList)
                {
                    //Debug.WriteLine("Send a packet to Lamp {0}", t);
                    Send(packetList[t].Dequeue());
                }

                // alle leeren Lampen aus der targetList entfernen
                foreach (string t in packetList.Keys)
                {
                    if (packetList[t].Count == 0)
                    {
                        //Debug.WriteLine("Lamp {0} got all packets", t);
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
                            //Debug.WriteLine("RadioManager: <online> sending: " + p.ToString());

                            if (p is LampPacket)
                            {
                                if (((LampPacket)p).TargetId != destination)
                                {
                                    AddressPacket addressPacket = new AddressPacket(((LampPacket)p).TargetId);
                                    //Debug.WriteLine("RadioManager: <online> sending: " + addressPacket.ToString());
                                    if (addressPacket.Send(serialPort))
                                    {
                                        destination = addressPacket.Address;
                                    }
                                }
                            }

                            p.Send(serialPort);
                            Thread.Sleep(SEND_DELAY);
                        }
                        else
                        {
//#if DEBUG
 //                           SerialPacket p = sendQueue.Dequeue();
 //                           Debug.WriteLine("RadioManager: <offline> sending: " + p.ToString());
 //                           Thread.Sleep(SEND_DELAY);
//#else
                            //try to reconnect
                            Thread.Sleep(1000);
                            Console.WriteLine("Serial port is closed, try to reconnect ...");
                            Console.WriteLine(sendQueue.Count + " packets in queue");
                            Connect();
//#endif
                        }
                    }
                }
                else
                {
                    // Reduce cpu usages if queue empty
                    Thread.Sleep(50);
                }
            }
            try
            {
                serialPort.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Debug.WriteLine("Leaving SerialSend Thread");
        }
    }
}
