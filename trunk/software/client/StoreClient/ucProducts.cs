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
        public ucProducts()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            GridProducts.Columns.Add("ProductName", "Name");
            GridProducts.Columns.Add("ProductId", "Produktnummer");
            GridProducts.Columns.Add("WareGroup", "Warengruppe");
            GridProducts.Columns.Add("Price", "Preis");

            products = Connection.GetInstance().GetProducts();
            foreach (ProductData i in products)
            {
                GridProducts.Rows.Add(new string[] { i.Name, i.Sign.Id.ToString(), i.Sign.Region.Name, i.Price.ToString() });
            }
        }

        private void toolStripButtonPNew_Click(object sender, EventArgs e)
        {
            FormAddProduct adder = new FormAddProduct();
            if (adder.ShowDialog(this) == DialogResult.OK)
            {
            }
        }
    }
}
