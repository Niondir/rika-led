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
    public partial class ucLogin : UserControl
    {
        private bool userEntered = false;
        private bool passwordEntered = false;

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

        private bool PasswordEntered
        {
            get { return passwordEntered; }
            set
            {
                passwordEntered = value;
                if (value == false)
                {
                    textBoxPassword.ForeColor = Color.Silver;
                    textBoxPassword.PasswordChar = new char();
                    textBoxPassword.Text = "Passwort";
                }
                else
                {
                    textBoxPassword.ForeColor = SystemColors.ControlText;
                    textBoxPassword.Text = string.Empty;
                    textBoxPassword.PasswordChar = '*';
                }
            }
        }

        public ucLogin()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        private void textBoxUsername_Click(object sender, EventArgs e)
        {
            if (UserEntered == false)
                UserEntered = true;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (PasswordEntered == false)
                PasswordEntered = true;
        }

        private void textBoxUsername_Leave(object sender, EventArgs e)
        {
            if (textBoxUsername.Text.Length == 0)
                UserEntered = false;
        }

        private void textBoxPassword_Leave(object sender, EventArgs e)
        {
            if (textBoxPassword.Text.Length == 0)
                PasswordEntered = false;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            Connection con = Connection.GetInstance();
            if (!this.PasswordEntered || !this.UserEntered)
            {
                MessageBox.Show("Geben Sie bitte den Benutzernamen und das Passwort ein", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            con.Login(textBoxUsername.Text, textBoxPassword.Text);
        }

        private void textBoxUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                buttonLogin_Click(this, null);
                e.Handled = true;
            }
        }

        internal void PerformLogin()
        {
            buttonLogin_Click(this, null);
        }
    }
}
