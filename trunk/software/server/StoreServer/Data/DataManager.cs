using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;
using System.IO;
using CommunicationAPI.DataTypes;

namespace StoreServer.Data
{
    public class DataManager
    {
        private OdbcConnection connection;

        public OdbcConnection Connection
        {
            get { return connection; }
        }

        public DataManager(Config cfg)
        {
            OdbcConnectionStringBuilder sb = new OdbcConnectionStringBuilder();
            sb.Driver = cfg.SQLDriver; // {MySQL ODBC 3.51 Driver}
            sb.Add("Server", cfg.SQLServer);
            sb.Add("Database", cfg.SQLDatabase);
            sb.Add("User", cfg.SQLUser);
            sb.Add("Password", cfg.SQLPassword);
            sb.Add("Option", "3");
            //Debug.WriteLine(sb.ConnectionString);
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
            bool found = false;
            while (reader.Read())
            {
                string table = reader.GetString(0);
                if (table == "led_informations")
                {
                    found = true;
                    break;
                }
            }
            reader.Close();

            Version version = new Version(0, 0, 0, 0);

            if (!found)
            {
                Debug.WriteLine("Information table not found");

                OdbcTransaction transaction = connection.BeginTransaction();

                try {
                    command.Transaction = transaction;
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE `rika`.`led_informations` (
                                          `key` VARCHAR(255) NOT NULL,
                                          `value` VARCHAR(255) NOT NULL,
                                          PRIMARY KEY (`key`)
                                          );";
                    command.ExecuteNonQuery();

                    command.CommandText = @"INSERT INTO `rika`.`led_informations` (`key`, `value`) VALUES('version','0.0.0.0');";
                    command.ExecuteNonQuery();

                    transaction.Commit();

                    Debug.WriteLine("Information table created");
                }
                catch (Exception ex) {
                    transaction.Rollback();
                    Debug.WriteLine("Created informations table, Error: " + ex.Message);
                }
            }

            // Get version
            command.CommandText = "SELECT `value` FROM `rika`.`led_informations` WHERE `key` = 'version'";
            reader = command.ExecuteReader();

           
            while (reader.Read())
            {
                string v = reader.GetString(0);
                try
                {
                    version = new Version(v);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            BuildDatabase(ref version);

        }

        /// <summary>
        /// Erstelle die Datenbank aus den SQL Dateien
        /// </summary>
        /// <param name="version"></param>
        private void BuildDatabase(ref Version version)
        {
            string dir = Program.BaseDirectory + "/sql";
            if (!Directory.Exists(dir))
                return;

            SortedDictionary<string, Version> files = new SortedDictionary<string, Version>();

            foreach (string f in Directory.GetFiles(dir))
            {
                string file = Path.GetFileName(f);
                Version fileversion = new Version(file.Split('_')[0]);
                if (fileversion > version)
                {
                    //Debug.WriteLine("fileversion = " + fileversion.ToString());
                    files.Add(f, fileversion);
                    
                }
            }

            OdbcCommand command = connection.CreateCommand();

            foreach (string f in files.Keys)
            {
                Debug.WriteLine("Executing SQL file: " + f);

                FileStream fs = File.Open(f, FileMode.Open);
                TextReader reader = new StreamReader(fs);

                string content = reader.ReadToEnd();
                fs.Close();

                string[] querys = content.Split(new Char[] {';'}, StringSplitOptions.RemoveEmptyEntries);

                command.Transaction = connection.BeginTransaction();
                try
                {
                    foreach (string q in querys)
                    {
                        if (string.IsNullOrEmpty(q.Trim())) continue;
                        command.CommandText = q;
                        command.Prepare();
                        command.ExecuteNonQuery();
                    }


                    if (files[f] > version)
                        version = files[f];

                    command.CommandText = "UPDATE `rika`.`led_informations` SET `value` = ? WHERE `key` = 'version';";
                    command.Parameters.Add("version", OdbcType.VarChar).Value = version.ToString();
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    Debug.WriteLine("Database updated to Version: " + version.ToString());

                    command.Transaction.Commit();

                }
                catch (Exception ex)
                {
                    // Try to rollback the executed SQL file
                    command.Transaction.Rollback();
                    Console.WriteLine(ex.Message);
                }

                
            }

        }

        public void AddUser(UserData user)
        {
            OdbcCommand command = connection.CreateCommand();

            command.CommandText = "INSERT INTO led_users (roles_id, login, password) VALUES (?, ?, ?)";
            command.Parameters.AddWithValue("roles_id", 0);
            command.Parameters.AddWithValue("login", user.Username);
            command.Parameters.AddWithValue("password", user.Password);
        }
    }
}
