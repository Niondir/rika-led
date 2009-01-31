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
    public partial class ucStats : UserControl
    {
        static string userStats = "Benutzer: {0}\r\nBenutzergruppen: {1}";
        static string productStats = "Produkte: {0}\r\nProduktgruppen: {1}";
        static string adStats = "Werbeaufträge: {0}\r\nAusstrahlungen/Regionen: {1}/{2}\r\nGesamtausstrahlungsdauer: {3}";
        static string traceStats = "Kundendaten: {0}\r\nGesamtdauer der Laufwege: {1}\r\nAngelaufene Regionen: {2}";

        public ucStats()
        {
            InitializeComponent();
            this.Dock = DockStyle.Bottom;
            SetStats();
        }

        private void SetStats()
        {
            Connection connect = Connection.GetInstance();

            RegionData[] regions = connect.GetRegions();
            ProductData[] products = connect.GetProducts();
            UserData[] users = connect.GetUsers();
            RoleData[] roles = connect.GetRoles();
            AdvertisementData[] ads = connect.GetAds();


            // Ads stats
            TimeSpan adsCount = new TimeSpan();
            foreach (AdvertisementData i in ads)
                adsCount += TimeSpan.FromSeconds((i.StopTime - i.StartTime).Seconds * (i.StopDate - i.StartDate).Days);
            labelStatsAds.Text = String.Format(adStats, ads.Length, ads.Length, regions.Length , adsCount);

            // Product stats
            labelStatsProducts.Text = String.Format(productStats, products.Length, regions.Length);

            // user stats
            labelStatsUser.Text = String.Format(userStats, users.Length, roles.Length);

            // trace stats
            labelStatsTraces.Text = "not yet iimplemented";
        }
    }
}
