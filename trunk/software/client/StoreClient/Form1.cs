using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;
using CookComputing.XmlRpc;
using StoreClient.Remote.Interfaces;

namespace StoreClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            IStateName betty = XmlRpcProxyGen.Create<IStateName>();
            Cursor = Cursors.WaitCursor;
            try
            {
                int num = Convert.ToInt32(textBox1.Text);
                label1.Text = betty.GetStateName(num);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            Cursor = Cursors.Default;
        }

        private void HandleException(Exception ex)
        {
            string msgBoxTitle = "Error";
            try
            {
                throw ex;
            }
            catch (XmlRpcFaultException fex)
            {
                MessageBox.Show("Fault Response: " + fex.FaultCode + " "
                  + fex.FaultString, msgBoxTitle,
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (WebException webEx)
            {
                MessageBox.Show("WebException: " + webEx.Message, msgBoxTitle,
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (webEx.Response != null)
                    webEx.Response.Close();
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message, msgBoxTitle,
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}