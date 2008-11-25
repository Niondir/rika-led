using System;
using System.Collections.Generic;
using System.Text;

namespace StoreServer.Data
{
    public class DataManager
    {
        public DataManager()
        {
            /* Workflow:
             * Es kommen Events für Datenbank anfragen an. Diese müssen behandelt werden.
             * Behandlung eines Events kann dazu führen, dass dem FunkManager aufträge gegeben werden.
             * 
             * */

        }

        public void Slice()
        {
            // Gibt es Aufgaben die regelmäßig getan werden müssen?
        }
    }
}
