using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using IOPath = System.IO.Path; 

namespace Goatverse.Windows {
    /// <summary>
    /// Lógica de interacción para Match.xaml
    /// </summary>
    public partial class Match : Window {
        private StackPanel playersCardsStackPlanel;
        private List<Border> selectedCards = new List<Border>();
        public Match() {
            InitializeComponent();
            playersCardsStackPlanel = this.FindName("stackPanelPlayersCards") as StackPanel;
            InitializePlayerCards(3);
        }

        private void InitializePlayerCards(int initialCardCount) {
            for(int i = 0; i < initialCardCount; i++) {
                AddCardToPanel(i);
            }
        }

        private Border selectedCard = null;

        private void AddCardToPanel(int cardIndex) {
            var newCard = new Border {
                Width = 180,
                Height = 250,
                CornerRadius = new CornerRadius(10),
                Background = new SolidColorBrush(Color.FromRgb(255, 250, 250)),
                Style = (Style)FindResource("ClickableCardStyle"),
                Margin = new Thickness(-30 - (5 * cardIndex), 10, 10, 10),
                Effect = new DropShadowEffect {
                    BlurRadius = 10,
                    ShadowDepth = 5,
                    Color = Colors.Gray
                },
                DataContext = $"Tipo{cardIndex % 2}" 
            };

            newCard.MouseLeftButtonDown += ToggleCardPosition;

            var stackPanel = new StackPanel {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            var textBlock = new TextBlock {
                Text = $"Carta {cardIndex + 1}",
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(5)
            };
            stackPanel.Children.Add(textBlock);
            newCard.Child = stackPanel;

            stackPanelPlayersCards.Children.Add(newCard);
        }


        private void ToggleCardPosition(object sender, MouseButtonEventArgs e) {
            if(sender is Border clickedCard) {
                // Des-seleccionar carta
                if(clickedCard.Tag?.ToString() == "Clicked") {
                    clickedCard.Margin = new Thickness(clickedCard.Margin.Left, 10, clickedCard.Margin.Right, 10);
                    clickedCard.Tag = null;
                    selectedCards.Remove(clickedCard);
                }
                // Seleccionar carta
                else {
                    if(selectedCards.Count >= 2) {
                        MessageBox.Show("Solo puedes seleccionar dos cartas a la vez.", "Límite de selección", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    clickedCard.Margin = new Thickness(clickedCard.Margin.Left, -20, clickedCard.Margin.Right, 10);
                    clickedCard.Tag = "Clicked";
                    selectedCards.Add(clickedCard);

                    if(selectedCards.Count == 2) {
                        CheckAndStackCards();
                    }
                }
            }
        }
        private void CheckAndStackCards() {
            if(selectedCards.Count == 2) {
                var card1 = selectedCards[0];
                var card2 = selectedCards[1];

                if(card1 == null || card2 == null) {
                    MessageBox.Show("Ocurrió un error: Una de las cartas seleccionadas no existe.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if(card1.DataContext == null || card2.DataContext == null) {
                    MessageBox.Show("Las cartas no tienen información asignada.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if(card1.DataContext.ToString() == card2.DataContext.ToString()) {
                    MessageBox.Show("¡Cartas iguales! Se crea un stack.", "Stack creado", MessageBoxButton.OK, MessageBoxImage.Information);

                    stackPanelPlayersCards.Children.Remove(card1);
                    stackPanelPlayersCards.Children.Remove(card2);

                    var stackContainer = new StackPanel {
                        Orientation = Orientation.Vertical,
                        Margin = new Thickness(10)
                    };
                    stackContainer.Children.Add(card1);
                    stackContainer.Children.Add(card2);
                    stackPanelPlayerStacks.Children.Add(stackContainer);
                }
                else {
                    MessageBox.Show("Las cartas no coinciden.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                foreach(var card in selectedCards) {
                    card.Margin = new Thickness(card.Margin.Left, 10, card.Margin.Right, 10);
                    card.Tag = null;
                }
                selectedCards.Clear();
            }
        }


        private void BtnClickTakeCard(object sender, RoutedEventArgs e) {
            int maxCardCount = 5;
            int currentCardCount = stackPanelPlayersCards.Children.Count;

            if(currentCardCount >= maxCardCount) {
                MessageBox.Show("No puedes tener más de 5 cartas al mismo tiempo.", "Límite alcanzado", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string randomImagePath = GetRandomCardImagePath();

            var newCard = new Border {
                Width = 180,
                Height = 250,
                CornerRadius = new CornerRadius(10),
                Background = new ImageBrush {
                    ImageSource = new BitmapImage(new Uri(randomImagePath, UriKind.Relative))
                },
                Style = (Style)FindResource("ClickableCardStyle"),
                Margin = new Thickness(-30 - (5 * currentCardCount), 10, 10, 10),
                Effect = new DropShadowEffect {
                    BlurRadius = 10,
                    ShadowDepth = 5,
                    Color = Colors.Gray
                }
            };
            newCard.MouseLeftButtonDown += ToggleCardPosition;
            stackPanelPlayersCards.Children.Add(newCard);
        }

        private string GetRandomCardImagePath() {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                string projectRoot = IOPath.Combine(baseDirectory, @"..\..\..\");
                string folderPath = IOPath.Combine(projectRoot, "Goatverse", "Multimedia", "Cards");
                folderPath = IOPath.GetFullPath(folderPath);
                if(!Directory.Exists(folderPath)) {
                    throw new DirectoryNotFoundException($"La carpeta {folderPath} no existe.");
                }
                var imageFiles = Directory.GetFiles(folderPath, "*.png");

                if(imageFiles.Length == 0) {
                    throw new InvalidOperationException("No hay imágenes disponibles en la carpeta.");
                }

                var random = new Random();
                int randomIndex = random.Next(imageFiles.Length);
                return imageFiles[randomIndex];
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

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.RegisterAttached("IsSelected", typeof(bool), typeof(Match), new PropertyMetadata(false));
    }
}
