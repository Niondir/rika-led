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

        private List<string> history = new List<string>();
        private int historyPos = 0;

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
                Program.ConsoleHandler.HandleCommand(txtConsoleIn.Text);
                history.Add(txtConsoleIn.Text);
                txtConsoleIn.Clear();
                e.Handled = true;
            }
            else if (e.KeyChar == (char)Keys.Up)
            {
                if (historyPos >= 0 && historyPos < history.Count)
                    txtConsoleIn.Text = history[historyPos++];
            }
            else if (e.KeyChar == (char)Keys.Down)
            {
                if (historyPos >= 0 && historyPos < history.Count)
                    txtConsoleIn.Text = history[historyPos--];
            }
        }


    }
}
