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
    /// <summary>
    /// Stellt die Informationen über bestehende Werbeeinträge tabellarisch dar
    /// </summary>
    public partial class ucAdvertisement : UserControl
    {
        AdvertisementData[] ads;

        /// <summary>
        /// Initialisiert das Control und füllt es mit aktuellen Daten
        /// </summary>
        public ucAdvertisement()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            RefreshData();
        }

        /// <summary>
        /// Aktualisiert die Daten, indem der Server abgefragt wird und stellt sie dar
        /// </summary>
        private void RefreshData()
        {
            ads = Connection.GetInstance().GetAds();
            gridAds.Rows.Clear();
            foreach (AdvertisementData i in ads)
            {

                int rowNr = gridAds.Rows.Add(new object[] { i.Name, i.Region.Name,
                                                i.Text[0]+" ...",
                                                i.StartDate.ToShortDateString(),
                                                i.StopDate.ToShortDateString(),
                                                i.StartTime.ToShortTimeString(),
                                                i.StopTime.ToShortTimeString()});

                gridAds.Rows[rowNr].Tag = i;
            }
        }

        /// <summary>
        /// Öffnet ein Dialogfenser, in dem eine neue Werbung erstellt werden kann
        /// </summary>
        private void toolStripButtonNew_Click(object sender, EventArgs e)
        {
            FormAddAd addAd = new FormAddAd();
            if (addAd.ShowDialog() == DialogResult.OK)
            {
                Connection.GetInstance().Add(addAd.Value);
                RefreshData();
            }
        }

        /// <summary>
        /// Öffnet ein Dialogfenster, in dem die aktuell ausgewählte Werbung bearbeitet und abgespeichert werden kann
        /// </summary>
        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            if (gridAds.SelectedRows.Count == 0)
                return;
            FormAddAd editAdd = new FormAddAd((AdvertisementData)gridAds.SelectedRows[0].Tag);
            if (editAdd.ShowDialog() == DialogResult.OK)
            {
                Connection.GetInstance().EditAd(((AdvertisementData)gridAds.SelectedRows[0].Tag).Id, editAdd.Value);
                RefreshData();
            }
        }

        /// <summary>
        /// Öffnet ein Dialogfenster, in dem die aktuell ausgewählte Werbung bearbeitet und abgespeichert werden kann
        /// </summary>
        private void gridAds_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            toolStripButtonEdit_Click(null, null);
        }

        /// <summary>
        /// Führt eine Aktualisierung der Daten durch
        /// <seealso cref="RefreshData"/>
        /// </summary>
        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        /// <summary>
        /// Entfernt die aktuell ausgewählte Werbung lokal und vom Server
        /// </summary>
        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow i in gridAds.SelectedRows)
            {
                Connection.GetInstance().DeleteAd((AdvertisementData)i.Tag);
            }
            RefreshData();
        }

        /// <summary>
        /// Verwaltet die Verfügbarkeit der Buttons zum Löschen und Bearbeiten je nach Auswahl der Daten
        /// </summary>
        private void gridAds_SelectionChanged(object sender, EventArgs e)
        {
            if (gridAds.SelectedRows.Count > 0)
                toolStripButtonDelete.Enabled = toolStripButtonEdit.Enabled = true;
            else
                toolStripButtonDelete.Enabled = toolStripButtonEdit.Enabled = false;
        }
    }
}
