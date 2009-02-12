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
        public event EventHandler groupsChanged;

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
