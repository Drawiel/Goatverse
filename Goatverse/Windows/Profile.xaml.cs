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
        private GoatverseService.ProfilesManagerClient profileManager;

        public Profile() {
            InitializeComponent();

            UserSession userSession = new UserSession();
            userSession = UserSessionManager.getInstance().getUser();
            usernamePlayer = userSession.Username;
            profileManager = new GoatverseService.ProfilesManagerClient();

            ProfileData profileData = profileManager.ServiceLoadProfileData(usernamePlayer);
            txtBlockProfileLevelNumber.Text = profileData.ProfileLevel.ToString();
            txtBlockUsername.Text = usernamePlayer;
            txtBoxUsername.Text = usernamePlayer;
            
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

        private void Button_Click_2(object sender, RoutedEventArgs e) {

        }
    }
}
