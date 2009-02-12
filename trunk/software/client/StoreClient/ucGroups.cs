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
    /// Stellt die Informationen über bestehende Regionen tabellarisch dar
    /// </summary>
    public partial class ucGroups : UserControl
    {
        /// <summary>
        /// wird ausgelöst, sobald eine neue Gruppe hinzugefügt, verändert oder gelöscht wurde.
        /// Wichtig für die entkoppelte darstellung
        /// </summary>
        /// <seealso cref="FormAddRegion"/>
        public event EventHandler groupsChanged;
        RegionData[] groups;

        /// <summary>
        /// Initialisiert das Control und füllt es mit aktuellen Daten
        /// </summary>
        public ucGroups()
        {
            InitializeComponent();

            this.Dock = DockStyle.Fill;
            RefreshContent(null, null);
        }

        /// <summary>
        /// Aktualisiert die Daten, indem der Server abgefragt wird und stellt sie dar
        /// </summary>
        private void RefreshContent(object sender, EventArgs e)
        {
            GridRegions.Rows.Clear();
            groups = Connection.GetInstance().GetRegions();
            foreach (RegionData i in groups)
            {
                int aktRow = GridRegions.Rows.Add(i.Name, i.Id);
                GridRegions.Rows[aktRow].Tag = i;
            }
            GridRegions.Sort(GridRegions.Columns["name"], ListSortDirection.Ascending);
        }

        /// <summary>
        /// Öffnet ein Dialogfenser, in dem eine neue Region erstellt werden kann
        /// </summary>
        private void toolStripButtonNew_Click(object sender, EventArgs e)
        {
            FormAddRegion addReg = new FormAddRegion();
            if (addReg.ShowDialog(this) == DialogResult.OK)
                Connection.GetInstance().Add(addReg.Value);
            RefreshContent(null, null);
            if (groupsChanged != null)
                groupsChanged(this, null);
        }

        /// <summary>
        /// Entfernt die aktuell ausgewählte Region lokal und vom Server
        /// Hierbei werden auch alle der Gruppe zugehörigen Produkte gelöscht. Eine Abfrage schützt eine ungewollte Löschung
        /// </summary>
        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            int regCount = 0;
            foreach (DataGridViewRow i in GridRegions.SelectedRows)
            {
                regCount += Connection.GetInstance().GetProducts(Convert.ToString(i.Cells["id"].Value)).Length;
            }
            if (MessageBox.Show("Wollen Sie wirklich die " + GridRegions.SelectedRows.Count + " Produktgruppen löschen?\r\nHiermit löschen Sie auch die beinhaltenden " + regCount + " Produkte", "Warnung", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            foreach (DataGridViewRow i in GridRegions.SelectedRows)
            {
                Connection.GetInstance().DeleteRegion(Convert.ToString(i.Cells["id"].Value));
                GridRegions.Rows.Remove(i);
            }
            if (groupsChanged != null)
                groupsChanged(this, null);
        }

        /// <summary>
        /// Öffnet ein Dialogfenster, in dem die aktuell ausgewählte Region bearbeitet und abgespeichert werden kann
        /// </summary>
        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            GridRegions_CellDoubleClick(null, null);
        }

        /// <summary>
        /// Öffnet ein Dialogfenster, in dem die aktuell ausgewählte Region bearbeitet und abgespeichert werden kann
        /// </summary>
        private void GridRegions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow i in GridRegions.SelectedRows)
            {
                RegionData oldData = (RegionData)i.Tag;

                FormAddRegion editReg = new FormAddRegion(oldData);
                if (editReg.ShowDialog() == DialogResult.OK)
                {
                    Connection.GetInstance().EditRegion(oldData, editReg.Value);
                    RefreshContent(null, null);
                    if (groupsChanged != null)
                        groupsChanged(this, null);
                }
            }
        }

        /// <summary>
        /// Verwaltet die Verfügbarkeit der Buttons zum Löschen und Bearbeiten je nach Auswahl der Daten
        /// </summary>
        private void GridRegions_SelectionChanged(object sender, EventArgs e)
        {
            if (GridRegions.SelectedRows.Count > 0)
                toolStripButtonDelete.Enabled = toolStripButtonEdit.Enabled = true;
            else
                toolStripButtonDelete.Enabled = toolStripButtonEdit.Enabled = false;
        }
    }
}
