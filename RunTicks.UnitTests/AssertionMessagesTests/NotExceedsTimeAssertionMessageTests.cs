using Moq;
using NUnit.Framework;
using RunTicks.AssertionMessages;
using System;
using System.Globalization;
using System.Text;

namespace RunTicks.UnitTests
{
    [TestFixture]
    public class NotExceedTimeAssertionMessageTests
    {
        [Test]
        public void Ctor_When_Called_Sets_Provided_Info_Correctly()
        {
            // Arrange
            TimeSpan expectedTimeSpan = new TimeSpan(5567);
            TimeSpan actualTimeSpan = new TimeSpan(674567);
            string descr = "some description";

            // Act
            NotExceedTimeAssertionMessage notExceedsTimeAssertionMessage = new NotExceedTimeAssertionMessage(expectedTimeSpan, actualTimeSpan, descr);

            // Assert
            Assert.AreEqual(expectedTimeSpan, notExceedsTimeAssertionMessage.ExpectedMaxTimespan);
            Assert.AreEqual(actualTimeSpan, notExceedsTimeAssertionMessage.ActualTimespan);
            Assert.AreEqual(descr, notExceedsTimeAssertionMessage.Description);
        }

        [Test]
        public void AddSpecificMessagePart_When_Called_Adds_Own_String_To_Provided_StringBuilder()
        {
            // Arrange
            TimeSpan expectedTimeSpan = new TimeSpan(5567);
            TimeSpan actualTimeSpan = new TimeSpan(674567);
            string descr = "some description";
            NotExceedTimeAssertionMessage notExceedsTimeAssertionMessage = new NotExceedTimeAssertionMessage(expectedTimeSpan, actualTimeSpan, descr);
            StringBuilder sb = new StringBuilder();

            // Act
            notExceedsTimeAssertionMessage.AddSpecificMessagePart(sb, CultureInfo.InvariantCulture);

            // Assert
            var resString = sb.ToString();
            var expectedString = $"Expected total time span not to exceed 00:00:00.0005567, but had: 00:00:00.0674567 total time span.{Environment.NewLine}";
            Assert.AreEqual(expectedString, resString);
        }

        //[Test]
        //public void AddSpecificMessagePart_When_Called_Uses_CultureInfo()
        //{
        //    // Arrange
        //    TimeSpan expectedTimeSpan = new TimeSpan(5567);
        //    TimeSpan actualTimeSpan = new TimeSpan(674567);
        //    NotExceedTimeAssertionMessage notExceedsTimeAssertionMessage = new NotExceedTimeAssertionMessage(expectedTimeSpan, actualTimeSpan, null);
        //    StringBuilder sb = new StringBuilder();
        //    var cultureInfoMock = new Mock<CultureInfo>("es-ES");

        //    // Act
        //    notExceedsTimeAssertionMessage.AddSpecificMessagePart(sb, cultureInfoMock.Object);

        //    // Assert
        //    cultureInfoMock.Verify(x => x.GetHashCode(), Times.Exactly(2));
        //}



    }
}
