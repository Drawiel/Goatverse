using Goatverse.GoatverseService;
using Goatverse.Logic.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Goatverse.Windows {
    /// <summary>
    /// Lógica de interacción para Start.xaml
    /// </summary>
    public partial class Start : Window, GoatverseService.ILobbyManagerCallback{
        private GoatverseService.LobbyManagerClient lobbyManagerClient;
        private string usernamePlayer;
        public Start() {
            InitializeComponent();
            UserSession userSession = new UserSession();
            userSession = UserSessionManager.getInstance().getUser();
            usernamePlayer = userSession.Username;
        }

        private string GenerateLobbyCode(int length = 6) {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                                        .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void BtnClickCreateMatch(object sender, RoutedEventArgs e) {
            string gamertag = usernamePlayer;
            var lobbyWindow = new Lobby(gamertag);
            InstanceContext context = new InstanceContext(this);
            lobbyManagerClient = new GoatverseService.LobbyManagerClient(context);
            string lobbyCode = GenerateLobbyCode();
            Lobby lobby = new Lobby("QQQQQQQ");
            lobby.Show();
            this.Close();
            
        }

        public void ServiceGetMessage(MessageData messageData) {
            throw new NotImplementedException();
        }

        public bool ServiceSuccessfulJoin() {
            throw new NotImplementedException();
        }

        public bool ServiceSucessfulLeave() {
            throw new NotImplementedException();
        }

        private void BtnClickProfile(object sender, RoutedEventArgs e) {
            Profile profile = new Profile();
            profile.Show();
            this.Close();

        }

        private void BtnClickShowFriends(object sender, RoutedEventArgs e) {
            ViewboxFriends.Visibility = ViewboxFriends.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
