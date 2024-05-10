using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.Json;
using Newtonsoft.Json;
using System.Diagnostics.SymbolStore;
using System.IO.Ports;

namespace LoraCS_win
{
    /// <summary>
    /// Logica di interazione per UserInfo.xaml
    /// </summary>
    public partial class UserInfo : UserControl
    {
        ContentControl mainWindow;
        ESPController econtroller = new ESPController();

        public bool isConnected;
        public int addl = 0;
        public int addh = 0;
        public int chan = 0;
        public String port = "";

        SolidColorBrush verde = new SolidColorBrush(Color.FromRgb(3, 201, 136));
        SolidColorBrush rosso = new SolidColorBrush(Color.FromRgb(205, 24, 24));

        public UserInfo(ContentControl mw)
        {
            InitializeComponent();
            loadChan();
            loadPort();
            isRedict();
            mainWindow = mw;
        }

        public void isRedict()
        {
            if (File.Exists(@"C:\LoraCS\info.json"))
            {
                string jsonString = File.ReadAllText(@"C:\LoraCS\info.json");
                User mainU = JsonConvert.DeserializeObject<User>(jsonString);

                username_txt.Text = mainU.name;
                addh_txt.Text = mainU.addh.ToString();
                addl_txt.Text = mainU.addl.ToString();
                chan_opz.Text = mainU.chan.ToString();

                addl = mainU.addl;
                addh = mainU.addh;
                chan = mainU.chan;

                chan_opz.BorderBrush = verde;
                addh_txt.BorderBrush = verde;
                addl_txt.BorderBrush = verde;
                port_cmb.BorderBrush = rosso;

                checkForSave();
            }
        }

        public void loadChan()
        {
            for(int i = 0; i <= 32; i++)
            {
                chan_opz.Items.Add(i);
            }
        }

        public void loadPort()
        {
            string[] portNames = SerialPort.GetPortNames();

            foreach (string port in portNames)
            {
                port_cmb.Items.Add(port);
            }
        }

        public void editCMBport(bool conn)
        {
            if(conn)
            {
                isConnected = true;
                port_cmb.BorderBrush = verde;
            } else
            {
                isConnected = false;
                port_cmb.BorderBrush = rosso;
            }
            checkForSave();
        }

        public async Task checkPort()
        {
            var conn = await Task.Run(() => econtroller.createConnection(port));
            if (conn)
            {
                Dispatcher.Invoke(() => editCMBport(true));
            } else
            {
                Dispatcher.Invoke(() => editCMBport(false));
            }
        }

        private void save_button_Click(object sender, RoutedEventArgs e)
        {
            User mainU = new User(username_txt.Text, (byte)addl, (byte)addh, (byte)chan, 0, port);

            string dir = @"C:\LoraCS";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
                string json = JsonConvert.SerializeObject(mainU, Newtonsoft.Json.Formatting.Indented);
                System.IO.File.WriteAllText(@"C:\LoraCS\info.json", json);
            } else
            {
                string json = JsonConvert.SerializeObject(mainU, Newtonsoft.Json.Formatting.Indented);
                System.IO.File.WriteAllText(@"C:\LoraCS\info.json", json);
            }

            mainWindow.Content = new UserClient(mainWindow, mainU, econtroller);            
        }

        private void addl_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            int n = 0;

            if(Int32.TryParse(addl_txt.Text, out n))
            {
                if (n < 255 && n > 0)
                {
                    addl_txt.BorderBrush = verde;
                    addl = n;
                }
                else
                {
                    addl_txt.BorderBrush = rosso;
                    addl = 0;
                }
            }
            else
            {
                addl_txt.BorderBrush = rosso;
                addl = 0;
            }
            checkForSave();
        }

        private void addh_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            int n = 0;

            if (Int32.TryParse(addh_txt.Text, out n))
            {
                if (n < 255 && n > 0)
                {
                    addh_txt.BorderBrush = verde;
                    addh = n;
                }
                else
                {
                    addh_txt.BorderBrush = rosso;
                    addh = 0;
                }
            }
            else
            {
                addh_txt.BorderBrush = rosso;
                addh = 0;
            }
            checkForSave();
        }

        private void chan_opz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int n = 0;

            if (Int32.TryParse(chan_opz.SelectedValue.ToString(), out n))
            {
                chan_opz.BorderBrush = verde;
                chan = n;
                checkForSave();
            }
        }

        private void username_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (username_txt.Text == "USERNAME")
            {
                username_txt.Text = "";
            }
        }

        private void username_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkForSave();
        }

        private void port_cmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SolidColorBrush borderBrush = new SolidColorBrush(Color.FromRgb(255, 152, 0));
            port_cmb.BorderBrush = borderBrush;
            port = port_cmb.SelectedValue.ToString();
            Task.Run(() => checkPort());
        }

        public void checkForSave()
        {
            if (addl != 0 && addh != 0 && chan != 0 && port != "" && isConnected == true && (username_txt.Text != "USERNAME" && !String.IsNullOrWhiteSpace(username_txt.Text)))
            {
                save_button.IsEnabled = true;
            }
            else
            {
                save_button.IsEnabled = false;
            }
        }
    }
}