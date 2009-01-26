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
    public partial class FormGroupManagement : Form
    {
        public event EventHandler groupsChanged;
        RegionData[] groups;

        public FormGroupManagement()
        {
            InitializeComponent();
        }

        private void RefreshContent(object sender, EventArgs e)
        {
            GridRegions.Rows.Clear();
            groups = Connection.GetInstance().GetRegions();
            foreach (RegionData i in groups)
            {
                GridRegions.Rows.Add(i.Name, i.Id);
            }
            GridRegions.Sort(GridRegions.Columns["name"], ListSortDirection.Ascending);
        }

        private void toolStripButtonNew_Click(object sender, EventArgs e)
        {
            FormAddRegion addReg = new FormAddRegion();
            addReg.ShowDialog(this);
            RefreshContent(null, null);
            if (groupsChanged != null)
                groupsChanged(this, null);
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            int regCount = 0;
            foreach (DataGridViewRow i in GridRegions.SelectedRows){
                regCount += Connection.GetInstance().GetProducts(Convert.ToString(i.Cells["id"].Value)).Length;
            }
            if (MessageBox.Show("Wollen Sie wirklich die " + GridRegions.SelectedRows.Count + " Produktgruppen löschen?\r\nHiermit löschen Sie auch die beinhaltenden " + regCount + " Produkte", "Warnung", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            foreach (DataGridViewRow i in GridRegions.SelectedRows)
            {
                Connection.GetInstance().DeleteRegion(Convert.ToString(i.Cells["id"].Value));
                GridRegions.Rows.Remove(i);
            }
            if (groupsChanged != null)
                groupsChanged(this, null);
        }

        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            if (GridRegions.SelectedRows.Count > 0)
            {
                GridRegions.BeginEdit(true);
            }
        }
    }
}
