using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunicationAPI.Interface
{
    public enum ErrorCodes
    {
        XmlRpcInternal = 0,
        NotImplemented = 1,
        InvalidLogin = 101,
        AccessDenined = 102,
        DBWriteError = 201
    }
}
