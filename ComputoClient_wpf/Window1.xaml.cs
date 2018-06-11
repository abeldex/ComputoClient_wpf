using System.Windows;
using System;using System.Net.Sockets;using System.Text;

namespace ComputoClient_wpf
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        TcpClient clientSocket = new TcpClient();
        NetworkStream serverStream;
        public Window1()
        {
            InitializeComponent();
           
        }

        System.Windows.Forms.WebBrowser navegador = new System.Windows.Forms.WebBrowser();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            msg("Client Started");
            clientSocket.Connect("148.227.28.28", 8888);
            lbl_conexion.Content += "Client Socket Program - Server Connected ...";
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("Message from Client$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
            byte[] inStream = new byte[10025];
            serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
            string returndata = System.Text.Encoding.ASCII.GetString(inStream);
            msg("Data from Server : " + inStream);
        }

        public void msg(string mesg)
        {
            txt_msg.AppendText(Environment.NewLine + " >> " + mesg);
        }
    }
    
}
