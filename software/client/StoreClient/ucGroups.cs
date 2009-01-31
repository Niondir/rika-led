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
    public partial class ucGroups : UserControl
    {
        public event EventHandler groupsChanged;
        RegionData[] groups;
        public ucGroups()
        {
            InitializeComponent();

            this.Dock = DockStyle.Fill;
            RefreshContent(null, null);
        }
        private void RefreshContent(object sender, EventArgs e)
        {
            GridRegions.Rows.Clear();
            groups = Connection.GetInstance().GetRegions();
            foreach (RegionData i in groups)
            {
                int aktRow = GridRegions.Rows.Add(i.Name, i.Id);
                GridRegions.Rows[aktRow].Tag = i;
            }
            GridRegions.Sort(GridRegions.Columns["name"], ListSortDirection.Ascending);
        }

        private void toolStripButtonNew_Click(object sender, EventArgs e)
        {
            FormAddRegion addReg = new FormAddRegion();
            if (addReg.ShowDialog(this) == DialogResult.OK)
                Connection.GetInstance().Add(addReg.Value);
            RefreshContent(null, null);
            if (groupsChanged != null)
                groupsChanged(this, null);
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            int regCount = 0;
            foreach (DataGridViewRow i in GridRegions.SelectedRows)
            {
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
            GridRegions_CellDoubleClick(null, null);
        }

        private void GridRegions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow i in GridRegions.SelectedRows)
            {
                RegionData oldData = (RegionData)i.Tag;

                FormAddRegion editReg = new FormAddRegion(oldData);
                if (editReg.ShowDialog() == DialogResult.OK)
                {
                    Connection.GetInstance().EditRegion(oldData, editReg.Value);
                    RefreshContent(null, null);
                    if (groupsChanged != null)
                        groupsChanged(this, null);
                }
            }
        }

        private void GridRegions_SelectionChanged(object sender, EventArgs e)
        {
            if (GridRegions.SelectedRows.Count > 0)
                toolStripButtonDelete.Enabled = toolStripButtonEdit.Enabled = true;
            else
                toolStripButtonDelete.Enabled = toolStripButtonEdit.Enabled = false;
        }
    }
}
