﻿using Goatverse.GoatverseService;
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
        private int playerCount = 0;
        private string ownerGamertag;


        private GoatverseService.LobbyManagerClient lobbyManagerClient;
        public ObservableCollection<UserControl> userControls { get; set; }
        private string usernamePlayer;
        private string lobbyCode;


        public Lobby(string joinedLobbyCode) {
            InitializeComponent();
            InitializeLobbyOwner(ownerGamertag);

            lobbyCode = joinedLobbyCode;
            UserSession userSession = new UserSession();
            userSession = UserSessionManager.getInstance().getUser();
            usernamePlayer = userSession.Username;

            InstanceContext context = new InstanceContext(this);
            lobbyManagerClient = new GoatverseService.LobbyManagerClient(context);

            userControls = new ObservableCollection<UserControl>();
            DataContext = this;

            lobbyManagerClient.ServiceConnectToLobby(usernamePlayer, lobbyCode);
            textBlockLobbyCode.Text = lobbyCode;

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
            scrollViewerChat.ScrollToBottom();
        }

        public bool ServiceSuccessfulJoin() {
            throw new NotImplementedException();
        }

        public bool ServiceSucessfulLeave() {
            throw new NotImplementedException();
        }

        private void BtnClickShowChat(object sender, RoutedEventArgs e) {
            BorderChat.Visibility = BorderChat.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;

        }

        private void BtnClickSendMessage(object sender, RoutedEventArgs e) {
            string messageText = textBoxMessage.Text;

            GoatverseService.MessageData messageData = new MessageData();
            messageData.Message = messageText;
            messageData.Username = usernamePlayer;
            messageData.LobbyCode = lobbyCode;

            lobbyManagerClient.ServiceSendMessageToLobby(messageData);
            textBoxMessage.Clear();
            scrollViewerChat.ScrollToBottom();
        }

        private void OnPlayerJoined(string gamertag) {
            AddPlayer(gamertag); 
        }

        public void AddPlayer(string gamertag) {
            if(playerCount < 4) 
            {
                PlayerCard playerCard = new PlayerCard();
                playerCard.Gamertag = gamertag;
                Grid.SetColumn(playerCard, playerCount + 1);
                Grid.SetRow(playerCard, 1); 

                (this.Content as Grid).Children.Add(playerCard);

                playerCount++; 
            }
            else {
                MessageBox.Show("El lobby está lleno."); 
            }
        }

        private void InitializeLobbyOwner(string ownerGamertag) {
            AddPlayer(ownerGamertag); 
        }


        public void SimulatePlayerJoining() {
            OnPlayerJoined("Jugador1");
            OnPlayerJoined("Jugador2");
            OnPlayerJoined("Jugador3");
           
        }

    }
}