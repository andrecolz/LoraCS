using HandyControl.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace LoraCS_win
{
    /// <summary>
    /// Logica di interazione per UserClient.xaml
    /// </summary>
    public partial class UserClient : UserControl
    {
        private DispatcherTimer timer;
        ContentControl mainWindow;
        User mainU;
        ESPController econtroller;

        List<Chat> chatList = new List<Chat>();
        int fselect = -1;
        String fromLora;
        
        public UserClient(ContentControl mw, User mu, ESPController ec)
        {
            InitializeComponent();
            viewChat();
            this.mainWindow = mw;
            this.mainU = mu;
            this.econtroller = ec;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += saveData;
            timer.Start();

            Thread t = new Thread(new ThreadStart(readLora));
            t.IsBackground = true;
            t.Start();
        }

        public void readLora()
        {
            while (true)
            {
                fromLora = econtroller.read();
                String[] input = fromLora.Split(';');
                if (input[0] == "newMessage") //type;name;addl;addh;chan;message;
                {
                    int i = 0;
                    foreach(Chat schat in chatList)
                    {
                        if(schat.friend.addl.ToString() == input[2] && schat.friend.addh.ToString() == input[3] && schat.friend.chan.ToString() == input[4])
                        {
                            chatList[i].Messages.Add(new Message(input[5], DateTime.Now, true));
                            addMsgL(input[5]);
                        } else
                        {
                            User newFriend = new User(input[1], Convert.ToByte(input[2]), Convert.ToByte(input[3]), Convert.ToByte(input[4]), 0, "");
                            Chat chat = new Chat(newFriend);
                            string json = JsonConvert.SerializeObject(chat, Newtonsoft.Json.Formatting.Indented);
                            System.IO.File.WriteAllText(@"C:\LoraCS\friend\" + newFriend.name + ".json", json);
                        }
                        i++;
                    }
                } else if (input[0] == "")
                {

                }
            }
        }

        public void saveData(object sender, EventArgs e)
        {
            string dir = @"C:\LoraCS\friend";
            if (Directory.Exists(dir))
            {
                for(int i = 0; i < chatList.Count; i++)
                {
                    string json = JsonConvert.SerializeObject(chatList[i], Newtonsoft.Json.Formatting.Indented);
                    System.IO.File.WriteAllText(@"C:\LoraCS\friend\" + chatList[i].friend.name + ".json", json);
                }
            } else
            {
                Directory.CreateDirectory(dir);
            }
        }

        public void viewChat()
        {
            string dir = @"C:\LoraCS\friend";
            if (Directory.Exists(dir))
            {
                friendbox.Items.Clear();
                chatList.Clear();
                string[] fileNames = Directory.GetFiles(dir);

                foreach (string fileName in fileNames)
                {
                    try
                    {
                        string jsonString = File.ReadAllText(fileName);
                        Chat chat = JsonConvert.DeserializeObject<Chat>(jsonString);
                        chatList.Add(chat);

                        friendbox.Items.Add(chat.friend.name);
                    }
                    catch
                    {
                        Console.WriteLine("errore con: " + fileName);
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(dir);
            }
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            AddUser addusr = new AddUser();
            addusr.Closed += viewAddUser;
            addusr.Show();
        }

        private void viewAddUser(object sender, EventArgs e)
        {
            viewChat();
        }

        private void snd_btn()
        {
            if (!String.IsNullOrWhiteSpace(msg_txt.Text) && fselect != -1)
            {
                chatList[fselect].Messages.Add(new Message(msg_txt.Text, DateTime.Now, true));
                addMsgR(msg_txt.Text);
                econtroller.sendMsg(msg_txt.Text, chatList[fselect].friend);
                msg_txt.Text = "";
            }
        }

        public void addMsgR(String text)
        {
            ChatBubble chatBubble = new ChatBubble();
            chatBubble.FontSize = 14;
            chatBubble.Content = text;
            messagebox.Items.Add(chatBubble);
            messagebox.ScrollIntoView(chatBubble);
        }

        public void addMsgL(String text)
        {
            ChatBubble chatBubble = new ChatBubble();
            chatBubble.FontSize = 14;
            chatBubble.FlowDirection = FlowDirection.RightToLeft;
            chatBubble.HorizontalAlignment = HorizontalAlignment.Left;
            chatBubble.Content = text;
            messagebox.Items.Add(chatBubble);
            messagebox.ScrollIntoView(chatBubble);
        }

        private void friendbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fselect = friendbox.SelectedIndex;
            messagebox.Items.Clear();
            List<Message> messageList = chatList[fselect].Messages;
            foreach (Message msg in messageList)
            {
                if (msg.mittente)
                {
                    addMsgR(msg.msg);
                }
                else
                {
                    addMsgL(msg.msg);
                }
            }
        }

        private void msg_txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                snd_btn();
            }
        }

        private void snd_btn_Click(object sender, RoutedEventArgs e)
        {
            snd_btn();
        }
    }
}