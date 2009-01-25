using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunicationAPI.Radio
{
    public enum LampCommand : int
    {
        Invalid = 0,
        SendTrace = 1,
        SetAd = 2,
        ResetLampBuffer = 3,
        SetSignMode = 4,
        SetLampId = 5,
        DisplayId = 6,
        SetPrice = 7,
        TracePacket = 8
    }
}
