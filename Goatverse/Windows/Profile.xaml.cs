using Goatverse.Logic.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
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
using Goatverse.GoatverseService;
using Goatverse.Properties.Langs;
using System.ServiceModel;
using System.ComponentModel;

namespace Goatverse.Windows {

    public partial class Profile : Window, GoatverseService.IMatchManagerCallback {

        private string usernamePlayer;
        private string emailInstance;
        private GoatverseService.ProfilesManagerClient profileManager;
        private GoatverseService.UsersManagerClient userManager;
        private GoatverseService.MatchManagerClient matchManager;
        private int imageId = -1;
        private Button lastSelectedButton = null;
        public List<MatchData> RecentMatches { get; set; }
        private int _totalWins;
        public int TotalWins {
            get { return _totalWins; }
            set {
                if(_totalWins != value) {
                    _totalWins = value;
                    OnPropertyChanged(nameof(TotalWins)); 
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public Profile() {
            InitializeComponent();

            UserSession userSession = UserSessionManager.GetInstance().GetUser();
            usernamePlayer = userSession.Username;
            emailInstance = userSession.Email;
            profileManager = new GoatverseService.ProfilesManagerClient();
            matchManager = new GoatverseService.MatchManagerClient(new InstanceContext(this));
            this.DataContext = this;

            LoadProfileData();
            LoadRecentMatches(); 
            LoadWonMatches();
        }


        private void LoadProfileData() {
            try {
                ProfileData profileData = profileManager.ServiceLoadProfileData(usernamePlayer);
                txtBlockProfileLevelNumber.Text = profileData.ProfileLevel.ToString();
                txtBlockUsername.Text = usernamePlayer;
                imageId = profileData.ImageId;
                Changeimage();
            } catch (Exception ex) {
               ExceptionHandler.HandleServiceException(ex);
            }
        }

        public void OnMatchDataReceived(MatchData matchData) {
        }

        private void LoadRecentMatches() {
            try {
                RecentMatches = matchManager.ServiceGetRecentMatches(10).ToList();

                if (RecentMatches.Count != 0) {
                    RecentMatchesControl.ItemsSource = RecentMatches;
                } else {
                    MessageBox.Show(Lang.messageDatabaseError);
                }

            } catch (Exception ex) {
                ExceptionHandler.HandleServiceException(ex);
            }
        }

        private void LoadWonMatches() {
            try {

                var matches = profileManager.ServiceGetWonMatchesByUsername(usernamePlayer);
                TotalWins = matches;


            } catch(Exception ex) {
                ExceptionHandler.HandleServiceException(ex);
            }
        }

        private void Changeimage() {
            try {
                string imagePath = "../Multimedia/sword.png";
                if (imageId != 0 && imageId != -1) {
                    imagePath = $"../Multimedia/gato{imageId}.png";
                }

                imgProfile.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

            } catch(Exception ex) {
                ExceptionHandler.HandleServiceException(ex);
            }
            
        }

        private void BtnClickGoBack(object sender, RoutedEventArgs e) {
            Start start = new Start();
            start.Show();
            this.Close();
        }

        private void BtnClickChangeProfileImage(object sender, RoutedEventArgs e) {
            ViewboxSelectImage.Visibility = ViewboxSelectImage.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }

        private void BtnClickUpdateProfile(object sender, RoutedEventArgs e) {
            string newUsername = txtBoxUsername.Text;
            string oldPassword = pwdBoxOldPassword.Password.ToString();
            string newPassword = pwdBoxNewPassword.Password.ToString();

            if (string.IsNullOrEmpty(emailInstance)) {
                MessageBox.Show("No se pudo recuperar el correo electrónico.");
                return;
            }

            userManager = new GoatverseService.UsersManagerClient();
            var userData = new UserData {
                Username = newUsername,
                Email = emailInstance,
                Password = newPassword
            };

            bool updatedResult = UpdateUserProfile(userData, newUsername, oldPassword);

            if (updatedResult) {
                MessageBox.Show("User changed");
                UpdateUserSession(newUsername, emailInstance);
            } else {
                MessageBox.Show("Error: User not changed");
            }
        }

        private void UpdateUserSession(string username, string email) {
            var userSession = new UserSession {
                Username = username,
                Email = email
            };
            UserSessionManager.GetInstance().LoginUser(userSession);
            txtBlockUsername.Text = username;
        }

        private bool UpdateUserProfile(UserData userData, string newUsername, string oldPassword) {
            try {
                if (!string.IsNullOrEmpty(newUsername) && !string.IsNullOrEmpty(oldPassword)) {
                    return UpdateUsernameAndPassword(userData, oldPassword);
                } else if (!string.IsNullOrEmpty(newUsername)) {
                    return userManager.ServiceUsernameChanged(userData);
                } else if (!string.IsNullOrEmpty(oldPassword)) {
                    return UpdatePassword(userData, oldPassword);
                }

                return false;
            } catch (Exception ex) {
                ExceptionHandler.HandleServiceException(ex);
                return false;
            }
        }

        private bool UpdatePassword(UserData userData, string oldPassword) {
            try {
                if (userManager.ServiceVerifyPassword(oldPassword, usernamePlayer)) {

                    if (FieldValidator.IsValidPassword(userData.Password)) {
                        return userManager.ServicePasswordChanged(userData);
                    } else {
                        MessageBox.Show(Lang.messageNotValidPassword);
                        return false;
                    }

                }
                MessageBox.Show("Old Password is Incorrect");
                return false;
            } catch (Exception ex) {
                ExceptionHandler.HandleServiceException(ex);
                return false;
            }
        }

        private bool UpdateUsernameAndPassword(UserData userData, string oldPassword) {
            try {
                if (userManager.ServiceUserExistsByUsername(userData.Username)) { 
                    MessageBox.Show(Lang.messageExistingUsername);
                    return false;
                }
                if (userManager.ServiceVerifyPassword(oldPassword, usernamePlayer)) {
                    if (FieldValidator.IsValidPassword(userData.Password)) {
                        return userManager.ServicePasswordAndUsernameChanged(userData);
                    } else {
                        MessageBox.Show(Lang.messageNotValidPassword);
                        return false;
                    }
                }
                MessageBox.Show("Old Password is Incorrect");
                return false;
            } catch (Exception ex) {
                ExceptionHandler.HandleServiceException(ex);
                return false;
            }
        }

        private void BtnClickSaveChanges(object sender, RoutedEventArgs e) {
            try {
                if (imageId != -1) {
                    bool imageChanged = profileManager.ServiceChangeProfileImage(usernamePlayer, imageId);

                    if (imageChanged) {
                        MessageBox.Show("image changed");
                        Changeimage();
                    } else {
                        MessageBox.Show(Lang.messageDatabaseError);
                    }
                }
            } catch (Exception ex) {
                ExceptionHandler.HandleServiceException(ex);
            }
        }

        private void BtnClickSelectImage(object sender, RoutedEventArgs e) {
            if (sender is Button clickedButton) {
                imageId = int.Parse(clickedButton.Tag.ToString());

                if (lastSelectedButton != null) {
                    lastSelectedButton.BorderBrush = Brushes.Black;
                }
                clickedButton.BorderBrush = Brushes.White;
                lastSelectedButton = clickedButton;
            }

            Console.WriteLine(imageId);
        }

        public void ServiceNotifyEndGame(string winnerUsername) { }

        public void ServiceUpdateCurrentTurn(string currentTurn, Dictionary<string, int> playerPoints) { }

        public void ServiceSyncTimer() { }

        public void ServiceReceiveDeck(Stack<CardData> cards) { }

        public void ServiceRemoveCardFromDeck() { }

        public void ServiceNotifyReturnToStart() { }
    }
}
