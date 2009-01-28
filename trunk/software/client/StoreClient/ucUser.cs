﻿using System;
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

            refreshContent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                textBox1.Text = users[listBox1.SelectedIndex].Username;
                comboBox1.Text = users[listBox1.SelectedIndex].Role.Name;
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
            tableLayoutPanel1.Enabled = !tableLayoutPanel1.Enabled;
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

            refreshContent();
        }

        private void refreshContent()
        {
            listBox1.Items.Clear();
            users = Connection.GetInstance().GetUsers();
            foreach (UserData i in users)
            {
                listBox1.Items.Add(i.Username);
            }

            comboBox1.Items.Clear();
            listBox2.Items.Clear();
            roles = Connection.GetInstance().GetRoles();
            foreach (RoleData i in roles)
            {
                comboBox1.Items.Add(i.Name);
                listBox2.Items.Add(i.Name);
            }
        }
    }
}