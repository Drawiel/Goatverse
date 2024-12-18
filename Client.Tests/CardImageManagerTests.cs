using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Goatverse.Logic.Classes;

namespace Goatverse.Tests {
    [TestClass]
    public class CardImageManagerTests {

        [TestMethod]
        public void TestGetImagePathValidId() {
            // Arrange
            int imageId = 3; // ID conocido
            string expectedPath = "../../Multimedia/Cards/notpac_goat.png";

            // Act
            string result = CardImageManager.GetImagePath(imageId);

            // Assert
            Assert.AreEqual(expectedPath, result, $"The path for imageId {imageId} should be {expectedPath}");
        }

        [TestMethod]
        public void TestGetImagePathDefaultPath() {
            // Arrange
            int imageId = 999; // ID no registrado
            string expectedPath = "../../Multimedia/Cards/goat.png";

            // Act
            string result = CardImageManager.GetImagePath(imageId);

            // Assert
            Assert.AreEqual(expectedPath, result, $"The path for invalid imageId {imageId} should default to {expectedPath}");
        }

        [TestMethod]
        public void TestGetImageSourceFromImageCardIdValidId() {
            // Arrange
            int imageId = 2; // ID conocido
            string expectedPath = "../../Multimedia/Cards/goat_salvatore.png";

            // Act
            ImageSource result = CardImageManager.GetImageSouceFromImageCardId(imageId);
            BitmapImage bitmapResult = result as BitmapImage;

            // Assert
            Assert.IsNotNull(bitmapResult, "The result should be a valid BitmapImage object");
            Assert.AreEqual(new Uri(expectedPath, UriKind.Relative), bitmapResult.UriSource, "The UriSource of the BitmapImage does not match the expected path");
        }

        [TestMethod]
        public void TestGetImageSourceFromImageCardIdInvalidId() {
            // Arrange
            int imageId = 999; // ID no registrado
            string expectedPath = "../../Multimedia/Cards/goat.png";

            // Act
            ImageSource result = CardImageManager.GetImageSouceFromImageCardId(imageId);
            BitmapImage bitmapResult = result as BitmapImage;

            // Assert
            Assert.IsNotNull(bitmapResult, "The result should be a valid BitmapImage object");
            Assert.AreEqual(new Uri(expectedPath, UriKind.Relative), bitmapResult.UriSource, "The UriSource of the BitmapImage for an invalid ID should match the default path");
        }
    }
}
