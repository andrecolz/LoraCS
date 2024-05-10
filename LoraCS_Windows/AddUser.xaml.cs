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
using System.Windows.Shapes;

namespace LoraCS_win
{
    /// <summary>
    /// Logica di interazione per AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public bool usr = false;
        public int addl = 0;
        public int addh = 0;
        public int chan = 0;
        SolidColorBrush verde = new SolidColorBrush(Color.FromRgb(3, 201, 136));
        SolidColorBrush rosso = new SolidColorBrush(Color.FromRgb(205, 24, 24));

        public AddUser()
        {
            InitializeComponent();
            loadChan();

            string dir = @"C:\LoraCS\friend";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        public void loadChan()
        {
            for (int i = 0; i <= 32; i++)
            {
                chan_opz.Items.Add(i);
            }
        }

        private void add_button_Click(object sender, RoutedEventArgs e)
        {
            User newFriend = new User(username_txt.Text, (byte)addl, (byte)addh, (byte)chan, 0, "");

            Chat chat = new Chat(newFriend);
            string json = JsonConvert.SerializeObject(chat, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(@"C:\LoraCS\friend\" + newFriend.name + ".json", json);
            this.Close();
        }

        private void addl_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            int n = 0;

            if (Int32.TryParse(addl_txt.Text, out n))
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

        public void checkForSave()
        { 
            if (addl != 0 && addh != 0 && chan != 0 && usr == true && (!String.IsNullOrWhiteSpace(username_txt.Text)))
            {
                add_button.IsEnabled = true;
            }
            else
            {
                add_button.IsEnabled = false;
            }
        }

        private void username_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (File.Exists(@"C:\LoraCS\friend\" + username_txt.Text + ".json"))
            {
                usr = false;
                username_txt.BorderBrush = rosso;
            }
            else
            {
                usr = true;
                username_txt.BorderBrush = verde;
                checkForSave();
            }
        }
    }
}