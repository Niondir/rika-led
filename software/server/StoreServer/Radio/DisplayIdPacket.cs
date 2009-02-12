using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.Radio;

namespace StoreServer.Radio
{
    /// <summary>
    /// Tell the lamp to broadcast a "schowStatus" message to all signs
    /// </summary>
    public class DisplayIdPacket : LampPacket
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lampId"></param>
        public DisplayIdPacket(string lampId)
            : base(LampCommand.DisplayId)
        {
            this.targetId = lampId;
        }
    }
}
