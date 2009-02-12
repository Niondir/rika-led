using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace StoreServer.Radio
{
    /// <summary>
    /// Any packet for the serial connection have to be of this type
    /// </summary>
    public abstract class SerialPacket
    {
        /// <summary>
        /// the bytes we send to the com port. Use Encode(string) to greate the byte array
        /// </summary>
        protected Byte[] sendBytes;

        /// <summary>
        /// Write this packet to the serial port
        /// </summary>
        /// <param name="port">SerialPort to use</param>
        /// <returns></returns>
        public bool Send(SerialPort port)
        {
            try
            {
                Write(port, sendBytes);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Write this packet to the serial port
        /// </summary>
        /// <param name="port">SerialPort to use</param>
        /// <param name="bytes">given byte array to send</param>
        protected virtual void Write(SerialPort port, Byte[] bytes)
        {
            string msg = Decode(bytes);
            if (this is LampPacket)
            {
                Console.WriteLine("Sending: " + msg + " to lamp " + ((LampPacket)this).TargetId);
            }
            else Debug.WriteLine("Sending: " + msg);

            port.Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Encode a given string to a byte array
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected virtual Byte[] Encode(string msg)
        {
            return Encoding.ASCII.GetBytes(msg);
        }

        /// <summary>
        /// Decode a given byte array to an ASCII String
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        protected virtual string Decode(Byte[] bytes)
        {
            return Encoding.ASCII.GetString(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Packet to string, for debug outputs
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + "\n" + Decode(sendBytes) + "\n";
        }
    }
}
