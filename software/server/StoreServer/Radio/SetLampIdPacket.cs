using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.Radio;

namespace StoreServer.Radio
{
    /// <summary>
    /// Cahnge the id of a lamp
    /// </summary>
    public class SetLampIdPacket : LampPacket
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldLampId"></param>
        /// <param name="newLampId"></param>
        public SetLampIdPacket(string oldLampId, string newLampId)
            : base(LampCommand.SetLampId, newLampId)
        {
            this.targetId = oldLampId;
        }
    }
}
