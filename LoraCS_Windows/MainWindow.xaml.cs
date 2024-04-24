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

        public void usrclPage()
        {
            mainContent.Content = new UserClient(mainContent, new User("s", 1,1,1,1,"s"));
        }

        public async Task initializeService()
        {
            if (File.Exists(@"C:\LoraCS\info.json"))
            {
                var isConnected = await Task.Run(() => econtroller.createConnection("COM3"));
                if (isConnected)
                {
                    Dispatcher.Invoke(() => usrclPage());
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
