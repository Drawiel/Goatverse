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

        public Profile() {
            InitializeComponent();

            UserSession userSession = new UserSession();
            userSession = UserSessionManager.GetInstance().GetUser();
            usernamePlayer = userSession.Username;
            emailInstance = userSession.Email;
            profileManager = new GoatverseService.ProfilesManagerClient();

            ProfileData profileData = profileManager.ServiceLoadProfileData(usernamePlayer);
            txtBlockProfileLevelNumber.Text = profileData.ProfileLevel.ToString();
            txtBlockUsername.Text = usernamePlayer;
            
        }

        private void LoadProfileData() {
            try {
                ProfileData profileData = profileManager.ServiceLoadProfileData(usernamePlayer);
                txtBlockProfileLevelNumber.Text = profileData.ProfileLevel.ToString();
                txtBlockUsername.Text = usernamePlayer;
                txtBoxUsername.Text = usernamePlayer;
            } catch (Exception ex) {
                MessageBox.Show($"Error al cargar datos del perfil: {ex.Message}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {

        }

        private void OnArrowLeftClick(object sender, RoutedEventArgs e) {
            Start start = new Start();
            start.Show();
            this.Close();
        }
        private void BtnClickChangeProfileImage(object sender, RoutedEventArgs e) {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Title = "Seleccionar imagen de perfil";
            dlg.Filter = "Archivos de imagen (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";

            bool? result = dlg.ShowDialog();

            if(result == true) {

                ProfileImage.Source = new BitmapImage(new Uri(dlg.FileName));
            }
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
    }
}
