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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoraCS_win
{
    /// <summary>
    /// Logica di interazione per UserClient.xaml
    /// </summary>
    public partial class UserClient : UserControl
    {
        ContentControl mainWindow;
        User mainU;
        public UserClient(ContentControl mw, User mu)
        {
            InitializeComponent();
            this.mainWindow = mw;
            this.mainU = mu;
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            Window addusr = new AddUser();
            addusr.Show();
        }
    }
}
