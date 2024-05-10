using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoraCS_win
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ESPController econtroller = new ESPController();
        public MainWindow()
        {
            InitializeComponent();
            Task.Run(() => initializeService());
            
        }

        public void signinPage()
        {
            mainContent.Content = new UserInfo(mainContent);
        }

        public void usrclPage(User mainU)
        {
            mainContent.Content = new UserClient(mainContent, mainU, econtroller);
        }

        public async Task initializeService()
        {
            if (File.Exists(@"C:\LoraCS\info.json"))
            {
                string jsonString = File.ReadAllText(@"C:\LoraCS\info.json");
                User mainU = JsonConvert.DeserializeObject<User>(jsonString);
                var isConnected = await Task.Run(() => econtroller.createConnection(mainU.port));
                if (isConnected)
                {
                    Dispatcher.Invoke(() => usrclPage(mainU));
                } else
                {
                    Dispatcher.Invoke(() => signinPage());
                }
            } else
            {
                Dispatcher.Invoke(() => signinPage());
            }
        }
    }
}
