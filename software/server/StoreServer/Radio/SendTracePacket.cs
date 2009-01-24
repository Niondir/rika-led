using System;
using System.Collections.Generic;
using System.Text;

namespace StoreServer.Radio
{
    public class SendTracePacket : LampPacket
    {
        public SendTracePacket(string lampId, bool enabled)
            : base(LampCommand.SendTrace, enabled ? "1" : "0")
        {
            this.targetId = lampId;
        }
    }
}
