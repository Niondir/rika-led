using System;
using System.Collections.Generic;
using System.Text;

namespace StoreServer.Radio
{
    public enum SignMode
    {
        Ad = 0,
        Price = 1
    }

    public class SetSignModePacket : LampPacket
    {
        public SetSignModePacket(SignMode mode) 
            : base(LampCommand.SetSignMode, ((int)mode).ToString())
        {
        }
    }
}
