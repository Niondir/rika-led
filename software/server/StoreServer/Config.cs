using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace StoreServer
{
    [XmlRoot("Config")]
    public class Config
    {
        [XmlElement("ComPort")]
        public string ComPort { get; set; }

        [XmlElement("SQLDriver")]
        public string SQLDriver { get; set; }

        [XmlElement("SQLServer")]
        public string SQLServer { get; set; }

        [XmlElement("SQLDatabase")]
        public string SQLDatabase { get; set; }

        [XmlElement("SQLUser")]
        public string SQLUser { get; set; }

        [XmlElement("SQLPassword")]
        public string SQLPassword { get; set; }

        public Config()
        {
            this.ComPort = "NONE";
            this.SQLDriver = "{MySQL ODBC 3.51 Driver}";
            this.SQLServer = "localhost";
            this.SQLDatabase = "ledcom";
            this.SQLUser = "user";
            this.SQLPassword = "password";
            
        }

        public static void Save() 
        {
            XmlSerializer xs = new XmlSerializer(typeof(Config));
            xs.Serialize(new XmlTextWriter("config.xml", Encoding.UTF8), new Config());
        }

        public static Config Load() 
        {
            XmlSerializer xs = new XmlSerializer(typeof(Config));
            return (Config)xs.Deserialize(new XmlTextReader("config.xml"));
        }

    }
}
