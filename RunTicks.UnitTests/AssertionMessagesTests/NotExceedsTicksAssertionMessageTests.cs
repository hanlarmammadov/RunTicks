using Moq;
using NUnit.Framework;
using RunTicks.AssertionMessages;
using System;
using System.Globalization;
using System.Text;

namespace RunTicks.UnitTests
{
    [TestFixture]
    public class NotExceedTicksAssertionMessageTests
    {
        [Test]
        public void Ctor_When_Called_Sets_Provided_Info_Correctly()
        {
            // Arrange
            Int64 expectedMaxTicks = 5567;
            Int64 actualTicks = 674567;
            string descr = "some description";

            // Act
            NotExceedTicksAssertionMessage notExceedsTicksAssertionMessage = new NotExceedTicksAssertionMessage(expectedMaxTicks, actualTicks, descr);

            // Assert
            Assert.AreEqual(expectedMaxTicks, notExceedsTicksAssertionMessage.ExpectedMaxTicks);
            Assert.AreEqual(actualTicks, notExceedsTicksAssertionMessage.ActualTicks);
            Assert.AreEqual(descr, notExceedsTicksAssertionMessage.Description);
        }
         
        [Test]
        public void AddSpecificMessagePart_When_Called_Adds_Own_String_To_Provided_StringBuilder()
        {
            // Arrange
            Int64 expectedMaxTicks = 5567;
            Int64 actualTicks = 674567;
            string descr = "some description";
            NotExceedTicksAssertionMessage notExceedsTicksAssertionMessage = new NotExceedTicksAssertionMessage(expectedMaxTicks, actualTicks, descr);
            StringBuilder sb = new StringBuilder();

            // Act
            notExceedsTicksAssertionMessage.AddSpecificMessagePart(sb, CultureInfo.InvariantCulture);

            // Assert
            var resString = sb.ToString();
            var expectedString = $"Expected total ticks not to exceed 5567, but had: 674567 total ticks.{Environment.NewLine}";
            Assert.AreEqual(expectedString, resString);
        }
    }
}
