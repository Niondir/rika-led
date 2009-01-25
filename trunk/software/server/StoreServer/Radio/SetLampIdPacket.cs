using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.Radio;

namespace StoreServer.Radio
{
    public class SetLampIdPacket : LampPacket
    {
        public SetLampIdPacket(string oldLampId, string newLampId)
            : base(LampCommand.SetLampId, newLampId)
        {
            this.targetId = oldLampId;
        }
    }
}
