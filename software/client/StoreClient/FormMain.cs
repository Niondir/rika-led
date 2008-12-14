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
        internal static void HandleException(Exception exception)
        {
            try
            {
                throw exception;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ConnectFailure)
                    MessageBox.Show("Es konnte keine Verbindung hergestellt werden. Bitte überprüfen Sie die Einstellungen", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (XmlRpcFaultException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch
            {
                throw exception;
            }
        }
        internal static string title = "iStore";
        
        private DisplayWindow currWindow = DisplayWindow.Login;
        public DisplayWindow CurrWindow
        {
            get { return currWindow; }
            set
            {
                panelMain.Controls.Clear();
                switch (value)
                {
                    case DisplayWindow.Login:
                        this.Text = FormMain.title + " - Login";
                        panelMain.Controls.Add(new ucLogin());
                        break;
                    case DisplayWindow.Groups:
                        break;
                    case DisplayWindow.Products:
                        this.Text = FormMain.title + " - Produkte";
                        panelMain.Controls.Add(new ucProducts());
                        break;
                }
                currWindow = value;
            }
        }

        private Connection connection;
        private bool connected = false;
        private bool Connected
        {
            get { return connected; }
            set
            {
                connected = value;
                if (value == true)
                {
                    verbindungHerstellenToolStripMenuItem.Text = "Verbindung trennen";
                    if (CurrWindow == DisplayWindow.Login)
                        CurrWindow = DisplayWindow.Products;
                }
                else
                {
                    verbindungHerstellenToolStripMenuItem.Text = "Verbindung herstellen";
                    CurrWindow = DisplayWindow.Login;
                }
            }
        }
        public FormMain()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            CurrWindow = DisplayWindow.Login;

            connection = Connection.GetInstance();
            connection.LoginChanged += new EventHandler<ConnectionChangedEventArgs>(connection_LoginChanged);
        }

        void connection_LoginChanged(object sender, ConnectionChangedEventArgs e)
        {
            if (e.Connected)
                Connected = true;
            else
                Connected = false;
            
        }

        private void produkteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrWindow = DisplayWindow.Products;
        }

        private void werbungToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrWindow = DisplayWindow.Advertisement;
        }

        private void verbindungHerstellenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!connected)
            {
                if (CurrWindow != DisplayWindow.Login)
                    CurrWindow = DisplayWindow.Login;
                else
                    ((ucLogin)(panelMain.Controls[0])).PerformLogin();
            }
            else
                connection.Logout();
        }

    }
    public enum DisplayWindow
    {
        Login,
        Products,
        Groups,
        Advertisement
    }
}