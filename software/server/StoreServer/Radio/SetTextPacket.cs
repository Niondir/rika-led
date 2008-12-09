using System;
using System.Collections.Generic;
using System.Text;
using StoreServer.Data;

namespace StoreServer.Radio
{
    public class SetTextPacket : LampPacket
    {
        public SetTextPacket(Sign sign)
            : base(LampCommand.SetText, sign.Id.ToString(), sign.Text)
        {
        }
    }
}
