using Goatverse.Logic.Classes;
using Goatverse.Windows;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static MaterialDesignThemes.Wpf.Theme;

namespace Goatverse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeMaterialDesign();
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e) {
            GoatverseService.UsersManagerClient usersManagerClient = new GoatverseService.UsersManagerClient();
            string username = textBoxUsername.Text;
            string password = passwordBoxPassword.Password.ToString();
            bool login = usersManagerClient.tryLogin(username, password);
            if (login) {
                UserSession userSession = new UserSession { Username = username};
                UserSessionManager.getInstance().loginUser(userSession);
                Lobby lobby = new Lobby();
                lobby.Show();
                this.Close();
            } else {
                textBoxUsername.Text = login.ToString();
            }
            
        }

        private void Card_MouseEnter(object sender, MouseEventArgs e) {
            var card = sender as MaterialDesignThemes.Wpf.Card;

            ThicknessAnimation marginAnimation = new ThicknessAnimation {
                From = card.Margin,
                To = new Thickness(10, -500, 10, -20),
                Duration = TimeSpan.FromSeconds(0.3) 
            };

            
            card.BeginAnimation(FrameworkElement.MarginProperty, marginAnimation);
        }

        private void Card_MouseLeave(object sender, MouseEventArgs e) {
            var card = sender as MaterialDesignThemes.Wpf.Card;

            ThicknessAnimation marginAnimation = new ThicknessAnimation {
                From = card.Margin,
                To = new Thickness(10, 0, 10, -20),
                Duration = TimeSpan.FromSeconds(0.3)
            };


            card.BeginAnimation(FrameworkElement.MarginProperty, marginAnimation);
        }

        private void InitializeMaterialDesign() {
            var card = new MaterialDesignThemes.Wpf.Card();
            var hue = new Hue("Dummy", Colors.Black, Colors.White);
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e) {
            GoatverseService.UsersManagerClient usersManagerClient = new GoatverseService.UsersManagerClient();
            string username = textBoxUsernameSignIn.Text;
            string password = passwordBoxPassword.Password.ToString();
            string email = textBoxEmail.Text;
            bool login = usersManagerClient.trySignIn(username, password, email);
        }
    }
}
