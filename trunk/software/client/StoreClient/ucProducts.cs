using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OutlookStyleControls;
using CommunicationAPI.DataTypes;

namespace StoreClient
{
    public partial class ucProducts : UserControl
    {
        private ProductData[] products;
        private RegionData[] groups;
        public ucProducts(bool showGroups)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            groups = Connection.GetInstance().GetRegions();

            refreshContent(null, null);

            if (showGroups)
                toolStripButtonGroups_Click(null, null);
        }

        void item_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Name == "all")
            {
                bool checkstate = !((ToolStripMenuItem)sender).Checked;
                foreach (ToolStripItem i in toolStripButtonFilterGroups.DropDownItems)
                {
                    if(i.Name != "seperator")
                       ((ToolStripMenuItem)i).Checked = checkstate;
                }
            }
            else
                ((ToolStripMenuItem)sender).Checked = !((ToolStripMenuItem)sender).Checked;
            GridProducts.Refresh();
        }

        private void toolStripButtonPNew_Click(object sender, EventArgs e)
        {
            FormAddProduct adder = new FormAddProduct();
            if (adder.ShowDialog(this) == DialogResult.OK)
            {
                Connection.GetInstance().Add(adder.Value);
                refreshContent(this, null);
            }
        }

        private void refreshContent(object sender, EventArgs e)
        {
            groups = Connection.GetInstance().GetRegions(); 

            toolStripButtonFilterGroups.DropDownItems.Clear();
            ToolStripMenuItem alleItem = new ToolStripMenuItem("Alle");
            alleItem.Name = "all";
            alleItem.Checked = true;
            alleItem.Click += new EventHandler(item_Click);
            toolStripButtonFilterGroups.DropDownItems.Add(alleItem);

            ToolStripSeparator sep = new ToolStripSeparator();
            sep.Name = "seperator";
            toolStripButtonFilterGroups.DropDownItems.Add(sep);

            foreach (RegionData i in groups)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(i.Name);
                item.Checked = true;
                item.Click += new EventHandler(item_Click);

                toolStripButtonFilterGroups.DropDownItems.Add(item);
            }
 
            ((DataGridViewComboBoxColumn)GridProducts.Columns["Group"]).Items.Clear();
            foreach (RegionData i in groups)
                ((DataGridViewComboBoxColumn)GridProducts.Columns["Group"]).Items.Add(i.Name);

            products = Connection.GetInstance().GetProducts();            
            GridProducts.Rows.Clear();
            foreach (ProductData i in products)
            {
                int aktRow = GridProducts.Rows.Add(new string[] { i.Name, i.Sign.Id.ToString(), i.Sign.Region.Name, i.Price.ToString("0.00") });
                GridProducts.Rows[aktRow].Cells["ProductID"].ReadOnly = true;
                GridProducts.Rows[aktRow].Tag = i;
            }
        }

        private void GridProducts_Paint(object sender, PaintEventArgs e)
        {
            foreach (DataGridViewRow i in GridProducts.Rows)
            {
                bool visible = true;
                foreach (ToolStripItem j in toolStripButtonFilterGroups.DropDownItems)
                {
                    if(j.Name != "seperator")
                        if (((ToolStripMenuItem)j).Checked == false && (string)i.Cells["Group"].Value == ((ToolStripMenuItem)j).Text)
                        visible = false;
                }
                i.Visible = visible;
            }
        }

        private void toolStripButtonPDelete_Click(object sender, EventArgs e)
        {
            if (GridProducts.SelectedRows.Count == 1)
                if (MessageBox.Show("Wollen Sie wirklich das Produkt \"" + ((ProductData)GridProducts.SelectedRows[0].Tag).Name + "\" löschen?", "Frage", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            if (GridProducts.SelectedRows.Count > 1)
                if (MessageBox.Show("Wollen Sie wirklich die " + GridProducts.SelectedRows.Count + " Produkte löschen?", "Frage", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            foreach (DataGridViewRow i in GridProducts.SelectedRows)
            {
                Connection.GetInstance().DeleteProduct(Convert.ToInt32(((ProductData)i.Tag).Sign.Id));
                GridProducts.Rows.Remove(i);
            }
        }

        private void toolStripButtonPEdit_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow i in GridProducts.SelectedRows)
            {
                ProductData oldData = (ProductData)i.Tag;

                FormAddProduct editPro = new FormAddProduct(oldData);
                if (editPro.ShowDialog() == DialogResult.OK)
                {
                    Connection.GetInstance().EditProduct(oldData.Sign.Id, editPro.Value);
                    refreshContent(null, null);
                }
            }
        }

        private string tmpValueInCell;
        ProductData newData;
        private void GridProducts_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            tmpValueInCell = (string)GridProducts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
        }

        private void GridProducts_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string newVal = (string)GridProducts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

            // Value changed during editing process
            if (newVal != tmpValueInCell)
            {
                newData = new ProductData();
                newData.Name = (string)GridProducts.Rows[e.RowIndex].Cells["ProdName"].Value;
                GridProducts.Rows[e.RowIndex].Cells["Price"].Value = ((string)GridProducts.Rows[e.RowIndex].Cells["Price"].Value).Replace('.', ',');
                try
                {
                    newData.Price = Convert.ToDouble(GridProducts.Rows[e.RowIndex].Cells["Price"].Value);
                    if (newData.Price < 0)
                        throw new FormatException();
                }
                catch (FormatException)
                {
                    MessageBox.Show("\"" + newVal + "\" ist kein gültiger Wert\r\n", "Falsche Formatierung", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    GridProducts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = tmpValueInCell;
                    return;
                }
                string regionID = "-1";
                foreach (RegionData i in groups)
                {
                    if (i.Name == (string)((DataGridViewComboBoxCell)GridProducts["Group", e.RowIndex]).Value)
                    {
                        regionID = i.Id;
                        break;
                    }
                }
                if (regionID == "-1")
                {
                    MessageBox.Show("Die Produktgruppe \"\" existiert nicht", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                newData.Sign = new SignData(0, new RegionData(regionID, ""));

                Connection.GetInstance().EditProduct(Convert.ToInt32(GridProducts.Rows[e.RowIndex].Cells["ProductID"].Value), newData);
            }
        }

        private void toolStripButtonGroups_Click(object sender, EventArgs e)
        {
            FormGroupManagement grouper = new FormGroupManagement();
            grouper.groupsChanged += new EventHandler(refreshContent);
            grouper.ShowDialog(this);
        }

        private void GridProducts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            toolStripButtonPEdit_Click(sender, null);
        }

        private void GridProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (GridProducts.SelectedRows.Count > 0)  
                toolStripButtonPDelete.Enabled = toolStripButtonPEdit.Enabled = true;
            else
                toolStripButtonPDelete.Enabled = toolStripButtonPEdit.Enabled = false;
        }
    }
}
