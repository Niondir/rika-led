using System;
using System.Collections.Generic;
using System.Text;

namespace StoreServer.Radio
{
    public enum LampCommand : int
    {
        SendTrace = 1,
        SetText = 2
    }

    public abstract class LampPacket : SerialPacket
    {
        public LampPacket(LampCommand command, params string[] parameters)
        {
            this.sendBytes = Encode(String.Format("<{0}|{1}>", (int)command, String.Join("|", parameters)));
        }
    }
}
