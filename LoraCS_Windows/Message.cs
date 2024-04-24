using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoraCS_win
{
    public class Message
    {
        public string msg;
        public DateTime ora;
        public bool mittente;

        public Message(string msg, DateTime ora, bool mittente)
        {
            this.msg = msg;
            this.ora = ora;
            this.mittente = mittente;
        }
    }
}
