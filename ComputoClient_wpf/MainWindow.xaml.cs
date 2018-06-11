using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ComputoClient_wpf
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Structure contain information about low-level keyboard input event
        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public Key key;
            public int scanCode;
            public int flags;
            public int time;
            public IntPtr extra;
        }
        //System level functions to be used for hook and unhook keyboard input
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int id, LowLevelKeyboardProc callback, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hook);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hook, int nCode, IntPtr wp, IntPtr lp);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string name);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern short GetAsyncKeyState(Key key);

        //Declaring Global objects
        private IntPtr ptrHook;
        private LowLevelKeyboardProc objKeyboardProcess;

        public MainWindow()
        {
            ProcessModule objCurrentModule = Process.GetCurrentProcess().MainModule; //Get Current Module
            objKeyboardProcess = new LowLevelKeyboardProc(captureKey); //Assign callback function each time keyboard process
            ptrHook = SetWindowsHookEx(13, objKeyboardProcess, GetModuleHandle(objCurrentModule.ModuleName), 0); //Setting Hook of Keyboard Process for current module
            InitializeComponent();
            IPAddress ipEscucha = IPAddress.Any; //indicamos que escuche por cualquier tarjeta de red local 
                                                 //IPAddress ipEscucha = IPAddress.Parse("0.0.0.0"); //o podemos indicarle la IP de la tarjeta de red local 
            int puertoEscucha = 8000; //puerto por el cual escucharemos datos             
            puntoLocal = new IPEndPoint(ipEscucha, puertoEscucha); //definimos la instancia del IPEndPoint 
                                                                   //lanzamos el escuchador por medio de un hilo 
            new Thread(Escuchador).Start();
            // Console.ReadLine(); //esperar a que el usuario escriba algo y de enter 
            // //finalizar el servidor 

            //Mandar mensaje al servidor que ya esta prendida la pc
            //Enviar("148.227.28.28", 2000, "encendida");
        }

        private void txt_cuenta_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        //bloqueo
        public void bloqueo()
        {
            
            Taskbar.Hide();
            KillCtrlAltDelete();
            Dispatcher.Invoke(() =>
            {
                // Set property or change UI compomponents. 
                this.Show();
                txt_cuenta.Focus();
            });
        }

        public delegate void CloseDelagate();
        public int contador = 0;
        //desbloqueo
        public void desbloqueo(int id)
        {
            //formState.Restore(this);
            Taskbar.Show();
            EnableCTRLALTDEL();
            Dispatcher.Invoke(() =>
            {
                // Set property or change UI compomponents. 
                this.Hide();
                Displaynotify(id);
            });
            
            //Application.Current.Shutdown();
           
        }

        private Window _not = null;

        protected void Displaynotify(int id)
        {
            if (_not == null)
            {
                Notify not = new Notify(txt_cuenta.Text, id);
                _not = not;

                not.Closed += (s, _) => _not = null; //Resets the field on close.
                not.Show();
            }
            else
            {
                _not.Activate(); //Focuses window if it exists.
            }
        }

        private void txt_cuenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Alt && e.SystemKey == Key.F12)
            {
                desbloqueo(0);
                e.Handled = true;         
            }
            else
            {
                base.OnKeyDown(e);
            }
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Entidades.e_usuarios _usuario = new Entidades.e_usuarios();
                _usuario.usuario = txt_cuenta.Text;
                _usuario.pass = txt_contraseña.Password;

                if (new Negocio.n_usuarios().Login(_usuario) == 1)
                {
                   

                    //guardamos el registro
                    Entidades.e_registros _registro = new Entidades.e_registros();
                    _registro.usuario = _usuario.usuario;
                    _registro.equipo = Environment.MachineName;
                    _registro.fecha = DateTime.Now.Date;
                    _registro.hora_inicio = DateTime.Now;
                    int id = new Negocio.n_registros().InsertarRegistro(_registro);
                    if ( id > 0)
                        desbloqueo(id);
                    else
                        MessageBox.Show("no se guardo el registro");

                }
                else
                {
                    MessageBox.Show("Usuario o contraseña Incorrectas"); 
                }

                /*if (new Bussiness.BLogin().checkLogin(int.Parse(txt_cuenta.Text), txt_contraseña.Password))
                {
                    desbloqueo();
                }
                else
                    MessageBox.Show("Usuario o contraseña Incorrectas");*/
            }
            catch (Exception eee)
            {
                MessageBox.Show(eee.Message);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// funciones para hacer el bloqueo de la pc
        /// </summary>
        //deshabilitar control alt suprimir
        public void KillCtrlAltDelete()
        {
            RegistryKey regkey;
            string keyValueInt = "1";
            string subKey = @"Software\Microsoft\Windows\CurrentVersion\Policies\System";

            try
            {
                regkey = Registry.CurrentUser.CreateSubKey(subKey);
                regkey.SetValue("DisableTaskMgr", keyValueInt);
                regkey.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //habilitar control alt suprimir
        public static void EnableCTRLALTDEL()
        {
            try
            {
                string subKey = @"Software\Microsoft\Windows\CurrentVersion\Policies\System";
                RegistryKey rk = Registry.CurrentUser;
                RegistryKey sk1 = rk.OpenSubKey(subKey);
                if (sk1 != null)
                    rk.DeleteSubKeyTree(subKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private IntPtr captureKey(int nCode, IntPtr wp, IntPtr lp)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lp, typeof(KBDLLHOOKSTRUCT));

                //if (objKeyInfo.key == Key.RWin || objKeyInfo.key == Key.LWin) // Disabling Windows keys
                //{
                //    return (IntPtr)1;
               // }
            }
            return CallNextHookEx(ptrHook, nCode, wp, lp);
        }

        public delegate void UpdateTextCallback(string message);
        private void UpdateText(string message)
        {
            lbl_socket.Content = message;
        }

        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            bloqueo();
            

        }

        //AQUI PROBAREMOS LO DE LOS SOCKETS
        //permite escuchador y/o enviar datos en protocolos UDP/TCP y otros 
        private Socket socket = null;
        //nos indicará si el servidor o hilo está escuchando, también nos servirá para finalizarlo 
        private bool corriendo = false;
        //el punto local es la IP de la tarjeta de RED local por la que escucharemos datos y el puerto 
        private IPEndPoint puntoLocal = null;

        //servidor de escucha de datos UDP, este es llamado por un hilo 
        private void Escuchador()
        {
            //instanciamos el socket 
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //asociamos el socket a la dirección local por la cual escucharemos (IP:Puerto) 
            //en caso de que otro programa esté escuchado por el mismo IP/Puerto nos lanzará un error aquí 
            try
            {
                socket.Bind(puntoLocal);
            }
            catch (Exception wss)
            {
                lbl_socket.Dispatcher.Invoke(
               new UpdateTextCallback(this.UpdateText),
               new object[] { wss.Message }
               );
            }
            
            //Console.WriteLine("escuchando...");
            lbl_socket.Dispatcher.Invoke(
                new UpdateTextCallback(this.UpdateText),
                new object[] { "Servidor listo..." }
                );
            //lbl_socket.Content += "escuchando"; 
            //declarar buffer para recibir los datos y le damos un tamaño máximo de datos recibidos por cada mensaje 
            byte[] buffer = new byte[1024];
            //definir objeto para obtener la IP y Puerto de quien nos envía los datos 
            EndPoint ipRemota = new IPEndPoint(IPAddress.Any, 0); //no importa que IPAddress o IP definamos aquí 
                                                                  //indicamos que el servidor a partir de aquí está corriendo 
            corriendo = true;
            //ciclo que permitirá escuchar continuamente mientras se esté corriendo el servidor 
            while (corriendo)
            {
                if (socket.Available == 0) //consultamos si hay datos disponibles que no hemos leido 
                {
                    Thread.Sleep(200); //esperamos 200 milisegundos para volver a preguntar 
                    continue; //esta sentencia hace que el programa regrese al ciclo while(corriendo) 
                }
                //en caso de que si hayan datos disponibles debemos leerlos 
                //indicamos el buffer donde se guardarán los datos y enviamos ipRemota como parámetro de referencia 
                //adicionalmente el método ReceiveFrom nos devuelve cuandos bytes se leyeron 
                int contadorLeido = socket.ReceiveFrom(buffer, ref ipRemota);
                //ahora tenemos los datos en buffer (1024 bytes) pero sabemos cuantos recibimos (contadorLeido) 
                //convertimos esos bytes a string 
                string datosRecibidos = Encoding.Default.GetString(buffer, 0, contadorLeido);
                //Console.WriteLine("Recibí: " + datosRecibidos);
                //MessageBox.Show(datosRecibidos);
                if (datosRecibidos == "u")
                {
                    //corriendo = false;
                    desbloqueo(0);
                }
                else if (datosRecibidos == "l")
                {
                    bloqueo();
                }
            }
        }

        private void Enviar(string ip, int puerto, string mensaje)
        {
            byte[] datosEnBytes = Encoding.Default.GetBytes(mensaje);
            EndPoint ipPuertoRemoto = new IPEndPoint(IPAddress.Parse(ip), puerto);
            socket.SendTo(datosEnBytes, ipPuertoRemoto);
        }

    }
}
