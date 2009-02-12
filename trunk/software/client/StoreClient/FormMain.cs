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
using CommunicationAPI;

namespace StoreClient
{

    public partial class FormMain : Form
    {
        /// <summary>
        /// Behandelt die übergebenen Exceptions und wirft am Ende die Exception wieder zurück, wenn keine passende Behandlung gefunden wurde
        /// </summary>
        /// <param name="exception">Exception die behandelt werden soll</param>
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
        
        internal static string title = "MyStore Manager";
        
        private DisplayWindow currWindow = DisplayWindow.Login;
        private bool showStats;
        /// <summary>
        /// Steuert die Anzeige der Controls im Hauptpanel. Die Unterscheidung wird im DisplayWindow getroffen
        /// <seealso cref="DisplayWindow"/>
        /// </summary>
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
                        showStats = false;
                        break;
                    case DisplayWindow.Groups:
                        this.Text = FormMain.title + " - Produktgruppen";
                        panelMain.Controls.Add(new ucGroups());
                        showStats = true;
                        break;
                    case DisplayWindow.Products:
                        this.Text = FormMain.title + " - Produkte";
                        panelMain.Controls.Add(new ucProducts(false));
                        showStats = true;
                        break;
                    case DisplayWindow.User:
                        this.Text = FormMain.title + " - Benutzer";
                        panelMain.Controls.Add(new ucUser());
                        showStats = true;
                        break;
                    case DisplayWindow.Advertisement:
                        this.Text = FormMain.title + " - Werbungen";
                        panelMain.Controls.Add(new ucAdvertisement());
                        showStats = true;
                        break;
                    case DisplayWindow.Analysis:
                        this.Text = FormMain.title + " - Analyse";
                        panelMain.Controls.Add(new ucAnalysis());
                        showStats = true;
                        break;
                }
                currWindow = value;
                if (showStats && hasRights() && value != DisplayWindow.Analysis)
                    panelMain.Controls.Add(new ucStats((AccessFlags)Connection.user.Role.Flags));

                setPermissions();
            }
        }

        /// <summary>
        /// Stellt die Benutzeroberfläche auf die Benutzerrechte ein, die der aktuell angemeldete Benutzer hat
        /// </summary>
        private void setPermissions()
        {
            bool regions = false, products = false, ads = false, traces = false, user = false;
            if (((AccessFlags)Connection.user.Role.Flags & AccessFlags.Ads) != 0)
                ads = true;
            if (((AccessFlags)Connection.user.Role.Flags & AccessFlags.Product) != 0)
                products = true;
            if (((AccessFlags)Connection.user.Role.Flags & AccessFlags.Regions) != 0)
                regions = true;
            if (((AccessFlags)Connection.user.Role.Flags & AccessFlags.Traces) != 0)
                traces = true;
            if (((AccessFlags)Connection.user.Role.Flags & AccessFlags.User) != 0)
                user = true;

            produktgruppenToolStripMenuItem.Enabled = produkteToolStripMenuItem.Enabled = werbungToolStripMenuItem.Enabled = kundenanalyseToolStripMenuItem.Enabled = benutzerToolStripMenuItem.Enabled = false;

            // benötigt für produkte, regionen, ads und analyse
            if (regions)
            {
                produktgruppenToolStripMenuItem.Enabled = true;

                if (products)
                    produkteToolStripMenuItem.Enabled = true;
                if (ads)
                    werbungToolStripMenuItem.Enabled = true;
                if (traces)
                    kundenanalyseToolStripMenuItem.Enabled = true;
            }
            if (user)
                benutzerToolStripMenuItem.Enabled = true;
        }

        /// <summary>
        /// Prüft, ob überhaupt Rechte für den aktuell angemeldeten Benutzer vergeben sind
        /// </summary>
        /// <returns></returns>
        private bool hasRights()
        {
            if (((AccessFlags)Connection.user.Role.Flags & AccessFlags.Ads) != 0
                || ((AccessFlags)Connection.user.Role.Flags & AccessFlags.Product) != 0
                || ((AccessFlags)Connection.user.Role.Flags & AccessFlags.Regions) != 0
                || ((AccessFlags)Connection.user.Role.Flags & AccessFlags.Traces) != 0
                || ((AccessFlags)Connection.user.Role.Flags & AccessFlags.User)  != 0)
                return true;
            return false;
        }

        private Connection connection;
        private bool connected = false;
        /// <summary>
        /// Gibt den aktuellen Verbindungsstatus an oder legt diesen fest
        /// </summary>
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

        /// <summary>
        /// Erstellt eine Instanz des Hauptfensters und initialisiert die Komponenten.
        /// Zusätzlich wird die Verbindung zum Server konfiguriert.
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            CurrWindow = DisplayWindow.Login;

            connection = Connection.GetInstance();
            connection.ServerURL = Preferences.GetInstance().ServerAddress;
            connection.LoginChanged += new EventHandler<ConnectionChangedEventArgs>(connection_LoginChanged);
        }

        /// <summary>
        /// Wird ausgeführt, wenn die Verbindungsinstanz eine Änderung der Verbindung feststellt
        /// </summary>
        void connection_LoginChanged(object sender, ConnectionChangedEventArgs e)
        {
            Connected = e.Connected;
            
            setPermissions();
        }

        /// <summary>
        /// Ruft das Login Control in den Fordergrund, oder versucht einen Login anhand der Daten, die bereits eingetragen sind
        /// Wenn die Verbindung bereits aufgebaut ist, wird diese abgebaut
        /// </summary>
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
            {
                connection.Logout();
                CurrWindow = DisplayWindow.Login;
            }
        }

        /// <summary>
        /// Läd das Control in das haupt Panel, das für die Produkte zuständig ist
        /// </summary>
        private void produkteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrWindow = DisplayWindow.Products;
        }

        /// <summary>
        /// Läd das Control in das haupt Panel, das für die Werbung zuständig ist
        /// </summary>
        private void werbungToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrWindow = DisplayWindow.Advertisement;
        }

        /// <summary>
        /// Läd das Control in das haupt Panel, das für die Produktgruppen zuständig ist
        /// </summary>
        private void produktgruppenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrWindow = DisplayWindow.Groups;
        }

        /// <summary>
        /// Läd das Control in das haupt Panel, das für die Benutzersteuerung zuständig ist
        /// </summary>
        private void benutzerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrWindow = DisplayWindow.User;
        }

        /// <summary>
        /// Läd das Control in das haupt Panel, das für die Kundenanalyse zuständig ist
        /// </summary>
        private void kundenanalyseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrWindow = DisplayWindow.Analysis;
        }

        /// <summary>
        /// Öffnet den Einstellungs Dialog
        /// </summary>
        private void einstellungenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormPreferences prefs = new FormPreferences();
            prefs.ShowDialog();
        }

        /// <summary>
        /// Schließt das Programm
        /// </summary>
        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

    /// <summary>
    /// Aufzählung, aller unterstützten Darstellungsmodi. Werden verwendet, um im Hauptfenster die Anzeige zu bestimmen
    /// </summary>
    public enum DisplayWindow
    {
        Login,
        Products,
        Groups,
        User,
        Advertisement,
        Analysis
    }
}