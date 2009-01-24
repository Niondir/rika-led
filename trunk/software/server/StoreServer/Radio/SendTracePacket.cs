using System;
using System.Collections.Generic;
using System.Text;

namespace StoreServer.Radio
{
    public class SendTracePacket : LampPacket
    {
        public SendTracePacket(bool enabled)
            : base(LampCommand.SendTrace, enabled ? "1" : "0")
        {
        }
    }
}
