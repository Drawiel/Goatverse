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
        public ObservableCollection<UIElement> userControls {  get; set; }
        private string usernamePlayer;
        public static string test = "Elemento Statico";

        public Lobby() {
            InitializeComponent();
           
            
            UserSession userSession = new UserSession();
            userSession = UserSessionManager.getInstance().getUser();
            usernamePlayer = userSession.Username;

            InstanceContext context = new InstanceContext(this);
            lobbyManagerClient = new GoatverseService.LobbyManagerClient(context);
            lobbyManagerClient.ServiceConnectToLobby(usernamePlayer,"A4231D");

            userControls = new ObservableCollection<UIElement>();
            chatSection.ItemsSource = userControls;

            var userChat = new UserChat {
                UserName = "Usuario de prueba",
            };

            var chatMessage = new ChatMessage {
                Message = "Mensaje de usuario",
            };

            userControls.Add(userChat);
            userControls.Add(chatMessage);

        }

        public void ServiceGetMessage(MessageData messageData) {
            Message message = new Message();
            message.LobbyCode = "A4231D";
            message.UserName = messageData.Username;

            var userChat = new UserChat {
               UserName = messageData.Username,
            };

            var chatMessage = new ChatMessage {
                Message = messageData.Message,
            };

            userControls.Add(userChat);
            userControls.Add(chatMessage);

            MessageBox.Show("New Message from: " + message.UserName + " Content: " + messageData.Message);
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
            messageData.LobbyCode = "A4231D";

            lobbyManagerClient.ServiceSendMessageToLobby(messageData);
        }
    }
}
