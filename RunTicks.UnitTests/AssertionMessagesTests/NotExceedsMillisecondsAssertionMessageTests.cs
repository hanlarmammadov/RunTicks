using Moq;
using NUnit.Framework;
using RunTicks.AssertionMessages;
using System;
using System.Globalization;
using System.Text;

namespace RunTicks.UnitTests
{
    [TestFixture]
    public class NotExceedMillisecondsAssertionMessageTests
    {
        [Test]
        public void Ctor_When_Called_Sets_Provided_Info_Correctly()
        {
            // Arrange
            Int64 expectedMaxMilliseconds = 342;
            Double actualMilliseconds = 22112;
            string descr = "some description";

            // Act
            NotExceedMillisecondsAssertionMessage notExceedsMillisecondsAssertionMessage = new NotExceedMillisecondsAssertionMessage(expectedMaxMilliseconds, actualMilliseconds, descr);

            // Assert
            Assert.AreEqual(expectedMaxMilliseconds, notExceedsMillisecondsAssertionMessage.ExpectedMaxMilliseconds);
            Assert.AreEqual(actualMilliseconds, notExceedsMillisecondsAssertionMessage.ActualMilliseconds);
            Assert.AreEqual(descr, notExceedsMillisecondsAssertionMessage.Description);
        }
        [Test]
        public void AddSpecificMessagePart_When_Called_Adds_Own_String_To_Provided_StringBuilder()
        {
            // Arrange 
            Int64 expectedMaxMilliseconds = 342;
            Double actualMilliseconds = 22112;
            string descr = "some description";
            NotExceedMillisecondsAssertionMessage notExceedsMillisecondsAssertionMessage = new NotExceedMillisecondsAssertionMessage(expectedMaxMilliseconds, actualMilliseconds, descr);
            StringBuilder sb = new StringBuilder();

            // Act
            notExceedsMillisecondsAssertionMessage.AddSpecificMessagePart(sb, CultureInfo.InvariantCulture);

            // Assert
            var resString = sb.ToString();
            var expectedString = $"Expected total milliseconds not to exceed 342, but had: 22112 total milliseconds.{Environment.NewLine}";
            Assert.AreEqual(expectedString, resString);
        }
        [Test]
        public void AddSpecificMessagePart_When_Called_Uses_CultureInfo()
        {
            // Arrange
            Int64 expectedMaxMilliseconds = 342;
            Double actualMilliseconds = 22112;
            string descr = "some description";
            NotExceedMillisecondsAssertionMessage notExceedsMillisecondsAssertionMessage = new NotExceedMillisecondsAssertionMessage(expectedMaxMilliseconds, actualMilliseconds, descr);
            StringBuilder sb = new StringBuilder();
            var cultureInfoMock = new Mock<CultureInfo>("es-ES");

            // Act
            notExceedsMillisecondsAssertionMessage.AddSpecificMessagePart(sb, cultureInfoMock.Object);

            // Assert
            cultureInfoMock.VerifyAll();
        }
    }
}
