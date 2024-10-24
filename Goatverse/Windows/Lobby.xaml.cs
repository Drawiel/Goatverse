using Goatverse.GoatverseService;
using Goatverse.Logic.Classes;
using Goatverse.Windows.UserControllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    /// Interaction logic for Lobby.xaml
    /// </summary>
    public partial class Lobby : Window, GoatverseService.ILobbyManagerCallback {

        private GoatverseService.LobbyManagerClient lobbyManagerClient;
        public ObservableCollection<UserControl> userControls {  get; set; }
        private string usernamePlayer;
        private string lobbyCode;
        

        public Lobby(string joinedLobbyCode) {
            InitializeComponent();

            lobbyCode = joinedLobbyCode; 
            UserSession userSession = new UserSession();
            userSession = UserSessionManager.getInstance().getUser();
            usernamePlayer = userSession.Username;

            userControls = new ObservableCollection<UserControl>();
            DataContext = this;

        }

        public void ServiceGetMessage(MessageData messageData) {
            Message message = new Message();
            message.LobbyCode = lobbyCode;
            message.UserName = messageData.Username;
            string color = "LightBlue";
            string horizontalAlignment = "Left";

            if(message.UserName == usernamePlayer) {
                color = "LightGray";
                horizontalAlignment = "Right";
            }

            var userChat = new UserChat {
               Username = messageData.Username,
               HorizontalAlignment = horizontalAlignment,
            };

            var chatMessage = new ChatMessage {
               Message = messageData.Message,
               Color = color,
               HorizontalAlignment = horizontalAlignment,
            };

            userControls.Add(userChat);
            userControls.Add(chatMessage);
        }

        public bool ServiceSuccessfulJoin() {
            throw new NotImplementedException();
        }

        public bool ServiceSucessfulLeave() {
            throw new NotImplementedException();
        }

        private void BtnClickShowChat(object sender, RoutedEventArgs e) {

        }

        private void BtnClickSendMessage(object sender, RoutedEventArgs e) {
            string messageText = txtMessage.Text;

            GoatverseService.MessageData messageData = new MessageData();
            messageData.Message = messageText;
            messageData.Username = usernamePlayer;
            messageData.LobbyCode = lobbyCode;

            lobbyManagerClient.ServiceSendMessageToLobby(messageData);
            txtMessage.Clear();
        }
    }
}
