using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace StoreServer.Radio
{
    /// <summary>
    /// Change the address of the Xbee controller where to send in future.
    /// Takes 60ms depending on the xbee settings!
    /// </summary>
    public class AddressPacket : SerialPacket
    {
        private string address;

        /// <summary>
        /// 
        /// </summary>
        private Byte[] swtichModeCmd;

        /// <summary>
        /// The new address
        /// </summary>
        public string Address { get { return address; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        public AddressPacket(string address)
        {
            this.address = address;
            this.swtichModeCmd = Encode("+++");
            this.sendBytes = Encode(String.Format("ATDH0,DL{0},CN\r", address));
        }

        /// <summary>
        /// Write the packet to the com port
        /// </summary>
        /// <param name="port"></param>
        /// <param name="bytes"></param>
        protected override void Write(SerialPort port, Byte[] bytes)
        {
            //guard time, sending +++ to switch to cmd mode
            Thread.Sleep(20);
            port.Write(swtichModeCmd, 0, swtichModeCmd.Length);
            
            Thread.Sleep(20);
            //setup the destination id
            port.Write(bytes, 0, sendBytes.Length);
            Thread.Sleep(20);

            //Debug.WriteLine(String.Format("Set address to {0}",address));
        }
    }
}
