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

namespace Goatverse.Windows {

    public partial class Profile : Window{
        private string usernamePlayer;
        private string emailInstance;
        private GoatverseService.ProfilesManagerClient profileManager;
        private GoatverseService.UsersManagerClient userManager;

        public Profile() {
            InitializeComponent();

            UserSession userSession = new UserSession();
            userSession = UserSessionManager.getInstance().getUser();
            usernamePlayer = userSession.Username;
            emailInstance = userSession.Email;
            profileManager = new GoatverseService.ProfilesManagerClient();

            /*ProfileData profileData = profileManager.ServiceLoadProfileData(usernamePlayer);
            txtBlockProfileLevelNumber.Text = profileData.ProfileLevel.ToString();
            txtBlockUsername.Text = usernamePlayer;
            txtBoxUsername.Text = usernamePlayer;*/
            
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

                        bool isOldPasswordTrue = userManager.ServiceVerifyPassword(oldPassword, usernamePlayer);
                        if (isOldPasswordTrue && newPassword != null) { 
                            updatedResult = userManager.ServicePasswordChanged(userData);
                        } else {
                            MessageBox.Show("Problem with password change");
                        }

                    } else if (!string.IsNullOrEmpty(newUsername) && string.IsNullOrEmpty(oldPassword)) {
                        updatedResult = userManager.ServiceUsernameChanged(userData);


                    } else if (!string.IsNullOrEmpty(newUsername) && !string.IsNullOrEmpty(oldPassword)) {

                        bool isOldPasswordTrue = userManager.ServiceVerifyPassword(oldPassword, usernamePlayer);
                        if (isOldPasswordTrue && !string.IsNullOrEmpty(newPassword)) {
                            updatedResult = userManager.ServicePasswordAndUsernameChanged(userData);
                        } else {
                            MessageBox.Show("Problem with password change");
                        }
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
    }
}
