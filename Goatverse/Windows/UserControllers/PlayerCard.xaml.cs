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
    /// Lógica de interacción para PlayerCard.xaml
    /// </summary>
    public partial class PlayerCard : UserControl {
        public PlayerCard() {
            InitializeComponent();
        }

        public static readonly DependencyProperty GamertagProperty = DependencyProperty.Register("Gamertag", typeof(string), typeof(PlayerCard), new PropertyMetadata(string.Empty));

        public string Gamertag {
            get { return (string)GetValue(GamertagProperty); }
            set { SetValue(GamertagProperty, value); }
        }

        public static readonly DependencyProperty LevelProperty = DependencyProperty.Register("Level", typeof(int), typeof(PlayerCard), new PropertyMetadata(0));

        public int Level {
            get { return (int)GetValue(LevelProperty); }
            set { SetValue(LevelProperty, value); }
        }

        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(PlayerCard), new PropertyMetadata(null));

        public ImageSource ImageSource {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }
    }
}
