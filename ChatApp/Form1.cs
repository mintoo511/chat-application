using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace ChatApp
{
    public partial class Form1 : Form
    {
        Socket sck;
        EndPoint epLocal, epRemote;
        byte[] buffer;
        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        } 

        private void Form1_Load(object sender, EventArgs e)
        {
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            textLocalIp.Text = GetLocalIp();
            textRemoteIp.Text = GetLocalIp();
        }
        private string GetLocalIp()
        {
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            }
            return "127.0.0.1";
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            //binding Socket
            epLocal = new IPEndPoint(IPAddress.Parse(textLocalIp.Text), Convert.ToInt32(textLocalPort.Text));
            sck.Bind(epLocal);
            //connecting Remote IP
            epRemote = new IPEndPoint(IPAddress.Parse(textRemoteIp.Text), Convert.ToInt32(textRemotePort.Text));
            sck.Connect(epRemote);
            //Listening to specific port
            buffer = new byte[1500];
            sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);
        }
        private void MessageCallBack(IAsyncResult aResult)
        {
            try
            {
                byte[] receivedData = new byte[1500];
                receivedData = (byte[])aResult.AsyncState;
                //converting byte[] to string
                ASCIIEncoding aEncoding = new ASCIIEncoding();
                string receiveMessage = aEncoding.GetString(receivedData);
                //Adding this message into Listbox
                listMessage.Items.Add("Friend:" + receiveMessage);
                buffer = new byte[1500];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            //Converting string message to byte[]
            ASCIIEncoding aEncoding = new ASCIIEncoding();
            byte[] sendingMessage = new byte[1500];
            sendingMessage = aEncoding.GetBytes(textMessage.Text);
            //Sending the Encoded Message
            sck.Send(sendingMessage);
            //adding to the listbox
            listMessage.Items.Add("Me:" + textMessage.Text);
            textMessage.Text = "";
        }

        private void textLocalIp_TextChanged(object sender, EventArgs e)
        {

        }

        private void textMessage_TextChanged(object sender, EventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("to be added");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("to be added");
        }

        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("to be added");
        }

        private void saveChatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("to be added");
        }

        private void changeBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("to be added");
        }

        private void buttonxy_Click(object sender, EventArgs e)
        {
            MessageBox.Show("to be added");
        } 
    }
}
