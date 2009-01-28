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

        public FormAddAd()
        {
            InitializeComponent();

            // init datetime pickers
            dateTimePicker1_ValueChanged(null, null);
            dateTimePicker1.MinDate = DateTime.Now;

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.MinDate = dateTimePicker1.Value + new TimeSpan(1, 0, 0, 0);
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            MessageBox.Show(((int)e.KeyChar).ToString());
            if (e.KeyChar == ' ')
                ;
        }

        private bool Valid()
        {
            string errorMsg = string.Empty;
            int i = 1;

            // check if region is valid and add it in case
            if (comboBoxGroup.Text.Length == 0)
            //if (comboBoxGroup.SelectedItem == null)
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
    }
}
