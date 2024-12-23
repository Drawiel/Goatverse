﻿using System;
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
    /// Interaction logic for UserChat.xaml
    /// </summary>
    public partial class UserChat : UserControl {

        public UserChat() {
            InitializeComponent();
        }

        public static readonly DependencyProperty UserNamePropierty = DependencyProperty.Register("Username", typeof(string), typeof(UserChat), new PropertyMetadata(string.Empty));

        public string Username {
            get { return (string)GetValue(UserNamePropierty); }
            set { SetValue(UserNamePropierty, value); }
        }

        public ImageSource Image {
            get { return (ImageSource)GetValue(ImagePropierty); }
            set { SetValue(ImagePropierty, value); }
        }
        public static readonly DependencyProperty ImagePropierty = DependencyProperty.Register("Image", typeof(ImageSource), typeof(UserChat));

    }
}
