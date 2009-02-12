using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.Radio;

namespace StoreServer.Radio
{
    /// <summary>
    /// Reset the buffer of the lamp
    /// </summary>
    public class ResetLampBufferPacket : LampPacket
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lampId"></param>
        public ResetLampBufferPacket(string lampId)
            : base(LampCommand.ResetLampBuffer)
        {
            this.targetId = lampId;
        }
    }
}
