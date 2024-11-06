using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goatverse.Logic.Classes {
    public class UserSessionManager {
        private static UserSessionManager instance;
        private UserSession user;

        public static UserSessionManager GetInstance() {
            if (instance == null) { 
                instance = new UserSessionManager();
            }
            return instance;
        }

        public void LoginUser(UserSession user) { 
            this.user = user;
        }

        public UserSession GetUser() { 
            return this.user;
        }

        public void LogoutUser() { 
            this.user = null;
        }
    }
}
