using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;

namespace StoreServer.Data
{
    public class DataManager
    {
        private OdbcConnection connection;

        public OdbcConnection Connection
        {
            get { return connection; }
        }

        public DataManager()
        {
            OdbcConnectionStringBuilder sb = new OdbcConnectionStringBuilder();
            sb.Driver = "{MySQL ODBC 3.51 Driver}";
            sb.Add("Server", "localhost");
            sb.Add("Database", "rika");
            sb.Add("User", "root");
            sb.Add("Password", "");
            sb.Add("Option", "3");

            connection = new OdbcConnection(sb.ToString());

            Console.WriteLine("Connecting to database with {0}:\nServer: {1}\nSchema: {2}\nUser: {3}\nPassword: <hidden>", 
                sb["Driver"], sb["Server"], sb["Database"], sb["User"]);
            
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            VeryfyDatabase();

            /* Workflow:
             * Es kommen Events für Datenbank anfragen an. Diese müssen behandelt werden.
             * Behandlung eines Events kann dazu führen, dass dem FunkManager aufträge gegeben werden.
             * 
             * */

            /* TODO:
             * - Datenbank anlegen
             * - Interface für die Datenbankzugriffe definieren
             * - Abstrakte Datenobjekte für die Schnitstelle
             * 
             * */

        }

        public void Slice()
        {
            // Gibt es Aufgaben die regelmäßig getan werden müssen?
        }

        private void VeryfyDatabase()
        {
            if (connection.State != System.Data.ConnectionState.Open) return;

            OdbcCommand command = connection.CreateCommand();
            command.CommandText = "SHOW TABLES;";
            
            OdbcDataReader reader = command.ExecuteReader();

            // Finde die info tabelle
            // lese version (wenn tabelle fehlt -> version = 0)
            // Führe alle SQL Files von gefundener Version bis Programm.Version aus
            while (reader.Read())
            {
                string table = reader.GetString(0);
                Debug.WriteLine("Table: " + table);

            }
            
        }

        private void CreateTables()
        {

        }
    }
}
