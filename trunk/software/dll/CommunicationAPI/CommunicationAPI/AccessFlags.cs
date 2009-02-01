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
        Ads = 4,
        Traces = 8,
        Authenticated = 16,
        Network = 32,
        Regions = AccessFlags.Product | AccessFlags.Ads,
        All = int.MaxValue
    }
}
