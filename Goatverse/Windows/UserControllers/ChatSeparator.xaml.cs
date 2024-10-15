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
    /// Interaction logic for ChatSeparator.xaml
    /// </summary>
    public partial class ChatSeparator : UserControl {
        public ChatSeparator() {
            InitializeComponent();
        }

        public string Title {
            get { return (string)GetValue(TitlePropierty); }
            set { SetValue(TitlePropierty, value); }
        }
        public static readonly DependencyProperty TitlePropierty = DependencyProperty.Register("Title", typeof(string), typeof(ChatSeparator));
    }
}
