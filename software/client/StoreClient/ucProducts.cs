using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OutlookStyleControls;

namespace StoreClient
{
    public partial class ucProducts : UserControl
    {
        public ucProducts()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            GridProducts.Columns.Add("ProductName", "Produktbezeichnung");
            GridProducts.Columns.Add("ProductId", "Identifikationsnummer");
            GridProducts.Columns.Add("ProductPrice", "Preis");
            GridProducts.Columns.Add("ProductBind", "Schild Nr.");
        }
    }
}
