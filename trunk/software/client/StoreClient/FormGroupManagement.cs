using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommunicationAPI.DataTypes;

namespace StoreClient
{
    /// <summary>
    /// Entkoppelte Anzeige der Produktgruppen
    /// <seealso cref="ucGroups"/>
    /// </summary>
    public partial class FormGroupManagement : Form
    {
        /// <summary>
        /// Wird ausgelöst, sobald eine neue Region erstellt oder eine bestehende bearbeitet oder gelöscht wurde
        /// </summary>
        public event EventHandler groupsChanged;

        /// <summary>
        /// Erstellt ein neues Fenster und initialisiert die komponenten
        /// </summary>
        public FormGroupManagement()
        {
            InitializeComponent();
        }

        private void ucGroups1_RegionChanged(object sender, EventArgs e)
        {
            if (groupsChanged != null)
                groupsChanged(this, null);
        }
    }
}
