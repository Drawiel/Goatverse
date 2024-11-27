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
    /// Lógica de interacción para ConfirmationMessage.xaml
    /// </summary>
    public partial class ConfirmationMessage : UserControl {
        public ConfirmationMessage() {
            InitializeComponent();
        }

        private void BtnClickCancel(object sender, RoutedEventArgs e) {
            if (this.Parent is Panel parentPanel) {
                parentPanel.Children.Remove(this);
            }
        }
    }
}
