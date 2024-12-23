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

        private int playerCount = 0;
        private string ownerGamertag;
        private GoatverseService.LobbyManagerClient lobbyManagerClient;
        public ObservableCollection<UserControl> UserControls { get; set; }
        private string usernamePlayer;
        private string lobbyCode;
        private PlayerData[] playersList;


        public Lobby(string joinedLobbyCode) {
            

            lobbyCode = joinedLobbyCode;
            UserSession userSession = UserSessionManager.GetInstance().GetUser();
            usernamePlayer = userSession.Username;
            try {
                InstanceContext context = new InstanceContext(this);
                lobbyManagerClient = new GoatverseService.LobbyManagerClient(context);
                UserControls = new ObservableCollection<UserControl>();
                DataContext = this;

                lobbyManagerClient.ServiceConnectToLobby(usernamePlayer, lobbyCode);
                InitializeComponent();
                chipCopyCode.Content = lobbyCode;
            } catch (Exception ex) {
                ExceptionHandler.HandleServiceException(ex);
                Start start = new Start();
                start.Show();
                this.Close();
                
            }
        }

        public void ServiceGetMessage(MessageData messageData) {
            try {
                Message message = new Message {
                    LobbyCode = lobbyCode,
                    UserName = messageData.Username
                };
                string color = "LightBlue";
                HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left;

                if (message.UserName == usernamePlayer) {
                    color = "LightGray";
                    horizontalAlignment = HorizontalAlignment.Right;
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

                UserControls.Add(userChat);
                UserControls.Add(chatMessage);
                scrollViewerChat.ScrollToBottom();
            } catch (Exception ex) {
                ExceptionHandler.HandleServiceException(ex);
            }
        }

        private void BtnClickShowChat(object sender, RoutedEventArgs e) {
            BorderChat.Visibility = BorderChat.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;

        }

        private void BtnClickSendMessage(object sender, RoutedEventArgs e) {
            try {
                if (!string.IsNullOrEmpty(textBoxMessage.Text)) { 
                    string messageText = textBoxMessage.Text;

                    GoatverseService.MessageData messageData = new MessageData();
                    messageData.Message = messageText;
                    messageData.Username = usernamePlayer;
                    messageData.LobbyCode = lobbyCode;

                    lobbyManagerClient.ServiceSendMessageToLobby(messageData);
                    textBoxMessage.Clear();
                    scrollViewerChat.ScrollToBottom();
                }
                
            } catch (Exception ex) {
                ExceptionHandler.HandleServiceException(ex);
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
            } catch (Exception ex) {
                ExceptionHandler.HandleServiceException(ex);
            }
        }

        private void BtnClickStartMatch(object sender, RoutedEventArgs e) {
            try {
                bool matchStarted = lobbyManagerClient.ServiceStartLobbyMatch(lobbyCode, usernamePlayer);
                if (!matchStarted) {
                    MessageBox.Show(Lang.messageLobbyOwnerStartGame);
                }
            } catch (Exception ex) {
                ExceptionHandler.HandleServiceException(ex);
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
            Match matchWindow = new Match(playersList, lobbyCode, ownerGamertag);
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
            } catch (Exception ex) {
                ExceptionHandler.HandleServiceException(ex);
            }
        }

        public void ServiceNotifyMatchStart() {
            try {
                Application.Current.Dispatcher.Invoke(() => {
                    Match match = new Match(playersList, lobbyCode, ownerGamertag);
                    match.Show();
                    this.Close();
                });
            } catch (Exception ex) {
                ExceptionHandler.HandleServiceException(ex);
            }
        }

        public void ServiceNotifyLobbyOwner(string owner) {
            try {
                Application.Current.Dispatcher.Invoke(() => {
                    ownerGamertag = owner;
                    Console.WriteLine($"Nuevo propietario del lobby: {owner}");

                    btnStartMatch.IsEnabled = ownerGamertag == usernamePlayer;

                    if (ownerGamertag == usernamePlayer) {
                        MessageBox.Show("Ahora eres el propietario del lobby. Puedes iniciar la partida.");
                    }
                });
            } catch (Exception ex) {
                ExceptionHandler.HandleServiceException(ex);
            }
        }

        public void ServiceOwnerLeftLobby(string newOwner) {
            try {
                Application.Current.Dispatcher.Invoke(() => {
                    if (string.IsNullOrEmpty(newOwner)) {
                        MessageBox.Show("El lobby se ha cerrado porque no hay más jugadores.");
                        Start startWindow = new Start();
                        startWindow.Show();
                        this.Close();
                        return;
                    }

                    ownerGamertag = newOwner;
                    btnStartMatch.IsEnabled = ownerGamertag == usernamePlayer;

                    if (ownerGamertag == usernamePlayer) {
                        MessageBox.Show("Ahora eres el propietario del lobby.");
                    }
                });
            } catch (Exception ex) {
                ExceptionHandler.HandleServiceException(ex);
            }
        }
    }

}