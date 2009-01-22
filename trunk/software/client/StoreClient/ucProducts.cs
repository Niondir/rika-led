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

            products = Connection.GetInstance().GetProducts();
            foreach (ProductData i in products)
            {
                GridProducts.Rows.Add(new string[] { i.Name, i.Sign.Id.ToString(), i.Sign.Region.Name, i.Price.ToString() });
            }

            groups = Connection.GetInstance().GetRegions();
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
            foreach (ProductData i in products)
            {
                GridProducts.Rows.Add(new string[] { i.Name, i.Sign.Id.ToString(), i.Sign.Region.Name, i.Price.ToString() });
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
            }
        }
    }
}
