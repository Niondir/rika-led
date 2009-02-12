using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.Radio;

namespace StoreServer.Radio
{
    /// <summary>
    /// Enable / disable the send trace mode for a lamp. 
    /// In send trace mode, the lamp send "send trace to cashregister" to the signs
    /// </summary>
    public class SendTracePacket : LampPacket
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lampId"></param>
        /// <param name="enabled"></param>
        public SendTracePacket(string lampId, bool enabled)
            : base(LampCommand.SendTrace, enabled ? "1" : "0", "FFFF")
        {
            this.targetId = lampId;
        }
    }
}
