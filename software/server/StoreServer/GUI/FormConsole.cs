using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace StoreServer.GUI
{
    public partial class FormConsole : Form
    {
        public TextWriter Out { get; set; }

        public FormConsole()
        {
            InitializeComponent();
            Out = new RichTextBoxTextWriter(rtbConsoleOut);
        }

        private void txtConsoleIn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Console.WriteLine(txtConsoleIn.Text);
                txtConsoleIn.Clear();
                e.Handled = true;
            }
        }


    }
}
