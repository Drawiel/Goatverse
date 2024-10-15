using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goatverse.Logic.Classes {
    public class UserSessionManager {
        private static UserSessionManager instance;
        private UserSession user;

        public static UserSessionManager getInstance() {
            if (instance == null) { 
                instance = new UserSessionManager();
            }
            return instance;
        }

        public void loginUser(UserSession user) { 
            this.user = user;
        }

        public UserSession getUser() { 
            return this.user;
        }
    }
}
