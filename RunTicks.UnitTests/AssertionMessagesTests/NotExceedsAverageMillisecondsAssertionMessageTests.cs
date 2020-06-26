using Moq;
using NUnit.Framework;
using RunTicks.AssertionMessages;
using System;
using System.Globalization;
using System.Text;

namespace RunTicks.UnitTests
{
    [TestFixture]
    public class NotExceedAverageMillisecondsAssertionMessageTests
    {
        [Test]
        public void Ctor_When_Called_Sets_Provided_Info_Correctly()
        {
            // Arrange
            Int64 expectedMaxAverageMilliseconds = 342;
            Double actualAverageMilliseconds = 22112;
            string descr = "some description";

            // Act
            NotExceedAverageMillisecondsAssertionMessage notExceedsAverageMillisecondsAssertionMessage = new NotExceedAverageMillisecondsAssertionMessage(expectedMaxAverageMilliseconds, actualAverageMilliseconds, descr);

            // Assert
            Assert.AreEqual(expectedMaxAverageMilliseconds, notExceedsAverageMillisecondsAssertionMessage.ExpectedMaxAverageMilliseconds);
            Assert.AreEqual(actualAverageMilliseconds, notExceedsAverageMillisecondsAssertionMessage.ActualAverageMilliseconds);
            Assert.AreEqual(descr, notExceedsAverageMillisecondsAssertionMessage.Description);
        }
        [Test]
        public void AddSpecificMessagePart_When_Called_Adds_Own_String_To_Provided_StringBuilder()
        {
            // Arrange 
            Int64 expectedMaxAverageMilliseconds = 342;
            Double actualAverageMilliseconds = 22112;
            string descr = "some description"; 
            NotExceedAverageMillisecondsAssertionMessage notExceedsAverageMillisecondsAssertionMessage = new NotExceedAverageMillisecondsAssertionMessage(expectedMaxAverageMilliseconds, actualAverageMilliseconds, descr);
            StringBuilder sb = new StringBuilder();

            // Act
            notExceedsAverageMillisecondsAssertionMessage.AddSpecificMessagePart(sb, CultureInfo.InvariantCulture);

            // Assert
            var resString = sb.ToString();
            var expectedString = $"Expected average (per run) milliseconds not to exceed 342, but had: 22112 average milliseconds.{Environment.NewLine}";
            Assert.AreEqual(expectedString, resString);
        }
        [Test]
        public void AddSpecificMessagePart_When_Called_Uses_CultureInfo()
        {
            // Arrange
            Int64 expectedMaxAverageMilliseconds = 342;
            Double actualAverageMilliseconds = 22112;
            string descr = "some description";
            NotExceedAverageMillisecondsAssertionMessage notExceedsAverageMillisecondsAssertionMessage = new NotExceedAverageMillisecondsAssertionMessage(expectedMaxAverageMilliseconds, actualAverageMilliseconds, descr);
            StringBuilder sb = new StringBuilder();
            var cultureInfoMock = new Mock<CultureInfo>("es-ES");

            // Act
            notExceedsAverageMillisecondsAssertionMessage.AddSpecificMessagePart(sb, cultureInfoMock.Object);

            // Assert
            cultureInfoMock.VerifyAll();
        }
    }
}
