using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirBoxMonitor
{
    public partial class Form1 : Form
    {

        TcpClient client;
        private bool connected = true;
        private DateTime dt;
        private string data;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connectToServer("10.0.0.120", 7001);
        }

        private void connectToServer(string serverHost, int port)
        {
       
            try
            {
                client = new TcpClient();
                client.Connect(new IPEndPoint(IPAddress.Parse(serverHost), port));
            }
            catch (Exception)
            {

                throw;
            }

            finally
            {
                client.Close();
            }
        }


        private void ReadData()
        {
            StreamReader sr = new StreamReader(client.GetStream());
            while (connected)
            {
                data = sr.ReadLine();
                dt = DateTime.Now;
            }
        }

        private void timerSystem_Tick(object sender, EventArgs e)
        {
            TimeSpan tm = DateTime.Now - dt;
            if (tm.Seconds > 100) richTextBox1.Text += tm.ToString();
            else
            {
                richTextBox1.Text += @"Ok";
            }

            richTextBox1.Text += data;
        }

    
    }
}
