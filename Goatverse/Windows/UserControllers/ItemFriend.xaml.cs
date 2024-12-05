using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Goatverse.GoatverseService;
using MaterialDesignThemes.Wpf;


namespace Goatverse.Windows.UserControllers {
    /// <summary>
    /// Lógica de interacción para ItemFriend.xaml
    /// </summary>
    public partial class ItemFriend : UserControl {
        public Button addButton = new Button {
            Content = "Agregar",
            Margin = new Thickness(5, 0, 0, 0)
        };

        public Button removeButton = new Button {
            Content = "Eliminar",
            Margin = new Thickness(5, 1, 0, 1)
        };

        public Button blockButton = new Button {
            Content = "Bloquear",
            Margin = new Thickness(5, 1, 0, 1)
        };

        public Button unblockButton = new Button {
            Content = "Desbloquear",
            Margin = new Thickness(5, 1, 0, 1)
        };

        public ItemFriend() {
            InitializeComponent();
        }
        
        public static readonly DependencyProperty UsernameProperty = DependencyProperty.Register("Username", typeof(string), typeof(ItemFriend), new PropertyMetadata(string.Empty));

        public string Username {
            get { return (string)GetValue(UsernameProperty); }
            set { SetValue(UsernameProperty, value); }
        }

        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register("Status", typeof(string), typeof(ItemFriend), new PropertyMetadata(string.Empty));

        public string Status {
            get { return (string)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ItemFriend), new PropertyMetadata(null));

        public ImageSource ImageSource {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public void ConfigureButtons(bool isFriend, bool isBlocked) {
            stckPanelButtons.Children.Clear();

            if (!isFriend && !isBlocked) {

                var icon = new PackIcon { Kind = PackIconKind.AccountPlus, };

                addButton.Content = icon;
                stckPanelButtons.Children.Add(addButton);

            } else if(isFriend && !isBlocked){
                bdr.Height = 70;
                var iconRemove = new PackIcon { Kind = PackIconKind.AccountRemove, };

                removeButton.Content = iconRemove;
                stckPanelButtons.Children.Add(removeButton);

                var iconBlock = new PackIcon { Kind = PackIconKind.Block, };

                blockButton.Content = iconBlock;
                stckPanelButtons.Children.Add(blockButton);
            } else if (isBlocked) {
                var iconUnblock = new PackIcon { Kind = PackIconKind.LockOpenVariant, };

                unblockButton.Content = iconUnblock;
                stckPanelButtons.Children.Add(unblockButton);
            }
        }
    }
}
