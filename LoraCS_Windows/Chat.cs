using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoraCS_win
{
    public class Chat
    {
        public int friendId;
        public List<Message> Messages;

        public Chat(int friendId)
        {
            this.friendId = friendId;
            Messages = new List<Message>();
        }

        public void insertMsg(Message msg)
        {
            this.Messages.Add(msg);
        }
    }
}
