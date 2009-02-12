using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace StoreServer
{
    /// <summary>
    /// The Config file
    /// </summary>
    [XmlRoot("Config")]
    public class Config
    {
        /// <summary>
        /// The COM port with a xbee module
        /// </summary>
        [XmlElement("ComPort")]
        public string ComPort { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("SQLDriver")]
        public string SQLDriver { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("SQLServer")]
        public string SQLServer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("SQLDatabase")]
        public string SQLDatabase { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("SQLUser")]
        public string SQLUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("SQLPassword")]
        public string SQLPassword { get; set; }

        /// <summary>
        /// has defaukl parameters for the config
        /// </summary>
        public Config()
        {
            this.ComPort = "NONE";
            this.SQLDriver = "{MySQL ODBC 3.51 Driver}";
            this.SQLServer = "localhost";
            this.SQLDatabase = "ledcom";
            this.SQLUser = "user";
            this.SQLPassword = "password";
            
        }

        /// <summary>
        /// Save the config
        /// </summary>
        public static void Save() 
        {
            XmlSerializer xs = new XmlSerializer(typeof(Config));
            xs.Serialize(new XmlTextWriter("config.xml", Encoding.UTF8), new Config());
        }

        /// <summary>
        /// Load the config
        /// </summary>
        /// <returns></returns>
        public static Config Load() 
        {
            XmlSerializer xs = new XmlSerializer(typeof(Config));
            return (Config)xs.Deserialize(new XmlTextReader("config.xml"));
        }

    }
}
