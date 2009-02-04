using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace StoreServer.Radio
{
    public class AddressPacket : SerialPacket
    {
        private string address;
        private Byte[] swtichModeCmd;

        public string Address { get { return address; } }

        public AddressPacket(string address)
        {
            this.address = address;
            this.swtichModeCmd = Encode("+++");
            this.sendBytes = Encode(String.Format("ATDH0,DL{0},CN\r", address));
        }

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
