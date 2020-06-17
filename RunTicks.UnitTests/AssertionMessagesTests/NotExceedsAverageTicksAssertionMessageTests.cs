using Moq;
using NUnit.Framework;
using RunTicks.AssertionMessages;
using System;
using System.Globalization;
using System.Text;

namespace RunTicks.UnitTests
{
    [TestFixture]
    public class NotExceedsAverageTicksAssertionMessageTests
    {
        [Test]
        public void Ctor_When_Called_Sets_Provided_Info_Correctly()
        {
            // Arrange
            Int64 expectedMaxAverageTicks = 5567;
            Int64 actualAverageTicks = 674567;
            string descr = "some description";

            // Act
            NotExceedsAverageTicksAssertionMessage notExceedsAverageTicksAssertionMessage = new NotExceedsAverageTicksAssertionMessage(expectedMaxAverageTicks, actualAverageTicks, descr);

            // Assert
            Assert.AreEqual(expectedMaxAverageTicks, notExceedsAverageTicksAssertionMessage.ExpectedMaxAverageTicks);
            Assert.AreEqual(actualAverageTicks, notExceedsAverageTicksAssertionMessage.ActualAverageTicks);
            Assert.AreEqual(descr, notExceedsAverageTicksAssertionMessage.Description);
        }
         
        [Test]
        public void AddSpecificMessagePart_When_Called_Adds_Own_String_To_Provided_StringBuilder()
        {
            // Arrange
            Int64 expectedMaxAverageTicks = 5567;
            Int64 actualAverageTicks = 674567;
            string descr = "some description"; 
            NotExceedsAverageTicksAssertionMessage notExceedsAverageTicksAssertionMessage = new NotExceedsAverageTicksAssertionMessage(expectedMaxAverageTicks, actualAverageTicks, descr);
            StringBuilder sb = new StringBuilder();

            // Act
            notExceedsAverageTicksAssertionMessage.AddSpecificMessagePart(sb, CultureInfo.InvariantCulture);

            // Assert
            var resString = sb.ToString();
            var expectedString = $"Expected average (per run) ticks not to exceed 5567, but had: 674567 average ticks.{Environment.NewLine}";
            Assert.AreEqual(expectedString, resString);
        }
    }
}
