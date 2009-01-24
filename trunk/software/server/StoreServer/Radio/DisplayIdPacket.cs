using System;
using System.Collections.Generic;
using System.Text;

namespace StoreServer.Radio
{
    public class DisplayIdPacket : LampPacket
    {
        public DisplayIdPacket(string lampId)
            : base(LampCommand.DisplayId)
        {
            this.targetId = lampId;
        }
    }
}
