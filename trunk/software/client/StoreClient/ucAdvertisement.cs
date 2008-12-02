using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace StoreClient
{
    public partial class ucAdvertisement : UserControl
    {
        public ucAdvertisement()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            outlookGrid1.Columns.Add("AdName", "Kurzname");
            outlookGrid1.Columns.Add("AdText", "Werbetext");
            outlookGrid1.Columns.Add("AdStartDate", "Start Datum");
            outlookGrid1.Columns.Add("AdEndDate", "Stop Datum");
            outlookGrid1.Columns.Add("AdStartTime", "Startzeit");
            outlookGrid1.Columns.Add("AdStopTime", "Stopzeit");
        }

        private void toolStripButtonNew_Click(object sender, EventArgs e)
        {
            FormAddAd addad = new FormAddAd();
            DialogResult r = addad.ShowDialog();
        }
    }
}
