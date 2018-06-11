using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Entidades;
using Negocio;

namespace ComputoClient_wpf
{
    /// <summary>
    /// Lógica de interacción para Notify.xaml
    /// </summary>
    public partial class Notify : Window
    {
        private System.Windows.Forms.NotifyIcon m_notifyIcon;
        String _cuenta;
        int ID;
        public Notify()
        {
        }
        public Notify(string cuenta, int id)
        {
            InitializeComponent();
            // initialise code here
            m_notifyIcon = new System.Windows.Forms.NotifyIcon();
            m_notifyIcon.BalloonTipText = "Para finalizar la sesion de click en este icono.";
            m_notifyIcon.BalloonTipTitle = "Inicio de sesion correcto";
            m_notifyIcon.Text = "Control Acceso Centro de Computo";
            m_notifyIcon.Icon = Properties.Resources.web_hi_res_512;
            m_notifyIcon.Click += new EventHandler(m_notifyIcon_Click);
            //cachamos el numero de cuenta
            lbl_cuenta.Content = id.ToString() + " : " +cuenta;
            ID = id;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            m_notifyIcon.Dispose();
            m_notifyIcon = null;  
        }

        private WindowState m_storedWindowState = WindowState.Normal;

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                Hide();
                if (m_notifyIcon != null)
                    m_notifyIcon.ShowBalloonTip(10000);
            }
            else
                m_storedWindowState = WindowState;
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CheckTrayIcon();
        }

        void m_notifyIcon_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = m_storedWindowState;
        }

        void CheckTrayIcon()
        {
            ShowTrayIcon(!IsVisible);
        }

        void ShowTrayIcon(bool show)
        {
            if (m_notifyIcon != null)
                m_notifyIcon.Visible = show;
        }

        private void btnOcultar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnFinalizar_Click(object sender, RoutedEventArgs e)
        {
            if (ID > 0)
            {
                e_registros reg = new e_registros();
                reg.id_registro = ID;
                reg.hora_fin = DateTime.Now;
                if (new n_registros().ActualizaSalida(reg))
                {
                    //Application.Current.Shutdown();
                    this.Hide();
                    MainWindow m = new MainWindow();
                    m.bloqueo();
                }
                else
                    MessageBox.Show("no se actualizo");    
            }
            else
            {
                this.Hide();
                MainWindow m = new MainWindow();
                m.bloqueo();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (ID > 0)
            {
                e_registros reg = new e_registros();
                reg.id_registro = ID;
                reg.hora_fin = DateTime.Now;
                new n_registros().ActualizaSalida(reg);
                this.Hide();
                MainWindow m = new MainWindow();
                m.bloqueo();
            }
        }
    }
}
