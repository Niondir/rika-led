using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.Radio;

namespace StoreServer.Radio
{
    public class ResetLampBufferPacket : LampPacket
    {
        public ResetLampBufferPacket(string lampId)
            : base(LampCommand.ResetLampBuffer)
        {
            this.targetId = lampId;
        }
    }
}
