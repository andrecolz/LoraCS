using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoraCS_win
{
    public class Chat
    {
        public User friend;
        public List<Message> Messages;

        public Chat(User fr)
        {
            friend = fr;
            Messages = new List<Message>();
        }

        public void insertMsg(Message msg)
        {
            this.Messages.Add(msg);
        }
    }
}
