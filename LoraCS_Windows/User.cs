using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoraCS_win
{
    public class User
    {
        public string name;
        public byte addl;
        public byte addh;
        public byte chan;
        public String port; //only for root user
        public int ID;

        public User(string name, byte addl, byte addh, byte chan, int iD, string port)
        {
            this.name = name;
            this.addl = addl;
            this.addh = addh;
            this.chan = chan;
            this.ID = iD;
            this.port = port;
        }
    }
}
