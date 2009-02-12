using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.IO;

namespace StoreServer.GUI
{
    /// <summary>
    /// A TextWriter to write text to a richtTextBox
    /// </summary>
    public class RichTextBoxTextWriter : TextWriter
    {
        private RichTextBox rtb;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rtb"></param>
        public RichTextBoxTextWriter(RichTextBox rtb)
        {
            this.rtb = rtb;
        }

        /// <summary>
        /// Write to the richtextbox
        /// </summary>
        /// <param name="value"></param>
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

        /// <summary>
        /// Write to the richtextbox
        /// </summary>
        /// <param name="line"></param>
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

        /// <summary>
        /// Write to the richtextbox
        /// </summary>
        /// <param name="line"></param>
        /// <param name="args"></param>
        public override void WriteLine(string line, params object[] args)
        {
            WriteLine(String.Format(line, args));
        }


        /// <summary>
        /// The Encoding, readonly
        /// </summary>
        public override Encoding Encoding
        {
            get { return Encoding.Default; }
        }
    }
}
