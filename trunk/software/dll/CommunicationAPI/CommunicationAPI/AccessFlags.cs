using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunicationAPI
{
    [Flags]
    public enum AccessFlags : int
    {
        None = 0,
        Guest = 1,
        Authenticated = 2
    }
}
