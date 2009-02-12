using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CommunicationAPI.DataTypes;

namespace StoreClient
{
    /// <summary>
    /// Enthält Eingabefälder für den Login
    /// </summary>
    public partial class ucLogin : UserControl
    {
        private bool userEntered = false;

        private bool UserEntered
        {
            get { return userEntered; }
            set
            {
                userEntered = value;
                if (value == false)
                {
                    textBoxUsername.ForeColor = Color.Silver;
                    textBoxUsername.Text = "Benutzername";
                }
                else
                {
                    textBoxUsername.ForeColor = SystemColors.ControlText;
                    textBoxUsername.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// Initialisiert das Control und füllt es gegebenenfalls mit Benutzerdaten, sofern die Einstellungen es verlangen
        /// </summary>
        public ucLogin()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            if (Preferences.GetInstance().AutoLogin)
            {
                textBoxPassword.UseSystemPasswordChar = true;
                textBoxPassword.Text = Preferences.GetInstance().Password;
                textBoxUsername.Text = Preferences.GetInstance().UserName;
            }
            Connection.GetInstance().ServerURL = Preferences.GetInstance().ServerAddress;
        }

        /// <summary>
        /// Überprüft die Angaben und führt bei erfolgreicher Überprüfung einen Login durch. Andernfalls wird der Benutzer auf die fehlenden Angaben hingewiesen
        /// </summary>
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            Connection.GetInstance().ServerURL = Preferences.GetInstance().ServerAddress;
            if (this.textBoxPassword.Text.Length == 0 && this.textBoxUsername.Text.Length == 0)
            {
                MessageBox.Show("Geben Sie bitte den Benutzernamen und das Passwort ein", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Connection.GetInstance().Login(textBoxUsername.Text, textBoxPassword.Text);
        }

        /// <summary>
        /// Erhöht die Benutzerfreundlichkeit durch Login, sobald die Enter Taste gedrückt wird
        /// </summary>
        /// <seealso cref="buttonLogin_Click"/>
        private void textBoxUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                buttonLogin_Click(this, null);
                e.Handled = true;
            }
        }

        /// <summary>
        /// Forciert einen Login
        /// </summary>
        /// <seealso cref="buttonLogin_Click"/>
        internal void PerformLogin()
        {
            buttonLogin_Click(this, null);
        }
    }
}
