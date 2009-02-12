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
        /// <summary>
        /// The output writer for the console form
        /// </summary>
        public TextWriter Out { get; set; }

        private List<string> history = new List<string>();
        private int historyPos = -1;

        private int HistoryPos
        {
            get
            {
                return historyPos;
            }
            set
            {
                historyPos = value;

                if (historyPos >= history.Count)
                {
                    historyPos = history.Count - 1;
                }

                if (historyPos < -1)
                {
                    historyPos = -1;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public FormConsole()
        {
            InitializeComponent();
            Out = new RichTextBoxTextWriter(rtbConsoleOut);
            txtConsoleIn.Focus();
        }

        private void txtConsoleIn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Console.WriteLine(txtConsoleIn.Text);
                Program.ConsoleHandler.HandleCommand(txtConsoleIn.Text);
                
                if (txtConsoleIn.Text != string.Empty)
                    history.Insert(0, txtConsoleIn.Text);
                
                txtConsoleIn.Clear();
                e.Handled = true;
            }
            
        }

        private void txtConsoleIn_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                HistoryPos++;
                if (HistoryPos >= 0)
                    txtConsoleIn.Text = history[HistoryPos];

            }
            else if (e.KeyCode == Keys.Down)
            {
                HistoryPos--;
                if (HistoryPos >= 0)
                    txtConsoleIn.Text = history[HistoryPos];
            }
            else 
            {
                historyPos = -1;
            }
        }

        private void rtbConsoleOut_TextChanged(object sender, EventArgs e)
        {
            //if (rtbConsoleOut.Lines.Length > 5)
            //{
                // remove first line
                //string mystring = rtbConsoleOut.Text;
                //rtbConsoleOut.Text = mystring.Substring(mystring.IndexOf("\n", 0) + 1, mystring.Length - mystring.IndexOf("\n", 0) - 1);
            //}
            
        }

        

    }
}
