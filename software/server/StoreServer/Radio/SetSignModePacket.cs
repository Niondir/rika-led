using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.Radio;

namespace StoreServer.Radio
{
    public enum SignMode
    {
        Ad = 0,
        Price = 1
    }

    public class SetSignModePacket : LampPacket
    {
        public SetSignModePacket(string lampId, SignMode mode) 
            : base(LampCommand.SetSignMode, ((int)mode).ToString())
        {
            this.targetId = lampId;
        }
    }
}
