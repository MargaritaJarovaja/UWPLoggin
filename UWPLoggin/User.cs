using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPLoggin
{
    class User
    {
        public string Username;
        public string Password;
        public User(string u, string p)
        {
            Username = u;
            Password = p;
        }
    }
}
