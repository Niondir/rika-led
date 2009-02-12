using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CommunicationAPI.DataTypes;
using CommunicationAPI;

namespace StoreClient
{
    /// <summary>
    /// Stellt eine Statusanzeige aller vorhandenen Daten in kürze dar
    /// </summary>
    public partial class ucStats : UserControl
    {
        static string userStats = "Benutzer: {0}\r\nBenutzergruppen: {1}";
        static string productStats = "Produkte: {0}\r\nProduktgruppen: {1}";
        static string adStats = "Werbeaufträge: {0}\r\nAusstrahlungen/Regionen: {1}/{2}\r\nGesamtausstrahlungsdauer: {3}";

        /// <summary>
        /// 
        /// </summary>
        public static string traceStats = "Kundendaten: {0}\r\nGesamtdauer der Laufwege: {1}\r\nAngelaufene Regionen: {2}";

        /// <summary>
        /// Initialisiert das Control
        /// </summary>
        /// <param name="access">die Rechte, die dem aktuell eingeloggten Benutzer zustehen</param>
        public ucStats(AccessFlags access)
        {
            InitializeComponent();
            this.Dock = DockStyle.Bottom;
            SetStats(access);
        }

        /// <summary>
        /// Stellt die Informationen graphisch dar
        /// </summary>
        /// <param name="access">die Rechte, die dem aktuell eingeloggten Benutzer zustehen</param>
        private void SetStats(AccessFlags access)
        {
            Connection connect = Connection.GetInstance();

            AdvertisementData[] ads = null;
            RegionData[] regions = null;
            ProductData[] products = null;
            UserData[] users = null;
            RoleData[] roles = null;
            TraceData[] traces = null;

            // check access flags and get possible data
            if ((access & AccessFlags.Ads) != 0)
                ads = connect.GetAds();
            if((access & AccessFlags.Regions) != 0)
                regions = connect.GetRegions();
            if ((access & AccessFlags.Product) != 0)
                products = connect.GetProducts();
            if ((access & AccessFlags.User) != 0)
            {
                users = connect.GetUsers();
                roles = connect.GetRoles();
            }
            if((access & AccessFlags.Traces) != 0)
                traces = connect.GetTraces();

            if (regions != null)
            {
                // Ads stats
                if (ads != null)
                {
                    TimeSpan adsCount = new TimeSpan();
                    foreach (AdvertisementData i in ads)
                        adsCount += TimeSpan.FromSeconds((i.StopTime - i.StartTime).Seconds * (i.StopDate - i.StartDate).Days);
                    labelStatsAds.Text = String.Format(adStats, ads.Length, ads.Length, regions.Length, adsCount);
                }
                // Product stats
                if(products != null)
                    labelStatsProducts.Text = String.Format(productStats, products.Length, regions.Length);
            }
            // user stats
            if(users != null)
                labelStatsUser.Text = String.Format(userStats, users.Length, roles.Length);

            // trace stats
            if (traces != null)
            {
                int a1 = traces.Length;
                TimeSpan a2 = new TimeSpan();
                foreach (TraceData i in traces)
                    for (int j = 0; j < i.Locations.Length - 1; j++)
                        a2 += (i.Locations[j + 1].Time - i.Locations[j].Time);
                int a3 = regions.Length;
                labelStatsTraces.Text = String.Format(traceStats, a1, a2.ToString(), a3);
            }
        }
    }
}
