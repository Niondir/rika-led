using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;



namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string s in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(s);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {   
            // wir basteln uns einen serial port
            SerialPort _serialPort;
            _serialPort = new SerialPort();
            _serialPort.PortName = comboBox1.Text;
            _serialPort.BaudRate = 9600;

  
            // umstaendlich aber noetig, ohne schnallt der xbee die befehle nicht
            Byte[] sendBytes1 = Encoding.ASCII.GetBytes("+++");
            Byte[] sendBytes2 = Encoding.ASCII.GetBytes("ATDH0,DL"+textBox1.Text+",CN\r");
            Byte[] sendBytes3 = Encoding.ASCII.GetBytes(textBox2.Text + "\r\n");

            try
            {
                _serialPort.Open();

                //guard time beachten, +++ senden um in cmd modus zu wechseln
                System.Threading.Thread.Sleep(20);
                _serialPort.Write(sendBytes1, 0, sendBytes1.Length);
                //_serialPort.WriteLine("+++");
        
                System.Threading.Thread.Sleep(20);

                //command senden, genauer: einstellen der ziel id
                _serialPort.Write(sendBytes2, 0, sendBytes2.Length);
                //_serialPort.WriteLine("ATDH0,DL" + textBox1.Text + ",CN\r");
                System.Threading.Thread.Sleep(20);

                //test per funk verschicken
                _serialPort.Write(sendBytes3, 0, sendBytes3.Length);
                //_serialPort.WriteLine(textBox2.Text + "\r\n");

                _serialPort.Close();
            }
            catch (Exception err)
            {
                textBox3.Text = err.Message;
            }

        }
    }
}
