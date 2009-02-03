using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace StoreServer.Radio
{
    public abstract class SerialPacket
    {
        protected Byte[] sendBytes;

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

        protected virtual void Write(SerialPort port, Byte[] bytes)
        {
            string msg = Decode(bytes);
            if (this is LampPacket)
            {
                Console.WriteLine("Sending: " + msg + " to " + ((LampPacket)this).TargetId);
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

        public override string ToString()
        {
            return base.ToString() + "\n" + Decode(sendBytes) + "\n";
        }
    }
}
