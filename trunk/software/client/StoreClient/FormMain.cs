using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;
using CookComputing.XmlRpc;
using StoreClient.Remote.Interfaces;
using CommunicationAPI.Interface;
using CommunicationAPI.DataTypes;

namespace StoreClient
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            panelMain.Controls.Clear();
            panelMain.Controls.Add(new ucLogin());
        }

        private void produkteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            panelMain.Controls.Add(new ucProducts());
        }

        private void werbungToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            panelMain.Controls.Add(new ucAdvertisement());
        }
    }
}