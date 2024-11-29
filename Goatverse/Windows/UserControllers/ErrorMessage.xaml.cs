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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Goatverse.Windows.UserControllers {
    /// <summary>
    /// Lógica de interacción para ErrorMessage.xaml
    /// </summary>
    public partial class ErrorMessage : UserControl {
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ErrorMessage), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(ErrorMessage), new PropertyMetadata(string.Empty));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public ErrorMessage()
        {
            InitializeComponent();
        }

        private void BtnClickAccept(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed; // Ocultar el mensaje al aceptar
        }

    }
}
