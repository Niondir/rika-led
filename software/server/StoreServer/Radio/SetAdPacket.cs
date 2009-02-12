using System;
using System.Collections.Generic;
using System.Text;
using StoreServer.Data;
using CommunicationAPI.Radio;

namespace StoreServer.Radio
{
    /// <summary>
    /// Tell the lamp to broadcast an advertisement
    /// </summary>
    public class SetAdPacket : LampPacket
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ad"></param>
        public SetAdPacket(Advertisement ad)
            : base(LampCommand.SetAd, ad.Region.Id.ToString(), ad.Text[0], ad.Text[1], ad.Text[2], ad.Text[3])
        {
            this.targetId = ad.Region.Id.ToString();
        }
    }
}
