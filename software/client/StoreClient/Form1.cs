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
using CommunicationAPI.Interface;
using CommunicationAPI.DataTypes;

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
            IRemoteFunctions betty = XmlRpcProxyGen.Create<IRemoteFunctions>();
            Cursor = Cursors.WaitCursor;
            try
            {
                int num = Convert.ToInt32(textBox1.Text);
                User user = new User("gast", "gast");
                Session session = betty.Login(user);
                label1.Text = session.ID.ToString();
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