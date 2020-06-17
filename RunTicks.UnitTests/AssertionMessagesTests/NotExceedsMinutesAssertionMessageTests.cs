using Moq;
using NUnit.Framework;
using RunTicks.AssertionMessages;
using System;
using System.Globalization;
using System.Text;

namespace RunTicks.UnitTests
{
    [TestFixture]
    public class NotExceedsMinutesAssertionMessageTests
    {
        [Test]
        public void Ctor_When_Called_Sets_Provided_Info_Correctly()
        {
            // Arrange
            Int64 expectedMinutes = 1;
            Double actualMinutes = 2;
            string descr = "some description";

            // Act
            NotExceedsMinutesAssertionMessage notExceedsMinutesAssertionMessage = new NotExceedsMinutesAssertionMessage(expectedMinutes, actualMinutes, descr);

            // Assert
            Assert.AreEqual(expectedMinutes, notExceedsMinutesAssertionMessage.ExpectedMaxMinutes);
            Assert.AreEqual(actualMinutes, notExceedsMinutesAssertionMessage.ActualMinutes);
            Assert.AreEqual(descr, notExceedsMinutesAssertionMessage.Description);
        }
        [Test]
        public void AddSpecificMessagePart_When_Called_Adds_Own_String_To_Provided_StringBuilder()
        {
            // Arrange 
            Int64 expectedMinutes = 1;
            Double actualMinutes = 2;
            string descr = "some description";
            NotExceedsMinutesAssertionMessage notExceedsMinutesAssertionMessage = new NotExceedsMinutesAssertionMessage(expectedMinutes, actualMinutes, descr);
            StringBuilder sb = new StringBuilder();

            // Act
            notExceedsMinutesAssertionMessage.AddSpecificMessagePart(sb, CultureInfo.InvariantCulture);

            // Assert
            var resString = sb.ToString();
            var expectedString = $"Expected total minutes not to exceed 1, but had: 2 total minutes.{Environment.NewLine}";
            Assert.AreEqual(expectedString, resString);
        }
        [Test]
        public void AddSpecificMessagePart_When_Called_Uses_CultureInfo()
        {
            // Arrange
            Int64 expectedMinutes = 1;
            Double actualMinutes = 2;
            string descr = "some description";
            NotExceedsMinutesAssertionMessage notExceedsMinutesAssertionMessage = new NotExceedsMinutesAssertionMessage(expectedMinutes, actualMinutes, descr);
            StringBuilder sb = new StringBuilder();
            var cultureInfoMock = new Mock<CultureInfo>("es-ES");

            // Act
            notExceedsMinutesAssertionMessage.AddSpecificMessagePart(sb, cultureInfoMock.Object);

            // Assert
            cultureInfoMock.VerifyAll();
        }
    }
}
