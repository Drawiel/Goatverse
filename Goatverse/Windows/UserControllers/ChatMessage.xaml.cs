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
    /// Interaction logic for ChatMessage.xaml
    /// </summary>
    public partial class ChatMessage : UserControl {
        public ChatMessage() {
            InitializeComponent();
        }

        public string Message {
            get { return (string)GetValue(MessagePropierty); }
            set { SetValue(MessagePropierty, value); }
        }
        public static readonly DependencyProperty MessagePropierty = DependencyProperty.Register("Message", typeof(string), typeof(ChatMessage));

        public Brush Color {
            get { return (Brush)GetValue(ColorPropierty); }
            set { SetValue(ColorPropierty, value); }
        }
        public static readonly DependencyProperty ColorPropierty = DependencyProperty.Register("Color", typeof(Brush), typeof(ChatMessage));
    }
}
