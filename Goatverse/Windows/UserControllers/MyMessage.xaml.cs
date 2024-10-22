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
    /// Interaction logic for MyMessage.xaml
    /// </summary>
    public partial class MyMessage : UserControl {
        public MyMessage() {
            InitializeComponent();
        }

        public static readonly DependencyProperty MessagePropierty = DependencyProperty.Register("Message", typeof(string), typeof(MyMessage), new PropertyMetadata(string.Empty));

        public string Message {
            get { return (string)GetValue(MessagePropierty); }
            set { SetValue(MessagePropierty, value); }
        }

        public static readonly DependencyProperty ColorPropierty = DependencyProperty.Register("Color", typeof(string), typeof(MyMessage), new PropertyMetadata(string.Empty));

        public string Color {
            get { return (string)GetValue(ColorPropierty); }
            set { SetValue(ColorPropierty, value); }
        }

        public static readonly DependencyProperty HorizontalAlignmentProperty = DependencyProperty.Register("HorizontalAlignment", typeof(string), typeof(MyMessage), new PropertyMetadata(string.Empty));

        public string HorizontalAlignment {
            get { return (string)GetValue(HorizontalAlignmentProperty); }
            set { SetValue(HorizontalAlignmentProperty, value); }
        }
    }
}
