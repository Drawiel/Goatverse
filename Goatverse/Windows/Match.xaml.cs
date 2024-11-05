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
                // Si la carta está marcada como "Clicked", se restaura; de lo contrario, se desplaza
                if(card.Tag?.ToString() == "Clicked") {
                    card.Tag = null; // Elimina el tag para que regrese a su posición original
                }
                else {
                    card.Tag = "Clicked"; // Marca la carta como desplazada
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
    }
}
