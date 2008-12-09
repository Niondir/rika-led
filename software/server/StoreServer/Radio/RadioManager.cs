using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace StoreServer.Radio
{
    public class RadioManager
    {
        private SerialPort serialPort;
        private Queue<SerialPacket> sendQueue;
        private Thread sendThread;

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

        /// <summary>
        /// thread
        /// </summary>
        private void sendLoop()
        {
            while (!Program.Closing && serialPort.IsOpen)
            {

            }
        }

        private void send()
        {
            // umstaendlich aber noetig, ohne schnallt der xbee die befehle nicht
            Byte[] sendBytes1 = Encoding.ASCII.GetBytes("+++");
            Byte[] sendBytes2 = Encoding.ASCII.GetBytes("ATDH0,DL" + 0 + ",CN\r");
            //<packet>
            Byte[] sendBytes3 = Encoding.ASCII.GetBytes("hallo welt" + "\r\n");

            try
            {
                serialPort.Open();

                //guard time beachten, +++ senden um in cmd modus zu wechseln
                Thread.Sleep(20);
                serialPort.Write(sendBytes1, 0, sendBytes1.Length);
                //_serialPort.WriteLine("+++");

                Thread.Sleep(20);

                //command senden, genauer: einstellen der ziel id
                serialPort.Write(sendBytes2, 0, sendBytes2.Length);
                Thread.Sleep(20);

                //test per funk verschicken
                serialPort.Write(sendBytes3, 0, sendBytes3.Length);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        /* Workflow:
         * DataManager stellt anfragen (wer ist hier eig. egal)
         * Anfragen werden so schnell wie möglich behandelt. Wir brauchen einen Puffer um nicht schneller als die Schnittstelle zu werden
         * Darf nicht blockierend sein!
         * 
         * 
         * */

        /* TODO:
         * Ansprechen des Funkmoduls über die serielle Schnittstelle
         * 
         * */
    }
}
