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
                    textBoxPassword.Text = "Passwort";
                }
                else
                {
                    textBoxPassword.ForeColor = SystemColors.ControlText;
                    textBoxPassword.Text = string.Empty;
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
    }
}
