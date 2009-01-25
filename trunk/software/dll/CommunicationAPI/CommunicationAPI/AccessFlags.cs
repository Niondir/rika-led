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
        User = 1,
        Product = 2,
        Ads = 4
    }
}
