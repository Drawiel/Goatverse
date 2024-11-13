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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Goatverse.Windows {
    /// <summary>
    /// Lógica de interacción para Match.xaml
    /// </summary>
    public partial class Match : Window {   
        public Match() {
            InitializeComponent();
        }

        private void ToggleCardPosition(object sender, MouseButtonEventArgs e) {
            if(sender is Border card) {
                if(card.Tag?.ToString() == "Clicked") {
                    card.Tag = null; 
                }
                else {
                    card.Tag = "Clicked"; 
                }
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

        private void ToggleCardSelection(object sender, MouseButtonEventArgs e) {
            var card = sender as Border;
            if(card != null) {
                bool isSelected = (bool)(card.Tag ?? false);
                card.Tag = !isSelected;
                card.SetValue(IsSelectedProperty, !isSelected);
            }
        }

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.RegisterAttached("IsSelected", typeof(bool), typeof(Match), new PropertyMetadata(false));

        public static bool GetIsSelected(UIElement element) {
            return (bool)element.GetValue(IsSelectedProperty);
        }

        public static void SetIsSelected(UIElement element, bool value) {
            element.SetValue(IsSelectedProperty, value);
        }

        private void Card_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            var card = sender as Border;
            if(card != null) {
                card.Tag = card.Tag == null ? "Clicked" : null;
            }
        }
    }
}
