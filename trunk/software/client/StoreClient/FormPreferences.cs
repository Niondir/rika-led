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
    /// <summary>
    /// Dialog Fenster für Einstellungen des MyStore Managers
    /// </summary>
    public partial class FormPreferences : Form
    {
        private static string defaultPort = "11000";
        
        /// <summary>
        /// Gibt die dem Dialog entsprechenden Einstellungen zurück oder legt diese fest
        /// </summary>
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

        /// <summary>
        /// Erstellt ein neues Dialogfenster für Produkte und initialisiert die Komponenten
        /// Zusätzlich werden gegebenenfalls vorhandene Einstellungen aus der cfg Datei ausgelesen
        /// </summary>
        public FormPreferences()
        {
            InitializeComponent();
            Value = Preferences.GetInstance();
        }

        /// <summary>
        /// Speichert die aktuell ausgewählten Einstellungen
        /// </summary>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            Preferences.Save(this.Value);
        }

        /// <summary>
        /// Speichert die aktuell ausgewählten Einstellungen und schließt das Dialogfenster
        /// </summary>
        private void buttonSaveAndClose_Click(object sender, EventArgs e)
        {
            buttonSave_Click(sender, e);
            this.Close();
        }

        /// <summary>
        /// Ermöglicht die Einstellung eines anderen Ports oder verhindert diese
        /// </summary>
        private void checkBoxPort_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPort.Enabled = checkBoxPort.Checked;
            if (!checkBoxPort.Checked)
                textBoxPort.Text = defaultPort;
        }

        /// <summary>
        /// Löscht den Inhalt der Passwort Textbox bei Betreten
        /// </summary>
        private void textBoxPassword_Enter(object sender, EventArgs e)
        {
            textBoxPassword.Text = "";
        }

        /// <summary>
        /// Überprüft bei Eingabe in die Textbox, ob die Eingabe gültig ist, also einer HEX Zahl entspricht
        /// <param name="e">Beinhaltet Informationen über die gedrückte Taste</param>
        /// <param name="sender">unbedeutend</param>
        /// </summary>
        private void CheckHex(object sender, KeyPressEventArgs e)
        {
            string valid = "0123456789abcdefABCDEF";
            string lower = "abcdef";
            if (e.KeyChar == '\r' && textBoxNewID.Text.Length > 0 && textBoxOldID.Text.Length > 0)
            {
                e.Handled = true;
                buttonSendID_Click(sender, null);
            }
            if (e.KeyChar == (char)0x08)
                return;
            if (!valid.Contains(new string(e.KeyChar, 1)))
                e.Handled = true;
            if (lower.Contains(new string(e.KeyChar, 1)))
            {
                e.KeyChar = new string(e.KeyChar, 1).ToUpper()[0];
            }
        }

        /// <summary>
        /// Veranlasstden Server ads Show ID Kommando abzusetzen
        /// <seealso cref="Connection"/>
        /// </summary>
        private void buttonShowID_Click(object sender, EventArgs e)
        {
            Connection.GetInstance().ShowID();
        }

        /// <summary>
        /// Ändert die ID des Lampencontrolles [textBoxOldID] in [textBoxNewID]
        /// <seealso cref="Connection"/>
        /// </summary>
        private void buttonSendID_Click(object sender, EventArgs e)
        {
            buttonSendID.Enabled = false;
            try
            {
                Connection.GetInstance().SetLampId(textBoxOldID.Text, textBoxNewID.Text);
                textBoxNewID.Text = textBoxOldID.Text = "";
            }
            catch(Exception x)
            {
                throw x;
            }
            buttonSendID.Enabled = true;
        }
    }

    /// <summary>
    /// Beinhaltet Informationen, die in der cfg Datei gespeichert werden.
    /// </summary>
    [Serializable]
    public class Preferences
    {
        /// <summary>
        /// Gibt die Adresse des Servers inklusive Port zurück oder legt sie fest
        /// </summary>
        public string ServerAddress { get; set; }

        /// <summary>
        /// Gibt die Adresse des Server ohne Port zurück
        /// </summary>
        public string ServerAddressOnly
        {
            get { return ServerAddress.Substring(0, ServerAddress.LastIndexOf(':')); }
        }

        /// <summary>
        /// Gibt nur den Port des Server zurück
        /// </summary>
        public string ServerPortOnly
        {
            get
            {
                int dindex = ServerAddress.LastIndexOf(':');
                int length = ServerAddress.Length;
                return ServerAddress.Substring(ServerAddress.LastIndexOf(':')+1, ServerAddress.Length - ServerAddress.LastIndexOf(':')-1);
            }
        }

        /// <summary>
        /// Gibt den Benutzernamen zurück oder legt diesen fest
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gibt das Passwort zurück oder legt dieses fest
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gibt an, ob zum Start des Programms der Benutzer automatisch eingeloggt werden soll oder legt es fest
        /// </summary>
        public bool AutoLogin { get; set; }

        static Preferences instance = null;

        /// <summary>
        /// Gibt die aktuell gespeicherten Einstellungen zurück.
        /// Sollten keine Einstellungen existieren, werden die Standardeinstellungen zurückgegeben
        /// </summary>
        /// <returns>Verwaltete Instanz der Einstellungen</returns>
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

        /// <summary>
        /// Speichert die verwalteten Einstellungen in der cfg Datei ab
        /// </summary>
        public static void Save()
        {
            BinaryFormatter bf = new BinaryFormatter();
            Stream writer = File.Open("config.cfg", FileMode.Create);
            bf.Serialize(writer, instance);
            writer.Close();
        }

        /// <summary>
        /// Überschreibt die verwalteten Einstellungen mit den übergebenen und speichert diese dann in der cfg Datei ab
        /// </summary>
        /// <param name="prefs">Einstellungen, die übernommen werden sollen</param>
        public static void Save(Preferences prefs)
        {
            instance = prefs;
            Save();
        }

        /// <summary>
        /// Gleichbedeutend zu GetInstance()
        /// <seealso cref="GetInstance"/>
        /// </summary>
        public static Preferences Load()
        {
            return GetInstance();
        }
    }
}
