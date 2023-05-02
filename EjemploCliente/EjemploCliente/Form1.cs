using System;
using System.Windows.Forms;
using System.Net.Sockets;

namespace EjemploCliente
{
    public partial class Form1 : Form
    {
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                NetworkStream serverStream = clientSocket.GetStream();

                byte[] outStream = System.Text.Encoding.ASCII.GetBytes(textBox1.Text + "$");
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();

                textBox1.Text = "";
                textBox1.Focus();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            clientSocket.Connect("127.0.0.1", 8888);
        }
    }
}

