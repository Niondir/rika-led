using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace StoreClient
{
    public partial class FormPreferences : Form
    {

        public Preferences Value
        {
            get
            {
                Preferences pref = new Preferences();
                pref.Password = (string)textBoxPassword.Text;
                pref.ServerAddress = textBoxServerAddr.Text + ":" + textBoxPort.Text;
                pref.UserName = textBoxUsername.Text;
                pref.AutoLogin = checkBoxPort.Checked;
                return pref;
            }
            set
            {
                if (textBoxUsername.Text.Length > 0)
                {
                    textBoxPassword.Text = "Password";
                    textBoxPassword.Tag = value.Password;
                }
                textBoxUsername.Text = value.UserName;
                textBoxServerAddr.Text = value.ServerAddressOnly;
                textBoxPort.Text = value.ServerPortOnly;
                checkBoxPort.Checked = value.AutoLogin;
            }
        }

        public FormPreferences()
        {
            InitializeComponent();
            Value = Preferences.GetInstance();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Preferences.Save(this.Value);
        }

        private void buttonSaveAndClose_Click(object sender, EventArgs e)
        {
            buttonSave_Click(sender, e);
            this.Close();
        }

        private void checkBoxPort_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPort.Enabled = checkBoxPort.Checked;
        }

        private void textBoxPassword_Enter(object sender, EventArgs e)
        {
            textBoxPassword.Text = "";
        }
    }

    [Serializable]
    public class Preferences
    {
        public string ServerAddress { get; set; }
        public string ServerAddressOnly
        {
            get { return ServerAddress.Substring(0, ServerAddress.LastIndexOf(':')); }
        }
        public string ServerPortOnly
        {
            get
            {
                int dindex = ServerAddress.LastIndexOf(':');
                int length = ServerAddress.Length;
                return ServerAddress.Substring(ServerAddress.LastIndexOf(':')+1, ServerAddress.Length - ServerAddress.LastIndexOf(':')-1);
            }
        }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool AutoLogin { get; set; }

        static Preferences instance = null;
        public static Preferences GetInstance()
        {
            if (instance != null)
                return instance;
            
            if (!File.Exists("config.cfg"))
            {
                instance = new Preferences();
                instance.AutoLogin = false;
                instance.Password = "";
                instance.UserName = "";
                instance.ServerAddress = "http://127.0.0.1:11000";
            }
            else
            {
                BinaryFormatter bf = new BinaryFormatter();
                Stream reader = File.Open("config.cfg", FileMode.Open, FileAccess.Read);
                instance = (Preferences)bf.Deserialize(reader);
                reader.Close();
            }
            return instance;
        }
        public static void Save()
        {
            BinaryFormatter bf = new BinaryFormatter();
            Stream writer = File.Open("config.cfg", FileMode.Create);
            bf.Serialize(writer, instance);
            writer.Close();
        }
        public static void Save(Preferences prefs)
        {
            instance = prefs;
            Save();
        }
        public static Preferences Load()
        {
            return GetInstance();
        }
    }
}
