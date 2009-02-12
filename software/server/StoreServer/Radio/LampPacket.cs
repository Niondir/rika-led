using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.Radio;

namespace StoreServer.Radio
{
    
    /// <summary>
    /// Any packet to a Lamp have to be of this type
    /// </summary>
    public abstract class LampPacket : SerialPacket
    {
        /// <summary>
        /// 
        /// </summary>
        protected string targetId = "FFFF";

        /// <summary>
        /// The target lamp id, FFFF is a bradcast
        /// </summary>
        public string TargetId { get { return targetId; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        public LampPacket(LampCommand command)
        {
            string cmdString = String.Format("{0}|", (int)command);
            string sendString = buildPacket(cmdString);
            PreparePacket(sendString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        public LampPacket(LampCommand command, params string[] parameters)
        {
            string cmdString = String.Format("{0}|{1}|", (int)command, String.Join("|", parameters));
            string sendString = buildPacket(cmdString);
            PreparePacket(sendString);
        }

        /// <summary>
        /// Generate a packet structure
        /// </summary>
        /// <param name="cmdString">a string: cmd|param|param| ...</param>
        /// <returns></returns>
        private string buildPacket(string cmdString)
        {
            string chkSum = GetChecksum(cmdString);
            return String.Format("<{0}{1}>", cmdString, chkSum);
        }

        private void PreparePacket(string msg)
        {
            this.sendBytes = Encode(msg);
        }


        private string GetChecksum(string str)
        {
            byte chkSum = 0;
            foreach (char c in str)
            {
                chkSum += (byte)c;
            }

            return chkSum.ToString();
        }
    }
}
