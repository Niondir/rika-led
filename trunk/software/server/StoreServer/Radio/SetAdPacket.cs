using System;
using System.Collections.Generic;
using System.Text;
using StoreServer.Data;

namespace StoreServer.Radio
{
    public class SetAdPacket : LampPacket
    {
        public SetAdPacket(Advertisement ad)
            : base(LampCommand.SetAd, ad.Region.Id.ToString(), ad.Text[0], ad.Text[1], ad.Text[2], ad.Text[3])
        {
            this.targetId = ad.Region.Id.ToString();
        }
    }
}
