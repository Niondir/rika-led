using System;
using System.Collections.Generic;
using System.Text;

namespace StoreServer
{
    class Debug
    {
        public static void WriteLine(string format, params object[] arg)
        {
            Debug.WriteLine(String.Format(format, arg));
        }

        public static void WriteLine(string value)
        {
#if DEBUG
            //Console.WriteLine(value);
#endif
        }
    }
}
