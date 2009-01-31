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
        public ProductData Value
        {
            get
            {
                SignData thisSign = new SignData(Convert.ToInt32(textBoxNumber.Text), regions[comboBoxGroup.SelectedIndex]);
                textBoxPrice.Text.Replace(',', '.');
                return new ProductData(thisSign, textBoxName.Text, Convert.ToDouble(textBoxPrice.Text));
            }
            set
            {
                textBoxName.Text = value.Name;
                textBoxNumber.Text = value.Sign.Id.ToString();
                textBoxPrice.Text = value.Price.ToString("0.00");
                comboBoxGroup.Text = value.Sign.Region.Name;
            }
        }
        private RegionData[] regions;
        public FormAddProduct()
        {
            InitializeComponent();
            SetRegions();
        }
        public FormAddProduct(ProductData editData)
            :this()
        {
            Value = editData;
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

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            if (Valid())
            {
                /*SignData thisSign = new SignData(Convert.ToInt32(textBoxNumber.Text), regions[comboBoxGroup.SelectedIndex]);
                textBoxPrice.Text.Replace(',', '.');
                ProductData thisProduct = new ProductData(thisSign, textBoxName.Text, Convert.ToDouble(textBoxPrice.Text));
                Connection.GetInstance().Add(new ProductData(new SignData(Convert.ToInt32(textBoxNumber.Text), regions[comboBoxGroup.SelectedIndex]), textBoxName.Text, Convert.ToDouble(textBoxPrice.Text)));
                */this.DialogResult = DialogResult.OK;
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
            if(comboBoxGroup.Text.Length == 0)
            //if (comboBoxGroup.SelectedItem == null)
            {
                comboBoxGroup.BackColor = Color.OrangeRed;
                errorMsg += i++.ToString() + ". Bitte wählen Sie eine Produktgruppe." + Environment.NewLine;
            }
            double result;
            if (!double.TryParse(textBoxPrice.Text, out result))
            {
                textBoxPrice.BackColor = Color.OrangeRed;
                errorMsg += i++.ToString() + ". Geben Sie einen gültigen Preis an." + Environment.NewLine;
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
 
        private void ChangeBackToWhiteBackCol(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = SystemColors.Window;
        }

        private void buttonNewGroup_Click(object sender, EventArgs e)
        {
            FormAddRegion addReg = new FormAddRegion();
            if (addReg.ShowDialog() == DialogResult.OK)
            {
                Connection.GetInstance().Add(addReg.Value);
                SetRegions(addReg.Value);
            }
        }

        private void EnterNewBox(object sender, EventArgs e)
        {
            string descString = "";
            if (sender == textBoxName)
                descString = "Geben Sie bitte die Produktbezeichnung ein\r\nDiese Bezeichnung erscheint ebenfalls auf dem Preisschild. Daher sind nur maximal 20 Zeichen pro Wort und 3 Worte erlaubt";
            if (sender == textBoxNumber)
                descString = "Geben Sie bitte die Nummer des Preisschildes, auf dem das Produkt dargestellt sein soll an";
            if (sender == textBoxPrice)
                descString = "Geben Sie bitte den Preis des Produkte in EUR an. Dezimalstellen können durch '.' und ',' getrennt werden";
            if (sender == comboBoxGroup)
                descString = "Wählen Sie bitte die Warengruppe, derer das Produkt zugehörig sein soll oder geben Sie eine neue ein";
            richTextBox1.Text = descString;
        }
    }
}
