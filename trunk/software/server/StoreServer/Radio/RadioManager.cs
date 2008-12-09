using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;

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

        private int destination;

        public int Destination
        {
            get { return destination; }
            set
            {
                Send(new AddressPacket(value));
                destination = value;
            }
        }

        public RadioManager(string portName)
        {
            sendQueue = new Queue<SerialPacket>();
            serialPort = new SerialPort(portName, 9600);

            try
            {
                serialPort.Open();
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
