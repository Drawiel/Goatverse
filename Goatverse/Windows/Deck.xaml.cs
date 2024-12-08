using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Goatverse.GoatverseService;

namespace Goatverse.Windows {
    /// <summary>
    /// Lógica de interacción para Deck.xaml
    /// </summary>
    public partial class Deck : Window {
        private GoatverseService.CardsManagerClient cardsManager;
        public Deck() {
            InitializeComponent();
            cardsManager = new GoatverseService.CardsManagerClient();
            ResourceManager test = new ResourceManager("Goatverse.Properties.Langs.Lang", typeof(MainWindow).Assembly);
        }

        public void BtnClickGoat(object sender, RoutedEventArgs e) {
            try {
                int cardId = 1;
                CardData card = cardsManager.ServiceGetCardById(cardId);
                string imagePath = "../Multimedia/Cards/Master Goat.png";

                if(card != null) {
                    ResourceManager test = new ResourceManager("Goatverse.Properties.Langs.Lang", typeof(MainWindow).Assembly);
                    string globalHistory = test.GetString("globalHistoryGoat");
                    txtBlockHistory.Text = globalHistory;
                    txtBlockCardName.Text = card.CardName;
                    imageGoat.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

                }
                else {
                    MessageBox.Show("Card not found.");
                }
            } catch(Exception ex) {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        public void BtnClickGoatSalvatore(object sender, RoutedEventArgs e) {
            try {
                int cardId = 2;
                CardData card = cardsManager.ServiceGetCardById(cardId);
                string imagePath = "../Multimedia/Cards/Master Goat.png";

                if(card != null) {
                    ResourceManager test = new ResourceManager("Goatverse.Properties.Langs.Lang", typeof(MainWindow).Assembly);
                    string globalHistory = test.GetString("globalHistoryGoatSalvatore");
                    txtBlockHistory.Text = globalHistory;
                    txtBlockCardName.Text = card.CardName;
                    imageGoat.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

                }
                else {
                    MessageBox.Show("Card not found.");
                }
            } catch(Exception ex) {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        public void BtnClickNotpacGoat(object sender, RoutedEventArgs e) {
            try {
                int cardId = 3;
                CardData card = cardsManager.ServiceGetCardById(cardId);
                string imagePath = "../Multimedia/Cards/Master Goat.png";

                if(card != null) {
                    ResourceManager test = new ResourceManager("Goatverse.Properties.Langs.Lang", typeof(MainWindow).Assembly);
                    string globalHistory = test.GetString("globalHistoryNotpacGoat");
                    txtBlockHistory.Text = globalHistory;
                    txtBlockCardName.Text = card.CardName;
                    imageGoat.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

                }
                else {
                    MessageBox.Show("Card not found.");
                }
            } catch(Exception ex) {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        public void BtnClickGoatSlayer(object sender, RoutedEventArgs e) {
            try {
                int cardId = 4;
                CardData card = cardsManager.ServiceGetCardById(cardId);
                string imagePath = "../Multimedia/Cards/Master Goat.png";

                if(card != null) {
                    ResourceManager test = new ResourceManager("Goatverse.Properties.Langs.Lang", typeof(MainWindow).Assembly);
                    string globalHistory = test.GetString("globalHistoryGoatSlayer");
                    txtBlockHistory.Text = globalHistory;
                    txtBlockCardName.Text = card.CardName;
                    imageGoat.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

                }
                else {
                    MessageBox.Show("Card not found.");
                }
            } catch(Exception ex) {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        public void BtnClickPlumberGoat(object sender, RoutedEventArgs e) {
            try {
                int cardId = 5;
                CardData card = cardsManager.ServiceGetCardById(cardId);
                string imagePath = "../Multimedia/Cards/Master Goat.png";

                if(card != null) {
                    ResourceManager test = new ResourceManager("Goatverse.Properties.Langs.Lang", typeof(MainWindow).Assembly);
                    string globalHistory = test.GetString("globalHistoryPlumberGoat");
                    txtBlockHistory.Text = globalHistory;
                    txtBlockCardName.Text = card.CardName;
                    imageGoat.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

                }
                else {
                    MessageBox.Show("Card not found.");
                }
            } catch(Exception ex) {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        public void BtnClickGoatink(object sender, RoutedEventArgs e) {
            try {
                int cardId = 6;
                CardData card = cardsManager.ServiceGetCardById(cardId);
                string imagePath = "../Multimedia/Cards/Master Goat.png";

                if(card != null) {
                    ResourceManager test = new ResourceManager("Goatverse.Properties.Langs.Lang", typeof(MainWindow).Assembly);
                    string globalHistory = test.GetString("globalHistoryGoatLink");
                    txtBlockHistory.Text = globalHistory;
                    txtBlockCardName.Text = card.CardName;
                    imageGoat.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

                }
                else {
                    MessageBox.Show("Card not found.");
                }
            } catch(Exception ex) {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        public void BtnClickGTA(object sender, RoutedEventArgs e) {
            try {
                int cardId = 7;
                CardData card = cardsManager.ServiceGetCardById(cardId);
                string imagePath = "../Multimedia/Cards/Master Goat.png";

                if(card != null) {
                    ResourceManager test = new ResourceManager("Goatverse.Properties.Langs.Lang", typeof(MainWindow).Assembly);
                    string globalHistory = test.GetString("globalHistoryG.T.A.");
                    txtBlockHistory.Text = globalHistory;
                    txtBlockCardName.Text = card.CardName;
                    imageGoat.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

                }
                else {
                    MessageBox.Show("Card not found.");
                }
            } catch(Exception ex) {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        public void BtnClickGoatzzz(object sender, RoutedEventArgs e) {
            try {
                int cardId = 8;
                CardData card = cardsManager.ServiceGetCardById(cardId);
                string imagePath = "../Multimedia/Cards/Master Goat.png";

                if(card != null) {
                    ResourceManager test = new ResourceManager("Goatverse.Properties.Langs.Lang", typeof(MainWindow).Assembly);
                    string globalHistory = test.GetString("globalHistoryGoatzzz");
                    txtBlockHistory.Text = globalHistory;
                    txtBlockCardName.Text = card.CardName;
                    imageGoat.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

                }
                else {
                    MessageBox.Show("Card not found.");
                }
            } catch(Exception ex) {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        public void BtnClickMasterGoat(object sender, RoutedEventArgs e) {
            try {
                int cardId = 9;
                CardData card = cardsManager.ServiceGetCardById(cardId);
                string imagePath = "../Multimedia/Cards/Master Goat.png";

                if(card != null) {
                    ResourceManager test = new ResourceManager("Goatverse.Properties.Langs.Lang", typeof(MainWindow).Assembly);
                    string globalHistory = test.GetString("globalHistoryMasterGoat");
                    txtBlockHistory.Text = globalHistory;
                    txtBlockCardName.Text = card.CardName;
                    imageGoat.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

                }
                else {
                    MessageBox.Show("Card not found.");
                }
            } catch(Exception ex) {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        public void BtnClickSliceOfGoat(object sender, RoutedEventArgs e) {
            try {
                int cardId = 10;
                CardData card = cardsManager.ServiceGetCardById(cardId);
                string imagePath = "../Multimedia/Cards/Master Goat.png";

                if(card != null) {
                    ResourceManager test = new ResourceManager("Goatverse.Properties.Langs.Lang", typeof(MainWindow).Assembly);
                    string globalHistory = test.GetString("globalHistoryMasterGoat");
                    txtBlockHistory.Text = globalHistory;
                    txtBlockCardName.Text = card.CardName;
                    imageGoat.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

                }
                else {
                    MessageBox.Show("Card not found.");
                }
            } catch(Exception ex) {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        public void BtnClickEscapeGoat(object sender, RoutedEventArgs e) {
            try {
                int cardId = 11;
                CardData card = cardsManager.ServiceGetCardById(cardId);
                string imagePath = "../Multimedia/Cards/Master Goat.png";

                if(card != null) {
                    ResourceManager test = new ResourceManager("Goatverse.Properties.Langs.Lang", typeof(MainWindow).Assembly);
                    string globalHistory = test.GetString("globalHistoryMasterGoat");
                    txtBlockHistory.Text = globalHistory;
                    txtBlockCardName.Text = card.CardName;
                    imageGoat.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

                }
                else {
                    MessageBox.Show("Card not found.");
                }
            } catch(Exception ex) {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        public void BtnClickGoatsRampage(object sender, RoutedEventArgs e) {
            try {
                int cardId = 12;
                CardData card = cardsManager.ServiceGetCardById(cardId);
                string imagePath = "../Multimedia/Cards/Master Goat.png";

                if(card != null) {
                    ResourceManager test = new ResourceManager("Goatverse.Properties.Langs.Lang", typeof(MainWindow).Assembly);
                    string globalHistory = test.GetString("globalHistoryMasterGoat");
                    txtBlockHistory.Text = globalHistory;
                    txtBlockCardName.Text = card.CardName;
                    imageGoat.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

                }
                else {
                    MessageBox.Show("Card not found.");
                }
            } catch(Exception ex) {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

    }
}
