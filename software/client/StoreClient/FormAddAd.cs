using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StoreClient
{
    public partial class FormAddAd : Form
    {
        public FormAddAd()
        {
            InitializeComponent();

            // init datetime pickers
            dateTimePicker1_ValueChanged(null, null);
            dateTimePicker1.MinDate = DateTime.Now;

            // init dropdownlist
            comboBox1.SelectedIndex = 1;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.MinDate = dateTimePicker1.Value + new TimeSpan(1, 0, 0, 0);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
