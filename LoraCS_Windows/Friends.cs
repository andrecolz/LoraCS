using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoraCS_win
{
    public class Friends
    {
        public int nFriends;
        public List<User> users;

        public Friends()
        {
            users = new List<User>();
            nFriends = 0;
        }
    }
}