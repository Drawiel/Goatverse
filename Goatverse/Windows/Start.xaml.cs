using Goatverse.GoatverseService;
using Goatverse.Logic.Classes;
using Goatverse.Properties.Langs;
using Goatverse.Windows.UserControllers;
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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private GoatverseService.LobbyManagerClient lobbyManagerClient;
        GoatverseService.FriendsManagerClient friendsManagerClient;
        private string usernamePlayer;

        public Start() {
            InitializeComponent();
            UserSession userSession = new UserSession();
            userSession = UserSessionManager.GetInstance().GetUser();
            usernamePlayer = userSession.Username;
            
            if (!usernamePlayer.Contains("Guest")) {
                LoadFriends();
                LoadFriendRequest();
                LoadBlockedUsers();
            } else {
                btnCreateMatch.Visibility = Visibility.Collapsed;
                viewboxFriends.Visibility = Visibility.Collapsed;
                btnFriends.Visibility = Visibility.Collapsed;
                btnProfile.Visibility = Visibility.Collapsed;
            }
            
            
        }

        private string GenerateLobbyCode(int length = 6) {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                                        .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void BtnClickCreateMatch(object sender, RoutedEventArgs e) {
            try {
                InstanceContext context = new InstanceContext(this);
                lobbyManagerClient = new GoatverseService.LobbyManagerClient(context);
                
                string lobbyCode = GenerateLobbyCode();
                bool lobbyCreated = lobbyManagerClient.ServiceCreateLobby(usernamePlayer, lobbyCode);
                if (lobbyCreated) { 
                    Lobby lobby = new Lobby(lobbyCode);
                    lobby.Show();
                    this.Close();
                } else {
                    MessageBox.Show("Lobby already exists");
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

        public void ServiceGetMessage(MessageData messageData) { }

        private void BtnClickProfile(object sender, RoutedEventArgs e) {
            Profile profile = new Profile();
            profile.Show();
            this.Close();

        }

        private void BtnClickShowFriends(object sender, RoutedEventArgs e) {
            viewboxFriends.Visibility = viewboxFriends.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }

        private void BtnClickJoinLobby(object sender, RoutedEventArgs e) {
            ViewboxJoinLobby.Visibility = ViewboxJoinLobby.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }

        private void BtnClickJoinLobbyWithCode(object sender, RoutedEventArgs e) {
            try {
                InstanceContext context = new InstanceContext(this);
                lobbyManagerClient = new GoatverseService.LobbyManagerClient(context);
                string lobbyCode = txtBoxLobbyCodeJoin.Text;

                int playersInLobby = lobbyManagerClient.ServiceCountPlayersInLobby(lobbyCode);
                if(playersInLobby != 0) {
                    if(playersInLobby < 4) {
                        Lobby lobby = new Lobby(lobbyCode);
                        lobby.Show();
                        this.Close();
                    }
                    else {
                        MessageBox.Show("");
                    }
                }
                else {
                    MessageBox.Show("");
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

        public void ServiceUpdatePlayersInLobby(PlayerData[] players) { }

        public void BtnClickLogOut(object sender, RoutedEventArgs e) {
            UserSessionManager.GetInstance().LogoutUser();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        public void LoadFriends() {
            try {
                stckPanelFriends.Children.Clear();
                friendsManagerClient = new GoatverseService.FriendsManagerClient();
                PlayerData[] friendsIds = friendsManagerClient.ServiceGetFriends(usernamePlayer);

                foreach (PlayerData friend in friendsIds) {

                    string imagePath = "../../Multimedia/sword.png";
                    if (friend.ImageId != 0 && friend.ImageId != -1) {
                        imagePath = $"../../Multimedia/gato{friend.ImageId}.png";
                    }

                    ItemFriend itemFriend = new ItemFriend() {
                        Username = friend.Username,
                        Status = " ",
                        ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Relative)),
                    };

                    itemFriend.ConfigureButtons(isFriend: true, isBlocked: false);
                    itemFriend.removeButton.Click += (s, e) => BtnClickRemoveFriend(friend.Username);
                    itemFriend.blockButton.Click += (s, e) => BtnClickBlockUser(friend.Username);

                    stckPanelFriends.Children.Add(itemFriend);
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

        private void BtnClickAddFriend(object sender, RoutedEventArgs e) {
            friendsManagerClient = new GoatverseService.FriendsManagerClient();
            string requestSendTo = txtBoxUsernameSearch.Text;

            try {
                bool blocked = friendsManagerClient.ServiceIsUserBlocked(requestSendTo, usernamePlayer);
                if (blocked) {
                    MessageBox.Show(Lang.messageBlockedByUser);
                    return;
                } 

                bool alreadySended = friendsManagerClient.ServiceIsPendingFriendRequest(usernamePlayer, requestSendTo);
                bool alreadyReceived = friendsManagerClient.ServiceIsPendingFriendRequest(requestSendTo, usernamePlayer);
                if (!alreadySended) {
                    bool requestSended = friendsManagerClient.ServiceSendFriendRequest(usernamePlayer, requestSendTo);

                    if (requestSended) {
                        MessageBox.Show("Friend request sended to: " + requestSendTo);
                    } else {
                        MessageBox.Show("error");
                    }

                } else if (alreadyReceived) {
                    MessageBox.Show("Already Receive Friend Request from user");
                } else {
                    MessageBox.Show("Already sent");
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

        public void LoadFriendRequest() {
            try {
                gridFriendRequests.Children.Clear();
                friendsManagerClient = new GoatverseService.FriendsManagerClient();
                PlayerData[] friendsRequestsIds = friendsManagerClient.ServiceGetPendingFriendRequest(usernamePlayer);

                foreach (PlayerData friend in friendsRequestsIds) {

                    string imagePath = "../../Multimedia/sword.png";
                    if (friend.ImageId != 0 && friend.ImageId != -1) {
                        imagePath = $"../../Multimedia/gato{friend.ImageId}.png";
                    }

                    ItemFriend itemFriendRequest = new ItemFriend() {
                        Username = friend.Username,
                        Status = Lang.globalPending,
                        ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Relative)),
                        
                    };

                    itemFriendRequest.ConfigureButtons(isFriend: false, isBlocked: false);
                    itemFriendRequest.addButton.Click += (s, e) => BtnClickAcceptFriend(friend.Username);

                    gridFriendRequests.Children.Add(itemFriendRequest);
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

        public void BtnClickAcceptFriend(string usernameSender) {
            try {
                friendsManagerClient = new GoatverseService.FriendsManagerClient();
                bool result = friendsManagerClient.ServiceAcceptFriendRequest(usernameSender, usernamePlayer);
                if (result) {
                    MessageBox.Show("Friend Accepted");
                    LoadFriendRequest();
                    LoadFriends();
                } else { 
                
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

        public void BtnClickRemoveFriend(string usernameFriend) {
            try {
                friendsManagerClient = new GoatverseService.FriendsManagerClient();
                bool friendRemoved = friendsManagerClient.ServiceRemoveFriend(usernameFriend, usernamePlayer);
                if (friendRemoved) {
                    MessageBox.Show("Friend Removed");
                    LoadFriends();
                } else if (friendRemoved = friendsManagerClient.ServiceRemoveFriend(usernamePlayer, usernameFriend)) {
                    MessageBox.Show("Friend Removed");
                    LoadFriends();
                } else {
                    MessageBox.Show(" ");
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

        public void ServiceStartMatch(PlayerData[] players) { }

        public void ServiceNotifyMatchStart() { }

        public void LoadBlockedUsers() { 
            try {
                gridBlockedUsers.Children.Clear();
                friendsManagerClient = new GoatverseService.FriendsManagerClient();
                PlayerData[] blockedUsersId = friendsManagerClient.ServiceGetBlockedUsers(usernamePlayer);

                foreach (PlayerData blockedUser in blockedUsersId) {

                    string imagePath = "../../Multimedia/sword.png";
                    if (blockedUser.ImageId != 0 && blockedUser.ImageId != -1) {
                        imagePath = $"../../Multimedia/gato{blockedUser.ImageId}.png";
                    }

                    ItemFriend itemFriendRequest = new ItemFriend() {
                        Username = blockedUser.Username,
                        Status = Lang.globalBlocked,
                        ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Relative)),

                    };

                    itemFriendRequest.ConfigureButtons(isFriend: false, isBlocked: true);
                    itemFriendRequest.unblockButton.Click += (s, e) => BtnClickRemoveBlock(blockedUser.Username);

                    gridBlockedUsers.Children.Add(itemFriendRequest);
                }
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

        public void BtnClickRemoveBlock(string username) {
            try {
                friendsManagerClient = new GoatverseService.FriendsManagerClient();
                bool friendRemoved = friendsManagerClient.ServiceRemoveBlock(username, usernamePlayer);
                if (friendRemoved) {
                    MessageBox.Show("Block removed");
                    LoadBlockedUsers();
                } else {
                    MessageBox.Show(" ");
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

        public void BtnClickBlockUser(string username) {
            try {
                friendsManagerClient = new GoatverseService.FriendsManagerClient();
                bool friendRemoved = friendsManagerClient.ServiceBlockUser(username, usernamePlayer);
                if (friendRemoved) {
                    MessageBox.Show("User Blocked");
                    LoadFriends();
                    LoadBlockedUsers();
                } else {
                    MessageBox.Show(" ");
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

    }
}
