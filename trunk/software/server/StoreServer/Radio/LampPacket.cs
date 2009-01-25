using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.Radio;

namespace StoreServer.Radio
{
    

    public abstract class LampPacket : SerialPacket
    {
        protected string targetId = "FFFF";
        public string TargetId { get { return targetId; } }

        public LampPacket(LampCommand command)
        {
            string cmdString = String.Format("{0}|", (int)command);
            string sendString = buildPacket(cmdString);
            send(sendString);
        }

        public LampPacket(LampCommand command, params string[] parameters)
        {
            string cmdString = String.Format("{0}|{1}|", (int)command, String.Join("|", parameters));
            string sendString = buildPacket(cmdString);
            send(sendString);
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

        private void send(string msg)
        {
            this.sendBytes = Encode(msg);
        }


        private string GetChecksum(string str)
        {
            int chkSum = 0;
            foreach (char c in str)
            {
                chkSum += (int)c;
            }

            return chkSum.ToString();
        }
    }
}
