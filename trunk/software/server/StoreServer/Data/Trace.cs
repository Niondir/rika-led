using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.DataTypes;
using System.Data.Odbc;

namespace StoreServer.Data
{
    public class Trace
    {

        public Trace(TraceData trace)
        {
            
        }

        public void Save(OdbcConnection connection)
        {
            // TODO: Not implemented: Save Trace
            throw new Exception("Not implemented");
        }

        public static List<Trace> Load(OdbcConnection connection)
        {
            // TODO: Not implemented: Load Trace
            throw new Exception("Not implemented");
        }

        public void Delete(OdbcConnection connection)
        {
            // TODO: Not implemented: Delete Trace
            throw new Exception("Not implemented");
        }
    }
}
