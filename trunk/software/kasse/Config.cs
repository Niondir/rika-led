using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Kasse
{
    [XmlRoot("Config")]
    public class Config
    {
        [XmlElement("ComPort")]
        public string ComPort { get; set; }

        [XmlElement("Username")]
        public string Username { get; set; }

        [XmlElement("Password")]
        public string Password { get; set; }

        public Config()
        {
            this.ComPort = "NONE";
            this.Username = "";
            this.Password = "";
            
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
