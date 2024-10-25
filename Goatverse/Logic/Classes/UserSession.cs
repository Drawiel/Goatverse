using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goatverse.Logic.Classes {
    public class UserSession {
        private string username;
        private string email;

        public string Username { get { return username; } set { username = value; } }
        public string Email { get { return email; } set { email = value; } }
    }
}
