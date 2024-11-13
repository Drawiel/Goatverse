﻿using Goatverse.GoatverseService;
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
            userSession = UserSessionManager.GetInstance().GetUser();
            usernamePlayer = userSession.Username;
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
            } catch (FaultException ex) {
                MessageBox.Show(ex.Message);
            }
            
        }

        public void ServiceGetMessage(MessageData messageData) { }

        private void BtnClickProfile(object sender, RoutedEventArgs e) {
            Profile profile = new Profile();
            profile.Show();
            this.Close();

        }

        private void BtnClickShowFriends(object sender, RoutedEventArgs e) {
            ViewboxFriends.Visibility = ViewboxFriends.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }

        private void BtnClickJoinLobby(object sender, RoutedEventArgs e) {
            ViewboxJoinLobby.Visibility = ViewboxJoinLobby.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }

        private void BtnClickJoinLobbyWithCode(object sender, RoutedEventArgs e) {
            try {
                InstanceContext context = new InstanceContext(this);
                lobbyManagerClient = new GoatverseService.LobbyManagerClient(context);
                string lobbyCode = txtBoxLobbyCodeJoin.Text;
                bool connectecToLobby = lobbyManagerClient.ServiceConnectToLobby(usernamePlayer, lobbyCode);
                if (connectecToLobby) { 
                    int playersInLobby = lobbyManagerClient.ServiceCountPlayersInLobby(lobbyCode);
                    if (playersInLobby < 4) {
                        Lobby lobby = new Lobby(lobbyCode);
                        lobby.Show();
                        this.Close();
                    } else {
                        MessageBox.Show("");
                    }
                } else {
                    MessageBox.Show("Lobby doesn't exists");
                }
                
            } catch (FaultException ex) {
                MessageBox.Show(ex.Message);
            }
            

        }

        public void ServiceUpdatePlayersInLobby(PlayerData[] players) { }

        public void BtnClickLogOut(object sender, RoutedEventArgs e) {
            UserSessionManager.GetInstance().LogoutUser();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
