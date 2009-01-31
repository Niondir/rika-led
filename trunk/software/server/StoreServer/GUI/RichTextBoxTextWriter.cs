using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.IO;

namespace StoreServer.GUI
{
    public class RichTextBoxTextWriter : TextWriter
    {
        private RichTextBox rtb;

        public RichTextBoxTextWriter(RichTextBox rtb)
        {
            this.rtb = rtb;
        }

        public override void Write(char value)
        {
            if (rtb.IsDisposed) return;
            if (rtb.InvokeRequired)
            {
                rtb.BeginInvoke((MethodInvoker)delegate { Write(value); });
                return;
            }

            rtb.AppendText(value.ToString());
            rtb.Update();
            rtb.ScrollToCaret();
        }

        public override void WriteLine(string line)
        {
            if (rtb.IsDisposed) return;
            if (rtb.InvokeRequired)
            {
                rtb.BeginInvoke((MethodInvoker)delegate { WriteLine(line); });
                return;
            }

            rtb.AppendText(line + Environment.NewLine);
            rtb.Update();
            rtb.ScrollToCaret();
        }

        public override void WriteLine(string line, params object[] args)
        {
            WriteLine(String.Format(line, args));
        }



        public override Encoding Encoding
        {
            get { return Encoding.Default; }
        }
    }
}
