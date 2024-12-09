﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
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
using System.Windows.Threading;
using Goatverse.GoatverseService;
using Goatverse.Logic.Classes;
using IOPath = System.IO.Path; 

namespace Goatverse.Windows {
    /// <summary>
    /// Lógica de interacción para Match.xaml
    /// </summary>
    public partial class Match : Window, GoatverseService.IMatchManagerCallback{
        private StackPanel playersCardsStackPlanel;
        private List<Border> selectedCards = new List<Border>();
        private DispatcherTimer turnTimer;
        private int turnTimeRemaining;
        private string lobbyCode;
        private string usernamePlayer;
        private string currentTurnPlayer;


        private GoatverseService.MatchManagerClient matchManagerClient;

        public Match(PlayerData[] playersList, string lobbyCode, string ownerGamertag) {
            UserSession userSession = UserSessionManager.GetInstance().GetUser();
            usernamePlayer = userSession.Username;
            InitializeComponent();
            playersCardsStackPlanel = this.FindName("stackPanelPlayersCards") as StackPanel;
            lblCurrentTurn = this.FindName("lblCurrentTurn") as Label;
            this.lobbyCode = lobbyCode;
            turnTimer = new DispatcherTimer();
            turnTimer.Interval = TimeSpan.FromSeconds(1); 
            turnTimer.Tick += TurnTimer_Tick;
            turnTimeRemaining = 10;
            InstanceContext context = new InstanceContext(this);
            matchManagerClient = new GoatverseService.MatchManagerClient(context);
            matchManagerClient.ServiceConnectToGame(usernamePlayer, lobbyCode);
            matchManagerClient.ServiceInitializeGameTurns(lobbyCode);
        }

        private void TurnTimer_Tick(object sender, EventArgs e) {
            turnTimeRemaining--;
            if (turnTimeRemaining > 0) {
                Debug.WriteLine($"Tiempo restante: {turnTimeRemaining}");
                lblTimeRemaining.Content = $"Tiempo restante: {turnTimeRemaining} segundos";

            } else {
                matchManagerClient.ServiceNotifyEndTurn(lobbyCode, currentTurnPlayer);
                turnTimer.Stop();
            }
        }

        public void UpdateCurrentTurn(string currentTurn) {
            Application.Current.Dispatcher.Invoke(() => {
                lblCurrentTurn.Content = $"Es el turno de: {currentTurn}";
            });
            currentTurnPlayer = currentTurn;
            turnTimeRemaining = 30;
            turnTimer.Start();
        }

        public void SyncTimer() {
            MessageBox.Show("El temporizador ha sido sincronizado.", "Sincronización de temporizador", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void InitializePlayerCards(int initialCardCount) {
            for(int i = 0; i < initialCardCount; i++) {
                AddCardToPanel(i);
            }
        }

        private Border selectedCard = null;


        private void ToggleCardPosition(object sender, MouseButtonEventArgs e) {
            if(sender is Border clickedCard) {
                
                if(clickedCard.Tag?.ToString() == "Clicked") {
                    clickedCard.Margin = new Thickness(clickedCard.Margin.Left, 10, clickedCard.Margin.Right, 10);
                    clickedCard.Tag = null;
                    selectedCards.Remove(clickedCard);
                }
                
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

                    var representativeCard = new Border {
                        Width = 180,
                        Height = 250,
                        CornerRadius = new CornerRadius(10),
                        Background = new SolidColorBrush(Color.FromRgb(200, 200, 200)),
                        BorderBrush = new SolidColorBrush(Color.FromRgb(50, 50, 50)),
                        BorderThickness = new Thickness(2),
                        Margin = new Thickness(10),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center     
                    };

                    var stackContainer = new StackPanel {
                        Orientation = Orientation.Vertical,
                        Margin = new Thickness(10),
                        HorizontalAlignment = HorizontalAlignment.Center, 
                        VerticalAlignment = VerticalAlignment.Center,     
                        Tag = new List<Border> { card1, card2 }
                    };

                    stackContainer.Children.Add(representativeCard);
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


        // crear una carta
        private Border CreateCard(int cardIndex, string randomImagePath = null) {
            Brush background;

            if(randomImagePath != null) {
                background = new ImageBrush {
                    ImageSource = new BitmapImage(new Uri(randomImagePath, UriKind.Relative))
                };
            }
            else {
                background = new SolidColorBrush(Color.FromRgb(255, 250, 250));
            }

            return new Border {
                Width = 180,
                Height = 250,
                CornerRadius = new CornerRadius(10),
                Background = background,
                Style = (Style)FindResource("ClickableCardStyle"),
                Margin = new Thickness(-30 - (5 * cardIndex), 10, 10, 10),
                Effect = new DropShadowEffect {
                    BlurRadius = 10,
                    ShadowDepth = 5,
                    Color = Colors.Gray
                },
                DataContext = $"Tipo{cardIndex % 2}" 
            };
        }

        // añadir una carta al panel
        private void AddCardToPanel(int cardIndex) {
            var newCard = CreateCard(cardIndex); 
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

        private void BtnClickTakeCard(object sender, RoutedEventArgs e) {
            int maxCardCount = 5;
            int currentCardCount = stackPanelPlayersCards.Children.Count;

            if(currentCardCount >= maxCardCount) {
                MessageBox.Show("No puedes tener más de 5 cartas al mismo tiempo.", "Límite alcanzado", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string randomImagePath = GetRandomCardImagePath();
            var newCard = CreateCard(currentCardCount, randomImagePath);
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

        public void ServiceNotifyEndGame(string matchId, string winnerUsername) {
            throw new NotImplementedException();
        }

        public void ServiceUpdateCurrentTurn(string currentTurn) {
            UpdateCurrentTurn(currentTurn);
        }

        public void ServiceSyncTimer() {
            turnTimeRemaining = 30;
            turnTimer.Start();
        }
    }

}
