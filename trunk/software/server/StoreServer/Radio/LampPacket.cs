using System;
using System.Collections.Generic;
using System.Text;

namespace StoreServer.Radio
{
    public enum LampCommand : int
    {
        SendTrace = 1,
        SetAd = 2,
        ResetLampBuffer = 3,
        SetLampId = 5,
        SetSignId = 6,
        SetPrice = 7
    }

    public abstract class LampPacket : SerialPacket
    {
        protected string targetId = "0";
        public string TargetId { get { return targetId; } }

        public LampPacket(LampCommand command, params string[] parameters)
        {
            string cmdString = String.Format("{0}|{1}|", (int)command, String.Join("|", parameters));
            string chkSum = GetChecksum(cmdString);

            string sendString = String.Format("<{0}{1}>", cmdString, chkSum);

            this.sendBytes = Encode(sendString);
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
