using Moq;
using NUnit.Framework;
using RunTicks.AssertionMessages;
using System;
using System.Globalization;
using System.Text;

namespace RunTicks.UnitTests
{
    [TestFixture]
    public class NotExceedsSecondsAssertionMessageTests
    {
        [Test]
        public void Ctor_When_Called_Sets_Provided_Info_Correctly()
        {
            // Arrange
            Int64 expectedSeconds = 2;
            Double actualSeconds = 5;
            string descr = "some description";

            // Act
            NotExceedsSecondsAssertionMessage notExceedsSecondsAssertionMessage = new NotExceedsSecondsAssertionMessage(expectedSeconds, actualSeconds, descr);

            // Assert
            Assert.AreEqual(expectedSeconds, notExceedsSecondsAssertionMessage.ExpectedMaxSeconds);
            Assert.AreEqual(actualSeconds, notExceedsSecondsAssertionMessage.ActualSeconds);
            Assert.AreEqual(descr, notExceedsSecondsAssertionMessage.Description);
        }
        [Test]
        public void AddSpecificMessagePart_When_Called_Adds_Own_String_To_Provided_StringBuilder()
        {
            // Arrange 
            Int64 expectedSeconds = 2;
            Double actualSeconds = 5;
            string descr = "some description";
            NotExceedsSecondsAssertionMessage notExceedsSecondsAssertionMessage = new NotExceedsSecondsAssertionMessage(expectedSeconds, actualSeconds, descr);
            StringBuilder sb = new StringBuilder();

            // Act
            notExceedsSecondsAssertionMessage.AddSpecificMessagePart(sb, CultureInfo.InvariantCulture);

            // Assert
            var resString = sb.ToString();
            var expectedString = $"Expected total seconds not to exceed 2, but had: 5 total seconds.{Environment.NewLine}";
            Assert.AreEqual(expectedString, resString);
        }
        [Test]
        public void AddSpecificMessagePart_When_Called_Uses_CultureInfo()
        {
            // Arrange
            Int64 expectedSeconds = 2;
            Double actualSeconds = 5;
            string descr = "some description";
            NotExceedsSecondsAssertionMessage notExceedsSecondsAssertionMessage = new NotExceedsSecondsAssertionMessage(expectedSeconds, actualSeconds, descr);
            StringBuilder sb = new StringBuilder();
            var cultureInfoMock = new Mock<CultureInfo>("es-ES");

            // Act
            notExceedsSecondsAssertionMessage.AddSpecificMessagePart(sb, cultureInfoMock.Object);

            // Assert
            cultureInfoMock.VerifyAll();
        }
    }
}
