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
        private Connection connection;
        private bool connected = false;
        private bool Connected
        {
            get { return connected; }
            set { connected = value; }
        }
        public FormMain()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            panelMain.Controls.Clear();
            panelMain.Controls.Add(new ucLogin());

            connection = Connection.GetInstance();
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