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

namespace Goatverse.Windows {

    public partial class Profile : Window{
        private string usernamePlayer;
        private string emailInstance;
        private GoatverseService.ProfilesManagerClient profileManager;
        private GoatverseService.UsersManagerClient userManager;
        private int imageId = -1;
        private Button lastSelectedButton = null;

        public Profile() {
            InitializeComponent();

            UserSession userSession = new UserSession();
            userSession = UserSessionManager.GetInstance().GetUser();
            usernamePlayer = userSession.Username;
            emailInstance = userSession.Email;
            profileManager = new GoatverseService.ProfilesManagerClient();

            LoadProfileData();
        }

        private void LoadProfileData() {
            try {
                ProfileData profileData = profileManager.ServiceLoadProfileData(usernamePlayer);
                txtBlockProfileLevelNumber.Text = profileData.ProfileLevel.ToString();
                txtBlockUsername.Text = usernamePlayer;
                imageId = profileData.ImageId;
                Changeimage();
            } catch (Exception ex) {
                MessageBox.Show($"Error al cargar datos del perfil: {ex.Message}");
            }
        }

        private void Changeimage() {
            string imagePath = "../Multimedia/sword.png";
            if (imageId != 0 && imageId != -1) {
                imagePath = $"../Multimedia/gato{imageId}.png";
            } 

            imgProfile.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative)); ;
        }

        private void OnArrowLeftClick(object sender, RoutedEventArgs e) {
            Start start = new Start();
            start.Show();
            this.Close();
        }

        private void BtnClickChangeProfileImage(object sender, RoutedEventArgs e) {
            ViewboxSelectImage.Visibility = ViewboxSelectImage.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }

        private void BtnClickUpdateProfile(object sender, RoutedEventArgs e) {
            try {
                string newUsername = txtBoxUsername.Text;
                string oldPassword = pwdBoxOldPassword.Password.ToString();
                string newPassword = pwdBoxNewPassword.Password.ToString();
                bool updatedResult = false;
                userManager = new GoatverseService.UsersManagerClient();

                if(emailInstance != null) {
                    var userData = new UserData {
                        Username = newUsername,  
                        Email = emailInstance,
                        Password = newPassword
                    };

                    if (!string.IsNullOrEmpty(oldPassword) && !string.IsNullOrEmpty(newUsername)) {
                        
                        updatedResult = UpdatePassword(userData, oldPassword);

                    } else if (!string.IsNullOrEmpty(newUsername) && string.IsNullOrEmpty(oldPassword)) {
                        updatedResult = userManager.ServiceUsernameChanged(userData);

                        UserSession userSession = new UserSession {
                            Username = newUsername,
                            Email = emailInstance,
                        };
                        UserSessionManager.GetInstance().LoginUser(userSession);
                        txtBlockUsername.Text = newUsername;

                    } else if (!string.IsNullOrEmpty(newUsername) && !string.IsNullOrEmpty(oldPassword)) {
                        updatedResult = UpdateUsernameAndPassword(userData, oldPassword);
                        UserSession userSession = new UserSession {
                            Username = newUsername,
                            Email = emailInstance,
                        };
                        UserSessionManager.GetInstance().LoginUser(userSession);
                        txtBlockUsername.Text = newUsername;
                    }
                    
                    if (updatedResult) {
                        MessageBox.Show("User changed");
                    } else {
                        MessageBox.Show("Error: User not changed");
                    }
                }
                else {
                    MessageBox.Show("No se pudo recuperar el correo electrónico.");
                }
            }
            catch(Exception ex) {
                MessageBox.Show($"Error al intentar actualizar el perfil: {ex.Message}");
            }
        }

        private bool UpdatePassword(UserData userData, string oldPassword) {
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
        }

        private bool UpdateUsernameAndPassword(UserData userData, string oldPassword) {
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
        }

        private void BtnClickSaveChanges(object sender, RoutedEventArgs e) {
            if (imageId != -1) {
                bool imageChanged = profileManager.ServiceChangeProfileImage(usernamePlayer, imageId);

                if (imageChanged) {
                    MessageBox.Show("image changed");
                    Changeimage();
                } else {
                    MessageBox.Show("Error");
                }
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
    }
}
