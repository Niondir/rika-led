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
                dateTimePickerStartDate.Value = value.StartDate;
                dateTimePickerStopDate.Value = value.StopDate;
                dateTimePickerStartTime.Value = value.StartTime;
                dateTimePickerStopTime.Value = value.StopTime;

                comboBoxGroup.Text = value.Region.Name;
                comboBoxGroup.Tag = value.Region;
            }
        }

        public FormAddAd()
        {
            InitializeComponent();

            // init datetime pickers
            dateTimePicker1_ValueChanged(null, null);
            dateTimePickerStartDate.MinDate = DateTime.Now;

            SetRegions();

        }
        public FormAddAd(AdvertisementData ad)
            : this()
        {
            this.Value = ad;            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerStopDate.MinDate = dateTimePickerStartDate.Value + new TimeSpan(1, 0, 0, 0);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!Valid())
                return;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

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
                        SetRegions(f.NewRegion);
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
        private void SetRegions(RegionData selected)
        {
            this.SetRegions();
            comboBoxGroup.SelectedIndex = comboBoxGroup.Items.Count - 1;
        }

        private void SetWhiteAgain(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = SystemColors.Window;
        }

        private void comboBoxGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxGroup.Tag = (RegionData)regions[comboBoxGroup.SelectedIndex];
        }
    }
}
