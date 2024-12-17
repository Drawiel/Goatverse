using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Goatverse.Logic.Classes {
    public class CardImageManager {
        public static ImageSource GetImageSouceFromImageCardId(int imageId) {
            string imagePath = GetImagePath(imageId);
            ImageSource cardImage = new BitmapImage(new Uri(imagePath, UriKind.Relative));
            return cardImage;
        }

        public static string GetImagePath(int imageId) {
            string imagePath = "../../Multimedia/Cards/goat.png";
            switch (imageId) {
                case 1:
                    imagePath = "../../Multimedia/Cards/goat.png";
                    break;
                case 2:
                    imagePath = "../../Multimedia/Cards/goat_salvatore.png";
                    break;
                case 3:
                    imagePath = "../../Multimedia/Cards/notpac_goat.png";
                    break;
                case 4:
                    imagePath = "../../Multimedia/Cards/goat_slayer.png";
                    break;
                case 5:
                    imagePath = "../../Multimedia/Cards/plumber_goat.png";
                    break;
                case 6:
                    imagePath = "../../Multimedia/Cards/goatink.png";
                    break;
                case 7:
                    imagePath = "../../Multimedia/Cards/goat_tactical_assault.png";
                    break;
                case 8:
                    imagePath = "../../Multimedia/Cards/goatzzz.png";
                    break;
                case 9:
                    imagePath = "../../Multimedia/Cards/master_goat.png";
                    break;
            }

            return imagePath;
        }
    }
}
