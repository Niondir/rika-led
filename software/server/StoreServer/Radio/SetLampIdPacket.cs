using System;
using System.Collections.Generic;
using System.Text;

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
