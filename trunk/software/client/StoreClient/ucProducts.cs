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
        public ucProducts()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            groups = Connection.GetInstance().GetRegions();

            toolStripButtonRefresh_Click(null, null);

            foreach (RegionData i in groups)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(i.Name);
                item.Checked = true;
                item.Click += new EventHandler(item_Click);

                toolStripButtonFilterGroups.DropDownItems.Add(item);
            }
        }

        void item_Click(object sender, EventArgs e)
        {
            ((ToolStripMenuItem)sender).Checked = !((ToolStripMenuItem)sender).Checked;
            GridProducts.Refresh();
        }

        private void toolStripButtonPNew_Click(object sender, EventArgs e)
        {
            FormAddProduct adder = new FormAddProduct();
            if (adder.ShowDialog(this) == DialogResult.OK)
            {
                toolStripButtonRefresh_Click(this, null);
            }
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            products = Connection.GetInstance().GetProducts();
            if (products == null)
                return;
            GridProducts.Rows.Clear();
            foreach (RegionData i in groups)
                ((DataGridViewComboBoxColumn)GridProducts.Columns["Group"]).Items.Add(i.Name);
            foreach (ProductData i in products)
            {
                GridProducts.Rows.Add(new string[] { i.Name, i.Sign.Id.ToString(), i.Sign.Region.Name, i.Price.ToString() });
                GridProducts.Rows[GridProducts.Rows.Count - 1].Cells["ProductID"].ReadOnly = true;

                //DataGridViewCell c = 
            }
        }

        private void GridProducts_Paint(object sender, PaintEventArgs e)
        {
            foreach (DataGridViewRow i in GridProducts.Rows)
            {
                bool visible = true;
                foreach (ToolStripMenuItem j in toolStripButtonFilterGroups.DropDownItems)
                {
                    if (j.Checked == false && (string)i.Cells["Group"].Value == j.Text)
                        visible = false;
                }
                i.Visible = visible;
            }
        }

        private void toolStripButtonPDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow i in GridProducts.SelectedRows)
            {
                Connection.GetInstance().DeleteProduct(Convert.ToInt32(i.Cells["ProductID"].Value));
                GridProducts.Rows.Remove(i);
            }
        }

        private void toolStripButtonPEdit_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow i in GridProducts.SelectedRows)
            {
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
                newData.Name = (string)GridProducts.Rows[e.RowIndex].Cells["ProductName"].Value;
                GridProducts.Rows[e.RowIndex].Cells["Price"].Value = ((string)GridProducts.Rows[e.RowIndex].Cells["Price"].Value).Replace('.', ',');
                try
                {
                    newData.Price = Convert.ToDouble(GridProducts.Rows[e.RowIndex].Cells["Price"].Value);
                    if (newData.Price < 0)
                        throw new FormatException();
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("\"" + newVal + "\" ist kein gültiger Wert\r\n", "Falsche Formatierung", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    GridProducts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = tmpValueInCell;
                    return;
                }
                int regionID = -1;
                foreach (RegionData i in groups)
                {
                    if (i.Name == (string)((DataGridViewComboBoxCell)GridProducts["Group", e.RowIndex]).Value)
                    {
                        regionID = i.Id;
                        break;
                    }
                }
                if (regionID == -1)
                {
                    MessageBox.Show("Die Produktgruppe \"\" existiert nicht", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                newData.Sign = new SignData(0, new RegionData(regionID, ""));

                Connection.GetInstance().EditProduct(Convert.ToInt32(GridProducts.Rows[e.RowIndex].Cells["ProductID"].Value), newData);
            }
        }
    }
}
