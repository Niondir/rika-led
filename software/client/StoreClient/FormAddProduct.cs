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
    public partial class FormAddProduct : Form
    {
        public ProductData Product { get; set; }
        private RegionData[] regions;
        public FormAddProduct()
        {
            InitializeComponent();
            SetRegions();
        }

        private void SetRegions()
        {
            regions = Connection.GetInstance().GetRegions();
            comboBoxGroup.Items.Clear();
            foreach (RegionData i in regions)
            {
                comboBoxGroup.Items.Add(i.Name);
            }
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            if (Valid())
            {
                SignData thisSign = new SignData(Convert.ToInt32(textBoxNumber.Text), regions[comboBoxGroup.SelectedIndex]);
                textBoxPrice.Text.Replace(',', '.');
                ProductData thisProduct = new ProductData(thisSign, textBoxName.Text, Convert.ToDouble(textBoxPrice.Text));
                Connection.GetInstance().Add(new ProductData(new SignData(Convert.ToInt32(textBoxNumber.Text), regions[comboBoxGroup.SelectedIndex]), textBoxName.Text, Convert.ToDouble(textBoxPrice.Text)));
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
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
            if (textBoxName.Text.Length < 1)
            {
                textBoxName.BackColor = Color.OrangeRed;
                errorMsg += i++.ToString()+". Bitte geben Sie eine gütige Produktbezeichnung ein." + Environment.NewLine;
            }
            int res;
            if (!int.TryParse(textBoxNumber.Text, out res))
            {
                textBoxNumber.BackColor = Color.OrangeRed;
                errorMsg += i++.ToString() + ". Bitte geben Sie eine gütige Produktnummer ein." + Environment.NewLine;
            }
            if (comboBoxGroup.SelectedItem == null)
            {
                comboBoxGroup.BackColor = Color.OrangeRed;
                errorMsg += i++.ToString() + ". Bitte wählen Sie eine Produktgruppe." + Environment.NewLine;
            }

            if (errorMsg.Length > 0)
            {
                MessageBox.Show(errorMsg);
                return false;
            }
            return true;
        }
 
        private void ChangeBackToWhiteBackCol(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = SystemColors.Window;
        }

        private void buttonNewGroup_Click(object sender, EventArgs e)
        {
            FormAddRegion addReg = new FormAddRegion();
            if (addReg.ShowDialog() == DialogResult.OK)
                SetRegions();
        }
    }
}
