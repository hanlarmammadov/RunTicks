using Moq;
using NUnit.Framework;
using RunTicks.AssertionMessages;
using System;
using System.Globalization;
using System.Text;

namespace RunTicks.UnitTests
{
    [TestFixture]
    public class NotExceedsAverageMinutesAssertionMessageTests
    {
        [Test]
        public void Ctor_When_Called_Sets_Provided_Info_Correctly()
        {
            // Arrange
            Int64 expectedAverageMinutes = 1;
            Double actualAverageMinutes = 2;
            string descr = "some description";

            // Act
            NotExceedsAverageMinutesAssertionMessage notExceedsAverageMinutesAssertionMessage = new NotExceedsAverageMinutesAssertionMessage(expectedAverageMinutes, actualAverageMinutes, descr);

            // Assert
            Assert.AreEqual(expectedAverageMinutes, notExceedsAverageMinutesAssertionMessage.ExpectedMaxAverageMinutes);
            Assert.AreEqual(actualAverageMinutes, notExceedsAverageMinutesAssertionMessage.ActualAverageMinutes);
            Assert.AreEqual(descr, notExceedsAverageMinutesAssertionMessage.Description);
        }
        [Test]
        public void AddSpecificMessagePart_When_Called_Adds_Own_String_To_Provided_StringBuilder()
        {
            // Arrange 
            Int64 expectedAverageMinutes = 1;
            Double actualAverageMinutes = 2;
            string descr = "some description";
            NotExceedsAverageMinutesAssertionMessage notExceedsAverageMinutesAssertionMessage = new NotExceedsAverageMinutesAssertionMessage(expectedAverageMinutes, actualAverageMinutes, descr);
            StringBuilder sb = new StringBuilder();

            // Act
            notExceedsAverageMinutesAssertionMessage.AddSpecificMessagePart(sb, CultureInfo.InvariantCulture);

            // Assert
            var resString = sb.ToString();
            var expectedString = $"Expected average (per run) minutes not to exceed 1, but had: 2 average minutes.{Environment.NewLine}";
            Assert.AreEqual(expectedString, resString);
        }
        [Test]
        public void AddSpecificMessagePart_When_Called_Uses_CultureInfo()
        {
            // Arrange
            Int64 expectedAverageMinutes = 1;
            Double actualAverageMinutes = 2;
            string descr = "some description";
            NotExceedsAverageMinutesAssertionMessage notExceedsAverageMinutesAssertionMessage = new NotExceedsAverageMinutesAssertionMessage(expectedAverageMinutes, actualAverageMinutes, descr);
            StringBuilder sb = new StringBuilder();
            var cultureInfoMock = new Mock<CultureInfo>("es-ES");

            // Act
            notExceedsAverageMinutesAssertionMessage.AddSpecificMessagePart(sb, cultureInfoMock.Object);

            // Assert
            cultureInfoMock.VerifyAll();
        }
    }
}
