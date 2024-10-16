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
            lobbyManagerClient.connectToLobby(usernamePlayer,"A4231D");

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

        public void GetMessage(User user) {
            Message message = new Message();
            message.LobbyCode = "A4231D";
            message.UserName = user.Username;

            var userChat = new UserChat {
               UserName = user.Username,
            };

            var chatMessage = new ChatMessage {
                Message = user.Message,
            };

            userControls.Add(userChat);
            userControls.Add(chatMessage);

            MessageBox.Show("New Message from: " + message.UserName + " Content: " + user.Message);
        }

        public bool SuccessfulJoin() {
            throw new NotImplementedException();
        }

        public bool SucessfulLeave() {
            throw new NotImplementedException();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            string messageText = txtMessage.Text;

            GoatverseService.User userMessage = new User();
            userMessage.Message = messageText;
            userMessage.Username = usernamePlayer;
            userMessage.LobbyCode = "A4231D";

            lobbyManagerClient.sendMessageToLobby(userMessage);
        }

        public class BoolToVis : IValueConverter {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
                if (value is bool boolValue) {
                    return boolValue ? Visibility.Visible : Visibility.Collapsed;
                }
                return Visibility.Collapsed;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
                return (value is Visibility visibility) && visibility == Visibility.Visible;
            }
        }
    }

    
}
