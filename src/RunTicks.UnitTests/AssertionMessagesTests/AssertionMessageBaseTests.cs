using Moq;
using NUnit.Framework;
using RunTicks.AssertionMessages;
using System;
using System.Globalization;
using System.Text;

namespace RunTicks.UnitTests
{
    [TestFixture]
    public class AssertionMessageBaseTests
    {
        private class TestAssertionMessageSubclass : AssertionMessageBase
        {
            private readonly string _stringToAdd;
            private CultureInfo _cultureInfoReceivedInMethod;

            public TestAssertionMessageSubclass(string description, string stringToAdd)
                : base(description)
            {
                _stringToAdd = stringToAdd;
            }

            internal override void AddSpecificMessagePart(StringBuilder stringBuilder, CultureInfo culture)
            {
                stringBuilder.AppendLine(_stringToAdd);
                _cultureInfoReceivedInMethod = culture;
            }

            public CultureInfo CultureInfoReceivedInMethod => _cultureInfoReceivedInMethod;
        }

        [Test]
        public void Ctor_When_Called_Sets_Description()
        {
            // Arrange
            string descr = "some description";
            // Act
            TestAssertionMessageSubclass testAssertionMessageSubclass = new TestAssertionMessageSubclass(descr, null);

            // Assert
            Assert.AreEqual(descr, testAssertionMessageSubclass.Description);
        }
        [Test]
        public void Ctor_When_Called_With_Null_Description_Sets_Null_Description()
        {
            // Act
            TestAssertionMessageSubclass testAssertionMessageSubclass = new TestAssertionMessageSubclass(null, null);

            // Assert
            Assert.IsNull(testAssertionMessageSubclass.Description);
        }

        [Test]
        public void Generate_When_Called_Creates_Resulted_Message_Correctly()
        {
            // Arrange 
            var descr = "some description";
            var stringToAdd = $"String line1{Environment.NewLine}String line2";
            TestAssertionMessageSubclass testAssertionMessageSubclass = new TestAssertionMessageSubclass(descr, stringToAdd);

            // Act 
            var resString = testAssertionMessageSubclass.Generate(CultureInfo.InvariantCulture);

            // Assert 
            var expectedString = @$"
--------
Assertion failed.
{stringToAdd}
{descr}
--------";

            Assert.AreEqual(expectedString, resString);
        }
        [Test]
        public void Generate_When_Called_Given_That_Description_Is_Null_Creates_Resulted_Message_Correctly()
        {
            // Arrange  
            var stringToAdd = $"String line1";
            TestAssertionMessageSubclass testAssertionMessageSubclass = new TestAssertionMessageSubclass(null as string, stringToAdd);

            // Act 
            var resString = testAssertionMessageSubclass.Generate(CultureInfo.InvariantCulture);

            // Assert 
            var expectedString = @$"
--------
Assertion failed.
{stringToAdd}
--------";

            Assert.AreEqual(expectedString, resString);
        }
        [Test]
        public void Generate_When_Called_Sends_CultureInfo_To_AddSpecificMessagePart_Method()
        {
            // Arrange 
            TestAssertionMessageSubclass testAssertionMessageSubclass = new TestAssertionMessageSubclass(null, null);
            var cultureInfo = new Mock<CultureInfo>("en-ZW").Object;

            // Act 
            var resString = testAssertionMessageSubclass.Generate(cultureInfo);

            // Assert
            Assert.AreEqual(cultureInfo, testAssertionMessageSubclass.CultureInfoReceivedInMethod);
        }
    }
}
