using Goatverse.GoatverseService;
using Goatverse.Logic.Classes;
using Goatverse.Properties.Langs;
using Goatverse.Windows;
using log4net.Config;
using log4net;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
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
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();
        }

        private void BtnClickLogIn(object sender, RoutedEventArgs e) {
            try {
                GoatverseService.UsersManagerClient usersManagerClient = new GoatverseService.UsersManagerClient();
                string username = textBoxUsernameLogIn.Text;
                string password = passwordBoxPasswordLogIn.Password.ToString();

                GoatverseService.UserData userData = new GoatverseService.UserData();
                userData.Username = username;
                userData.Password = password;

                if (!usersManagerClient.ServiceUserExistsByUsername(username)) {
                    MessageBox.Show(Lang.messageNotExistingUsername);
                    return;
                }

                if (!usersManagerClient.ServiceVerifyPassword(password, username)) {
                    MessageBox.Show(Lang.messageWrongPassword);
                    return;
                }

                bool login = usersManagerClient.ServiceTryLogin(userData);
                if (login) {
                    UserSession userSession = new UserSession {
                        Username = username,
                        Email = usersManagerClient.ServiceGetEmail(username),
                    };
                    UserSessionManager.GetInstance().LoginUser(userSession);

                    Start start = new Start();
                    start.Show();
                    this.Close();
                } else {
                    MessageBox.Show(Lang.messageWrongPassword);
                }
            } catch (Exception ex) {
                ExceptionHandler.HandleServiceException(ex);
            }


        }

        private void CardMouseEnter(object sender, MouseEventArgs e) {
            var card = sender as MaterialDesignThemes.Wpf.Card;

            ThicknessAnimation marginAnimation = new ThicknessAnimation {
                From = card.Margin,
                To = new Thickness(10, -500, 10, -20),
                Duration = TimeSpan.FromSeconds(0.3) 
            };

            
            card.BeginAnimation(FrameworkElement.MarginProperty, marginAnimation);
        }

        private void CardMouseLeave(object sender, MouseEventArgs e) {
            var card = sender as MaterialDesignThemes.Wpf.Card;

            ThicknessAnimation marginAnimation = new ThicknessAnimation {
                From = card.Margin,
                To = new Thickness(10, 0, 10, -20),
                Duration = TimeSpan.FromSeconds(0.3)
            };


            card.BeginAnimation(FrameworkElement.MarginProperty, marginAnimation);
        }

        private void BtnClickSignIn(object sender, RoutedEventArgs e) {
            try {
                GoatverseService.UsersManagerClient usersManagerClient = new GoatverseService.UsersManagerClient();
                string username = textBoxUsernameSignIn.Text;
                string password = passwordBoxPasswordSignIn.Password.ToString();
                string email = textBoxEmail.Text;

                GoatverseService.UserData userData = new GoatverseService.UserData();
                userData.Username = username;
                userData.Password = password;
                userData.Email = email;

                if (usersManagerClient.ServiceUserExistsByUsername(username)) {
                    MessageBox.Show(Lang.messageExistingUsername);
                    return;
                }

                if (!FieldValidator.IsValidPassword(password)) {
                    MessageBox.Show(Lang.messageNotValidPassword);
                    return;
                }

                if (!FieldValidator.IsValidEmail(email)) { 
                    MessageBox.Show(Lang.messageNotValidEmail);
                    return;
                }

                bool login = usersManagerClient.ServiceTrySignIn(userData);
                if (login) { 
                    MessageBox.Show(Lang.messageSuccessfulSignIn);
                } 
            } catch (Exception ex) {
                ExceptionHandler.HandleServiceException(ex);
            }

        }

        private void BtnClickExit(object sender, RoutedEventArgs e) { 
            this.Close();
        }

        private void BtnClickPlayAsGuest(object sender, RoutedEventArgs e) { 
            string username = "Guest " + textBoxUsernameGuest.Text;
            UserSession userSession = new UserSession {
                Username = username,
            };
            UserSessionManager.GetInstance().LoginUser(userSession);

            Start start = new Start();
            start.Show();
            this.Close();

        }
    }
}
