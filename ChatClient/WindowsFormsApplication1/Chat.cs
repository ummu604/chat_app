﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Chat_client
{
    public partial class Chat : Form
    {
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        NetworkStream serverStream = default(NetworkStream);
        string readData = null;

        public Chat()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle; 
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(textBox1.Text + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
        }

       private void button2_Click(object sender, EventArgs e)
        {

            textBox1.Text = " enter username ";
            textBox2.Text = " ";
                connBtn.Enabled = false;
                readData = "Conected to Chat Server ...";
                msg();
                clientSocket.Connect("127.0.0.1", 8080);
                serverStream = clientSocket.GetStream();

                byte[] outStream = System.Text.Encoding.ASCII.GetBytes(textBox3.Text + "$");
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();
                Thread ctThread = new Thread(getMessage);
                ctThread.IsBackground = true;
                ctThread.Start();
                
            
          
            
            
        }

        private void getMessage()
        {
            while (true)
            {
                serverStream = clientSocket.GetStream();
                int buffSize = clientSocket.ReceiveBufferSize;
                var inStream = new byte[10024];
                
                serverStream.Read(inStream, 0, buffSize);
                string returndata = System.Text.Encoding.ASCII.GetString(inStream);
                readData = "" + returndata;
                msg();
            }
        }

        private void msg()
        {
           if (this.InvokeRequired)
               this.Invoke(new MethodInvoker(msg));
           else
                textBox2.Text = textBox2.Text + Environment.NewLine + ">>" + readData;
        }

        private void Chat_Load(object sender, EventArgs e)
        {

        }

    }
}
