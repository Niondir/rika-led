using System;
using System.Collections.Generic;
using System.Text;

namespace StoreServer.Radio
{
    public class ResetLampBufferPacket : LampPacket
    {
        public ResetLampBufferPacket()
            : base(LampCommand.ResetLampBuffer)
        {
        }
    }
}
