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
    public partial class ucAdvertisement : UserControl
    {
        AdvertisementData[] ads;
        RegionData[] regions;

        public ucAdvertisement()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            RefreshData();
        }

        private void RefreshData()
        {
            ads = Connection.GetInstance().GetAds();
            regions = Connection.GetInstance().GetRegions();
        }

        private void toolStripButtonNew_Click(object sender, EventArgs e)
        {
            FormAddAd addad = new FormAddAd();
            DialogResult r = addad.ShowDialog();
        }
    }
}
