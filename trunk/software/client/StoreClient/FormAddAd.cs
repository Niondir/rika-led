using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommunicationAPI.DataTypes;

namespace StoreClient
{
    public partial class FormAddAd : Form
    {
        private RegionData[] regions;

        /// <summary>
        /// Gibt die dem Dialog entsprechende Werbung zurück oder legt diese fest
        /// </summary>
        public AdvertisementData Value {
            get
            {
                return new AdvertisementData(-1, (RegionData)comboBoxGroup.Tag, textBoxName.Text,
                    new string[] { textBoxAdLine1.Text, textBoxAdLine2.Text, textBoxAdLine3.Text, textBoxAdLine4.Text },
                    dateTimePickerStartDate.Value, dateTimePickerStopDate.Value,
                    dateTimePickerStartTime.Value, dateTimePickerStopTime.Value);
            }
            set
            {
                textBoxAdLine1.Text = value.Text[0];
                textBoxAdLine2.Text = value.Text[1];
                textBoxAdLine3.Text = value.Text[2];
                textBoxAdLine4.Text = value.Text[3];

                textBoxName.Text = value.Name;

                // adjust the min values 
                if (value.StartDate < dateTimePickerStartDate.MinDate)
                {
                    dateTimePickerStartDate.MinDate = dateTimePickerStartTime.MinDate = value.StartDate;
                }

                try
                {
                    dateTimePickerStopDate.Value = value.StopDate;
                    dateTimePickerStartDate.Value = value.StartDate;
                    dateTimePickerStopTime.Value = value.StopTime;
                    dateTimePickerStartTime.Value = value.StartTime;
                }
                catch { }

                comboBoxGroup.Text = value.Region.Name;
                comboBoxGroup.Tag = value.Region;
            }
        }

        /// <summary>
        /// Erstellt ein FormAddAd und initialisiert die benötigten Komponenten.
        /// Zusätzlich werden die Komponenten, wenn möglich mit Sinnvollen Daten gefüllt.
        /// Datumseinstellungen werden begrenzt und die Produktgruppen ins Auswahlmenü geladen
        /// </summary>
        public FormAddAd()
        {
            InitializeComponent();

            // init datetime pickers
            dateTimePicker1_ValueChanged(null, null);
            dateTimePickerStartDate.MinDate = DateTime.Now;

            SetRegions();

        }

        /// <summary>
        /// Erstellt ein FormAddAd und initialisiert die benötigten Komponenten.
        /// Außerdem wird die Anzeige auf die übergebene Werbeeinblendung angepasst
        /// </summary>
        /// <param name="ad">Die Werbeeinblendun mit der die Form gefüllt werden soll</param>
        public FormAddAd(AdvertisementData ad)
            : this()
        {
            this.Value = ad;            
        }

        /// <summary>
        /// Ändert das Datum des stop DateTime Pickers entsprechend der Startzeit + einen Tag
        /// </summary>
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerStopDate.MinDate = dateTimePickerStartDate.Value + new TimeSpan(1, 0, 0, 0);
        }

        /// <summary>
        /// Übernimmt den aktuellen Status und schließt den Dialog wenn alle Eingaben korreckt sind mit dem DialogResult: OK
        /// </summary>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!Valid())
                return;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Schließt den Dialog ohne die Änderungen zu übernehmen. DialogResult: CANCEL
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Überprüft, ob die Angaben im Dialog eine gülte Werbeeinblendung ergeben und weist gegebenenfalls darauf hin, Einträge zu korrigieren.
        /// </summary>
        /// <returns>Gibt das Ergebnis der Validierung zurück</returns>
        private bool Valid()
        {
            string errorMsg = string.Empty;
            int i = 1;

            if (textBoxAdLine1.Text.Length + textBoxAdLine2.Text.Length + textBoxAdLine3.Text.Length + textBoxAdLine4.Text.Length == 0)
                errorMsg += i++.ToString() + ". Der Werbetext ist leer." + Environment.NewLine;

            // check if region is valid and add it in case
            if (comboBoxGroup.Text.Length == 0)
            {
                comboBoxGroup.BackColor = Color.OrangeRed;
                errorMsg += i++.ToString() + ". Bitte wählen Sie eine Produktgruppe." + Environment.NewLine;
            }
            if (comboBoxGroup.SelectedIndex == -1 && comboBoxGroup.Text.Length > 0)
            {
                if (MessageBox.Show("Die Warengruppe \"" + comboBoxGroup.Text + "\" existiert nicht, soll sie nun erstellt werden?", "Neue Gruppe erstellen?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    FormAddRegion f = new FormAddRegion(comboBoxGroup.Text);
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        Connection.GetInstance().Add(f.Value);
                        SetRegions(f.Value);
                    }
                    else
                    {
                        comboBoxGroup.BackColor = Color.OrangeRed;
                        errorMsg += i++.ToString() + ". Bitte wählen Sie eine Produktgruppe." + Environment.NewLine;
                    }
                }
                else
                {
                    comboBoxGroup.BackColor = Color.OrangeRed;
                    errorMsg += i++.ToString() + ". Bitte wählen Sie eine Produktgruppe." + Environment.NewLine;
                }
            }
            if (errorMsg.Length > 0)
            {
                MessageBox.Show(errorMsg);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Holt vom Server alle aktuell exisitierenden Produktgruppen und Stellt sie in der comboBoxGroup dar
        /// </summary>
        private void SetRegions()
        {
            regions = Connection.GetInstance().GetRegions();
            if (regions == null)
                return;
            comboBoxGroup.Items.Clear();
            foreach (RegionData i in regions)
            {
                comboBoxGroup.Items.Add(i.Name);
            }
        }

        /// <summary>
        /// Holt vom Server alle aktuell exisitierenden Produktgruppen und Stellt sie in der comboBoxGroup dar
        /// Zusätzlich wird die übergebene Produktgruppe selektiert
        /// </summary>
        /// <param name="selected">Produktgruppe, die selektiert werden soll</param>
        private void SetRegions(RegionData selected)
        {
            this.SetRegions();
            comboBoxGroup.SelectedIndex = comboBoxGroup.Items.Count - 1;
        }

        /// <summary>
        /// Setzt die Hintergrundfarbe des Controls auf White. Dient dazu, bei Betreten eines zuvor rot gefärbten Controls, auf weiss zurück zu stellen
        /// </summary>
        /// <param name="sender">Control, das weisste Hintergrundfarbe fordert</param>
        /// <param name="e">unbedeutend</param>
        private void SetWhiteAgain(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = SystemColors.Window;
        }

        /// <summary>
        /// Ändert den Tag der comboBoxGroup auf das aktuell selektierte Objekt
        /// </summary>
        private void comboBoxGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxGroup.Tag = (RegionData)regions[comboBoxGroup.SelectedIndex];
        }

        /// <summary>
        /// Verhindert, dass die Stopzeit des Ausstrahlungswertes vor dem Startwert liegt.
        /// Daher liegt dieser mindestens eine Stunde später.
        /// </summary>
        private void dateTimePickerStartTime_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerStopTime.MinDate = dateTimePickerStartTime.Value + TimeSpan.FromHours(1.0);
        }
    }
}
