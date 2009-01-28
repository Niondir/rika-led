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
    public partial class ucUser : UserControl
    {
        UserData[] users;
        RoleData[] roles;
        UserData iUser;
        CheckBox[] roleFlags;

        public ucUser()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            refreshContent(null, null);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxUsers.SelectedIndex >= 0)
            {
                textBoxName.Text = users[listBoxUsers.SelectedIndex].Username;
                comboBoxGroup.Text = users[listBoxUsers.SelectedIndex].Role.Name;

                listBoxUsers.Tag = users[listBoxUsers.SelectedIndex];

                toolStripButtonDelete.Enabled = true;
                toolStripButtonEdit.Enabled = true;
            }
            else
            {
                toolStripButtonDelete.Enabled = false;
                toolStripButtonEdit.Enabled = false;
                toolStripButtonSave.Enabled = false;
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex < 0)
                return;

            if ((roles[listBox2.SelectedIndex].Flags & (int)CommunicationAPI.AccessFlags.Ads) != 0)
                checkBoxAds.Checked = true;
            else
                checkBoxAds.Checked = false;
            if ((roles[listBox2.SelectedIndex].Flags & (int)CommunicationAPI.AccessFlags.Product) != 0)
                checkBoxProducts.Checked = true;
            else
                checkBoxProducts.Checked = false;
            if ((roles[listBox2.SelectedIndex].Flags & (int)CommunicationAPI.AccessFlags.User) != 0)
                checkBoxUser.Checked = true;
            else
                checkBoxUser.Checked = false;

            textBox5.Text = roles[listBox2.SelectedIndex].Name;
        }

        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            bool setVal = !tableLayoutPanel1.Enabled;

            tableLayoutPanel1.Enabled = setVal;
            toolStripButtonSave.Enabled = setVal;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex < 0)
                return;

            int flags = 0;
            if(checkBoxAds.Checked)
                flags |= (int)CommunicationAPI.AccessFlags.Ads;
            if(checkBoxProducts.Checked)
                flags |= (int)CommunicationAPI.AccessFlags.Product;
            if(checkBoxUser.Checked)
                flags |= (int)CommunicationAPI.AccessFlags.User;

            Connection.GetInstance().EditRole(roles[listBox2.SelectedIndex], new RoleData(textBox5.Text, flags));

            refreshContent(null, null);
        }

        private void refreshContent(object sender, EventArgs e)
        {
            listBoxUsers.Items.Clear();
            users = Connection.GetInstance().GetUsers();
            foreach (UserData i in users)
            {
                listBoxUsers.Items.Add(i.Username);
            }

            comboBoxGroup.Items.Clear();
            listBox2.Items.Clear();
            roles = Connection.GetInstance().GetRoles();
            foreach (RoleData i in roles)
            {
                comboBoxGroup.Items.Add(i.Name);
                listBox2.Items.Add(i.Name);
            }
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text.Length == 0)
            {
                MessageBox.Show("Bitte geben Sie einen Benutzernamen ein", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                clearPWBoxes();
                return;
            }
            if (comboBoxGroup.SelectedIndex == -1)
            {
                MessageBox.Show("Ein Benutzer muss einer Gruppe angehören, die bereits exisitert.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                clearPWBoxes();
                return;
            }
            if (textBoxNewPW.Text != textBoxNewPWagain.Text || textBoxNewPW.Text.Length == 0)
            {
                MessageBox.Show("Die Eingabe der beiden neuen Passwörter stimmen nicht überein", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                clearPWBoxes();
                return;
            }
            UserData newUser = new UserData(textBoxName.Text, textBoxNewPW.Text);
            newUser.Role = new RoleData(comboBoxGroup.Text);
            if (listBoxUsers.SelectedIndex >= 0)
            {
                UserData oldUser = new UserData(((UserData)listBoxUsers.Tag).Username, textBoxOldPW.Text);
                Connection.GetInstance().EditUser(oldUser, newUser);
            }
            else
                Connection.GetInstance().Add(newUser);
            

            tableLayoutPanel1.Enabled = false;
            toolStripButtonSave.Enabled = false;

            refreshContent(null, null);

            clearAllBoxes();
        }
        private void clearPWBoxes()
        {
            textBoxNewPW.Text = textBoxNewPWagain.Text = textBoxOldPW.Text = "";
        }
        private void clearAllBoxes()
        {
            textBoxName.Text = "";
            comboBoxGroup.Text = "";
            clearPWBoxes();
        }
        private void toolStripButtonNew_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Enabled = true;
            toolStripButtonSave.Enabled = true;

            clearAllBoxes();

            listBoxUsers.SelectedIndex = -1;
        }

        private void textBoxOldPW_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                toolStripButtonSave_Click(null, null);
                e.Handled = true;
            }
        }
    }
}
