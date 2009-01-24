using System;
using System.Collections.Generic;
using System.Text;

namespace StoreServer.Radio
{
    public class SetLampIdPacket : LampPacket
    {
        public SetLampIdPacket(string lampId)
            : base(LampCommand.SetLampId, lampId)
        {
        }
    }
}
