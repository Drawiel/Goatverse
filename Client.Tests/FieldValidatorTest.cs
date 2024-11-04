using Microsoft.VisualStudio.TestTools.UnitTesting;
using Goatverse.Logic.Classes;

namespace Goatverse.Tests {
    [TestClass]
    public class FieldValidatorTest {

        [TestMethod]
        public void TestPasswordValid() {
            //Arrange
            string password = "Valid123!";

            //Act
            bool result = FieldValidator.IsValidPassword(password);
            
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestPasswordTooShort() {
            //Arrange
            string password = "Val1!";
            
            //Act
            bool result = FieldValidator.IsValidPassword(password);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestPasswordNoUpperCase() {
            //Arrange
            string password = "valid123!";

            //Act
            bool result = FieldValidator.IsValidPassword(password);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestPasswordNoNumber() {
            //Arrange
            string password = "ValidPass!";

            //Act
            bool result = FieldValidator.IsValidPassword(password);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestPasswordNoSpecialCharacter() {
            //Arrange
            string password = "Valid123";

            //Act
            bool result = FieldValidator.IsValidPassword(password);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestPasswordOnlyLowerCase() {
            //Arrange
            string password = "password";

            //Act
            bool result = FieldValidator.IsValidPassword(password);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestPasswordOnlyUpperCaseAndNumbers() {
            //Arrange
            string password = "PASSWORD123";

            //Act
            bool result = FieldValidator.IsValidPassword(password);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestValidEmail() {
            //Arrange
            string email = "example@example.com";

            //Act
            bool result = FieldValidator.IsValidEmail(email);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestEmailWithoutAtSymbol() {
            //Arrange
            string email = "example.com";

            //Act
            bool result = FieldValidator.IsValidEmail(email);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestEmailWithoutDomain() {
            //Arrange
            string email = "example@.com";

            //Act
            bool result = FieldValidator.IsValidEmail(email);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestEmailWithSpaces() {
            //Arrange
            string email = "example @example.com";

            //Act
            bool result = FieldValidator.IsValidEmail(email);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestEmailWithoutTopLevelDomain() {
            //Arrange
            string email = "example@example";

            //Act
            bool result = FieldValidator.IsValidEmail(email);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestEmailWithMultipleAtSymbols() {
            //Arrange
            string email = "example@@example.com";

            //Act
            bool result = FieldValidator.IsValidEmail(email);

            //Assert
            Assert.IsFalse(result);
        }
    }
}
