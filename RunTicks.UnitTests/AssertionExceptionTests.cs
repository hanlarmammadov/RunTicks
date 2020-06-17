using NUnit.Framework;

namespace RunTicks.UnitTests
{
    [TestFixture]
    public class AssertionExceptionTests
    {
        [Test]
        public void Ctor_When_Called_Sets_Message()
        {
            // Arrange
            string message = "some message";

            // Act
            AssertionException assertionException = new AssertionException(message);

            // Assert
            Assert.AreEqual(message, assertionException.Message);
        }

        [Test]
        public void ToString_When_Called_Returns_Message()
        {
            // Arrange
            string message = "some message";
            AssertionException assertionException = new AssertionException(message);

            // Act
            var result = assertionException.ToString();

            // Assert
            Assert.AreEqual(message, result);
        }
    }
}
