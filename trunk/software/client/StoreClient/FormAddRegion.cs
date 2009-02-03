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
    public partial class FormAddRegion : Form
    {
        public RegionData Value
        {
            get { return new RegionData(textBoxID.Text, textBoxName.Text); }
            set
            {
                textBoxName.Text = value.Name;
                textBoxID.Text = value.Id;
            }
        }


        public FormAddRegion()
        {
            InitializeComponent();
        }
        public FormAddRegion(string name)
            : this()
        {
            textBoxName.Text = name;
            textBoxID.Focus();
        }
        public FormAddRegion(RegionData editData)
            :this()
        {
            this.Value = editData;
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            if (Valid())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool Valid()
        {
            string errorMsg = string.Empty;
            int i = 1;
            if (textBoxName.Text.Length < 1)
            {
                textBoxName.BackColor = Color.OrangeRed;
                errorMsg += i++.ToString() + ". Bitte geben Sie eine gütige Produktgruppen Nummer ein" + Environment.NewLine;
            }
            int res;
            if (!int.TryParse(textBoxID.Text, out res))
            {
                textBoxID.BackColor = Color.OrangeRed;
                errorMsg += i++.ToString() + ". Bitte geben Sie eine gütige Identifikationsnummer ein" + Environment.NewLine;
            }
            if (errorMsg.Length > 0)
            {
                MessageBox.Show(errorMsg);
                return false;
            }
            return true;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ChangeBackToWhiteBackCol(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = SystemColors.Window;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            richTextBox1.Text = "Bitte geben Sie eine für die neue Produktgruppe einen Namen ein";
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            richTextBox1.Text = "Bitte geben Sie die Identifikationsnummer der zugehörigen Lampe ein. Diese finden Sie auf der Rückseite der Steuerschaltung";
        }
    }
}
