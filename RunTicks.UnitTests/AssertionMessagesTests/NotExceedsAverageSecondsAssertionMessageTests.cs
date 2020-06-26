using Moq;
using NUnit.Framework;
using RunTicks.AssertionMessages;
using System;
using System.Globalization;
using System.Text;

namespace RunTicks.UnitTests
{
    [TestFixture]
    public class NotExceedAverageSecondsAssertionMessageTests
    {
        [Test]
        public void Ctor_When_Called_Sets_Provided_Info_Correctly()
        {
            // Arrange
            Int64 expectedAverageSeconds = 2;
            Double actualAverageSeconds = 5;
            string descr = "some description";

            // Act
            NotExceedAverageSecondsAssertionMessage notExceedsAverageSecondsAssertionMessage = new NotExceedAverageSecondsAssertionMessage(expectedAverageSeconds, actualAverageSeconds, descr);

            // Assert
            Assert.AreEqual(expectedAverageSeconds, notExceedsAverageSecondsAssertionMessage.ExpectedMaxAverageSeconds);
            Assert.AreEqual(actualAverageSeconds, notExceedsAverageSecondsAssertionMessage.ActualAverageSeconds);
            Assert.AreEqual(descr, notExceedsAverageSecondsAssertionMessage.Description);
        }
        [Test]
        public void AddSpecificMessagePart_When_Called_Adds_Own_String_To_Provided_StringBuilder()
        {
            // Arrange 
            Int64 expectedAverageSeconds = 2;
            Double actualAverageSeconds = 5;
            string descr = "some description";
            NotExceedAverageSecondsAssertionMessage notExceedsAverageSecondsAssertionMessage = new NotExceedAverageSecondsAssertionMessage(expectedAverageSeconds, actualAverageSeconds, descr);
            StringBuilder sb = new StringBuilder();

            // Act
            notExceedsAverageSecondsAssertionMessage.AddSpecificMessagePart(sb, CultureInfo.InvariantCulture);

            // Assert
            var resString = sb.ToString();
            var expectedString = $"Expected average (per run) seconds not to exceed 2, but had: 5 average seconds.{Environment.NewLine}";
            Assert.AreEqual(expectedString, resString);
        }
        [Test]
        public void AddSpecificMessagePart_When_Called_Uses_CultureInfo()
        {
            // Arrange
            Int64 expectedAverageSeconds = 2;
            Double actualAverageSeconds = 5;
            string descr = "some description";
            NotExceedAverageSecondsAssertionMessage notExceedsAverageSecondsAssertionMessage = new NotExceedAverageSecondsAssertionMessage(expectedAverageSeconds, actualAverageSeconds, descr);
            StringBuilder sb = new StringBuilder();
            var cultureInfoMock = new Mock<CultureInfo>("es-ES");

            // Act
            notExceedsAverageSecondsAssertionMessage.AddSpecificMessagePart(sb, cultureInfoMock.Object);

            // Assert
            cultureInfoMock.VerifyAll();
        }
    }
}
