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

namespace Goatverse.Windows {

    public partial class Profile : Window {
        public Profile() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {

        }

        private void OnArrowLeftClick(object sender, RoutedEventArgs e) {

        }
        private void ChangeProfileImage_Click(object sender, RoutedEventArgs e) {
            // Crear un diálogo para seleccionar una imagen
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Title = "Seleccionar imagen de perfil";
            dlg.Filter = "Archivos de imagen (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";

            bool? result = dlg.ShowDialog();

            if(result == true) {
                // Establecer la nueva imagen
                ProfileImage.Source = new BitmapImage(new Uri(dlg.FileName));
            }
        }


    }
}
