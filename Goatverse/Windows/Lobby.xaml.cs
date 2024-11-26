﻿using Goatverse.GoatverseService;
using Goatverse.Logic.Classes;
using Goatverse.Properties.Langs;
using Goatverse.Windows.UserControllers;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private int playerCount = 0;
        private string ownerGamertag;


        private GoatverseService.LobbyManagerClient lobbyManagerClient;
        public ObservableCollection<UserControl> userControls { get; set; }
        private string usernamePlayer;
        private string lobbyCode;
        private PlayerData[] playersList;


        public Lobby(string joinedLobbyCode) {
            InitializeComponent();

            lobbyCode = joinedLobbyCode;
            UserSession userSession = new UserSession();
            userSession = UserSessionManager.GetInstance().GetUser();
            usernamePlayer = userSession.Username;
            try {


                InstanceContext context = new InstanceContext(this);
                lobbyManagerClient = new GoatverseService.LobbyManagerClient(context);

                userControls = new ObservableCollection<UserControl>();
                DataContext = this;

                lobbyManagerClient.ServiceConnectToLobby(usernamePlayer, lobbyCode);
                chipCopyCode.Content = lobbyCode;
            } catch (EndpointNotFoundException ex) {
                MessageBox.Show(Lang.messageServerLostConnection);
                log.Error(ex.Message);
            } catch (TimeoutException ex) {
                MessageBox.Show(Lang.messageConnectionTookTooLong);
                log.Error(ex.Message);
            } catch (CommunicationException ex) {
                MessageBox.Show(Lang.messageLostInternetConnection);
                log.Error(ex.Message);
            } catch (Exception ex) {
                MessageBox.Show(Lang.messageUnexpectedError);
                log.Error(ex.Message);
            }
        }

        public void ServiceGetMessage(MessageData messageData) {
            try {
                Message message = new Message();
                message.LobbyCode = lobbyCode;
                message.UserName = messageData.Username;
                string color = "LightBlue";
                string horizontalAlignment = "Left";

                if (message.UserName == usernamePlayer) {
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
            } catch (EndpointNotFoundException ex) {
                MessageBox.Show(Lang.messageServerLostConnection);
                log.Error(ex.Message);
            } catch (TimeoutException ex) {
                MessageBox.Show(Lang.messageConnectionTookTooLong);
                log.Error(ex.Message);
            } catch (CommunicationException ex) {
                MessageBox.Show(Lang.messageLostInternetConnection);
                log.Error(ex.Message);
            } catch (Exception ex) {
                MessageBox.Show(Lang.messageUnexpectedError);
                log.Error(ex.Message);
            }
        }

        private void BtnClickShowChat(object sender, RoutedEventArgs e) {
            BorderChat.Visibility = BorderChat.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;

        }

        private void BtnClickSendMessage(object sender, RoutedEventArgs e) {
            try {
                string messageText = textBoxMessage.Text;

                GoatverseService.MessageData messageData = new MessageData();
                messageData.Message = messageText;
                messageData.Username = usernamePlayer;
                messageData.LobbyCode = lobbyCode;

                lobbyManagerClient.ServiceSendMessageToLobby(messageData);
                textBoxMessage.Clear();
                scrollViewerChat.ScrollToBottom();
            } catch (EndpointNotFoundException ex) {
                MessageBox.Show(Lang.messageServerLostConnection);
                log.Error(ex.Message);
            } catch (TimeoutException ex) {
                MessageBox.Show(Lang.messageConnectionTookTooLong);
                log.Error(ex.Message);
            } catch (CommunicationException ex) {
                MessageBox.Show(Lang.messageLostInternetConnection);
                log.Error(ex.Message);
            } catch (Exception ex) {
                MessageBox.Show(Lang.messageUnexpectedError);
                log.Error(ex.Message);
            }
        }

        public void AddPlayer(string gamertag, int level, int imageId) {
            if(playerCount < 4) {
                string imagePath = "../../Multimedia/sword.png";
                if(imageId != 0 && imageId != -1) {
                    imagePath = $"../../Multimedia/gato{imageId}.png";
                }

                PlayerCard playerCard = new PlayerCard() {
                    Gamertag = gamertag,
                    Level = level,
                    ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Relative))
                };

                Grid.SetColumn(playerCard, playerCount + 1);
                Grid.SetRow(playerCard, 1);

                (this.Content as Grid).Children.Add(playerCard);

                playerCount++;
            }
            else {
                MessageBox.Show("El lobby está lleno.");
            }
        }

        public void ServiceUpdatePlayersInLobby(PlayerData[] players) {
            try {
                int rowToClear = 1;
                var elementsInRow = (this.Content as Grid).Children.OfType<PlayerCard>().Where(child => Grid.GetRow(child) == rowToClear).ToList();

                foreach (var element in elementsInRow) {
                    (this.Content as Grid).Children.Remove(element);
                    playerCount--;
                }

                Console.WriteLine("Lista actualizada de jugadores en el lobby:");
                foreach (var player in players) {
                    Console.WriteLine($"Jugador: {player.Username}, Nivel: {player.Level}");
                    AddPlayer(player.Username, player.Level, player.ImageId);
                }

                playersList = players;
            } catch (EndpointNotFoundException ex) {
                MessageBox.Show(Lang.messageServerLostConnection);
                log.Error(ex.Message);
            } catch (TimeoutException ex) {
                MessageBox.Show(Lang.messageConnectionTookTooLong);
                log.Error(ex.Message);
            } catch (CommunicationException ex) {
                MessageBox.Show(Lang.messageLostInternetConnection);
                log.Error(ex.Message);
            } catch (Exception ex) {
                MessageBox.Show(Lang.messageUnexpectedError);
                log.Error(ex.Message);
            }
        }

        private void BtnClickStartMatch(object sender, RoutedEventArgs e) {
            try {
                bool matchStarted = lobbyManagerClient.ServiceStartLobbyMatch(lobbyCode);
                if (!matchStarted) {
                    MessageBox.Show("Error al iniciar la partida. Verifica si el lobby es válido.");
                }
            } catch (EndpointNotFoundException ex) {
                MessageBox.Show(Lang.messageServerLostConnection);
                log.Error(ex.Message);
            } catch (TimeoutException ex) {
                MessageBox.Show(Lang.messageConnectionTookTooLong);
                log.Error(ex.Message);
            } catch (CommunicationException ex) {
                MessageBox.Show(Lang.messageLostInternetConnection);
                log.Error(ex.Message);
            } catch (Exception ex) {
                MessageBox.Show(Lang.messageUnexpectedError);
                log.Error(ex.Message);
            }
        }

        public void ServiceStartMatch(PlayerData[] players) {
            Console.WriteLine("La partida ha comenzado.");

            StartMatch();
            this.Close();
        }

        private void StartMatch() {
            MessageBox.Show("Iniciando la partida...");
            this.Close();
            Match matchWindow = new Match(playersList, lobbyCode);
            matchWindow.Show();
        }

        private void BtnClickCopyCode(object sender, RoutedEventArgs e) {
            string code = chipCopyCode.Content.ToString();
            Clipboard.SetText(code);

        }

        private void BtnClickLeaveLobby(object sender, RoutedEventArgs e) {
            try {


                bool disconnectFromLobby = lobbyManagerClient.ServiceDisconnectFromLobby(usernamePlayer, lobbyCode);
                if (disconnectFromLobby) {
                    Start start = new Start();
                    start.Show();
                    this.Close();
                } else {
                    MessageBox.Show("Couldn't disconnect from lobby");
                }
            } catch (EndpointNotFoundException ex) {
                MessageBox.Show(Lang.messageServerLostConnection);
                log.Error(ex.Message);
            } catch (TimeoutException ex) {
                MessageBox.Show(Lang.messageConnectionTookTooLong);
                log.Error(ex.Message);
            } catch (CommunicationException ex) {
                MessageBox.Show(Lang.messageLostInternetConnection);
                log.Error(ex.Message);
            } catch (Exception ex) {
                MessageBox.Show(Lang.messageUnexpectedError);
                log.Error(ex.Message);
            }
        }

        public void ServiceNotifyMatchStart() {
            try {
                Application.Current.Dispatcher.Invoke(() => {
                    Match match = new Match(playersList, lobbyCode);
                    match.Show();
                    this.Close();
                });
            } catch (EndpointNotFoundException ex) {
                MessageBox.Show(Lang.messageServerLostConnection);
                log.Error(ex.Message);
            } catch (TimeoutException ex) {
                MessageBox.Show(Lang.messageConnectionTookTooLong);
                log.Error(ex.Message);
            } catch (CommunicationException ex) {
                MessageBox.Show(Lang.messageLostInternetConnection);
                log.Error(ex.Message);
            } catch (Exception ex) {
                MessageBox.Show(Lang.messageUnexpectedError);
                log.Error(ex.Message);
            }
        }
    }
}