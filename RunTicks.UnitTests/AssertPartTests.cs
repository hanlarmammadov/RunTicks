using NUnit.Framework;
using System;
using System.Linq;

namespace RunTicks.UnitTests
{
    [TestFixture]
    public class AssertPartTests
    {
        private RunResult GetTestMeasureResult(Int64 ticks, Int64 numberOfRuns)
        {
            return new RunResult(ticks, numberOfRuns, DateTime.Now, DateTime.Now, "testMeasurement", "testTry");
        }

        [Test]
        public void Ctor_When_Called_Sets_MeasureResult()
        {
            // Arrange
            RunResult measureResult = GetTestMeasureResult(1, 1);

            // Act
            AssertPart assertPart = new AssertPart(measureResult);

            // Assert
            Assert.AreEqual(measureResult, assertPart.MeasureResult);
        }

        [Test]
        public void NotExceedsTicks_When_Called_Given_That_Result_Ticks_Arent_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange
            RunResult measureResult = GetTestMeasureResult(2, 1);
            AssertPart assertPart = new AssertPart(measureResult);

            // Act
            assertPart.NotExceedsTicks(3);
            assertPart.NotExceedsTicks(2);
        }
        [Test]
        public void NotExceedsTicks_When_Called_Given_That_Result_Ticks_Are_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange
            RunResult measureResult = GetTestMeasureResult(4, 1);
            AssertPart assertPart = new AssertPart(measureResult);

            Assert.Catch<AssertionException>(() =>
            {
                // Act
                assertPart.NotExceedsTicks(2, "some description");
            });
        }

        [Test]
        public void NotExceedsMilliseconds_When_Called_Given_That_Result_Milliseconds_Arent_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange 
            var resultTime = new TimeSpan(0, 0, 2);
            RunResult measureResult = GetTestMeasureResult(resultTime.Ticks, 1);
            AssertPart assertPart = new AssertPart(measureResult);

            // Act
            assertPart.NotExceedsMilliseconds(3000);
            assertPart.NotExceedsMilliseconds(2000);
        }
        [Test]
        public void NotExceedsMilliseconds_When_Called_Given_That_Result_Milliseconds_Are_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange
            var resultTime = new TimeSpan(0, 0, 2);
            RunResult measureResult = GetTestMeasureResult(resultTime.Ticks, 1);
            AssertPart assertPart = new AssertPart(measureResult);

            Assert.Catch<AssertionException>(() =>
            {
                // Act
                assertPart.NotExceedsMilliseconds(1000, "some description");
            });
        }

        [Test]
        public void NotExceedsSeconds_When_Called_Given_That_Result_Seconds_Arent_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange 
            var resultTime = new TimeSpan(0, 0, 2);
            RunResult measureResult = GetTestMeasureResult(resultTime.Ticks, 1);
            AssertPart assertPart = new AssertPart(measureResult);

            // Act
            assertPart.NotExceedsSeconds(3);
            assertPart.NotExceedsSeconds(2);
        }
        [Test]
        public void NotExceedsSeconds_When_Called_Given_That_Result_Seconds_Are_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange
            var resultTime = new TimeSpan(0, 0, 2);
            RunResult measureResult = GetTestMeasureResult(resultTime.Ticks, 1);
            AssertPart assertPart = new AssertPart(measureResult);

            Assert.Catch<AssertionException>(() =>
            {
                // Act
                assertPart.NotExceedsSeconds(1, "some description");
            });
        }

        [Test]
        public void NotExceedsMinutes_When_Called_Given_That_Result_Minutes_Arent_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange 
            var resultTime = new TimeSpan(0, 2, 0);
            RunResult measureResult = GetTestMeasureResult(resultTime.Ticks, 1);
            AssertPart assertPart = new AssertPart(measureResult);

            // Act
            assertPart.NotExceedsMinutes(3);
            assertPart.NotExceedsMinutes(2);
        }
        [Test]
        public void NotExceedsMinutes_When_Called_Given_That_Result_Minutes_Are_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange
            var resultTime = new TimeSpan(0, 2, 0);
            RunResult measureResult = GetTestMeasureResult(resultTime.Ticks, 1);
            AssertPart assertPart = new AssertPart(measureResult);

            Assert.Catch<AssertionException>(() =>
            {
                // Act
                assertPart.NotExceedsMinutes(1, "some description");
            });
        }

        [Test]
        public void NotExceedsTime_When_Called_Given_That_Result_Time_Arent_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange 
            var resultTime = new TimeSpan(0, 2, 0);
            RunResult measureResult = GetTestMeasureResult(resultTime.Ticks, 1);
            AssertPart assertPart = new AssertPart(measureResult);

            // Act
            assertPart.NotExceedsTime(new TimeSpan(0, 3, 0));
            assertPart.NotExceedsTime(new TimeSpan(0, 2, 0));
        }
        [Test]
        public void NotExceedsTime_When_Called_Given_That_Result_Time_Are_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange
            var resultTime = new TimeSpan(0, 2, 0);
            RunResult measureResult = GetTestMeasureResult(resultTime.Ticks, 1);
            AssertPart assertPart = new AssertPart(measureResult);

            Assert.Catch<AssertionException>(() =>
            {
                // Act
                assertPart.NotExceedsTime(new TimeSpan(0, 1, 0), "some description");
            });
        }

        [Test]
        public void NotExceedsAverageTicks_When_Called_Given_That_Result_Ticks_Arent_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange
            RunResult measureResult = GetTestMeasureResult(100, 20);
            AssertPart assertPart = new AssertPart(measureResult);

            // Act
            assertPart.NotExceedsAverageTicks(5);
            assertPart.NotExceedsAverageTicks(6);
        }
        [Test]
        public void NotExceedsAverageTicks_When_Called_Given_That_Result_Ticks_Are_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange
            RunResult measureResult = GetTestMeasureResult(100, 20);
            AssertPart assertPart = new AssertPart(measureResult);

            Assert.Catch<AssertionException>(() =>
            {
                // Act
                assertPart.NotExceedsAverageTicks(2, "some description");
            });
        }

        [Test]
        public void NotExceedsAverageMilliseconds_When_Called_Given_That_Result_Milliseconds_Arent_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange 
            var resultTime = new TimeSpan(0, 0, 60);
            RunResult measureResult = GetTestMeasureResult(resultTime.Ticks, 10);
            AssertPart assertPart = new AssertPart(measureResult);

            // Act
            assertPart.NotExceedsAverageMilliseconds(7000);
        }
        [Test]
        public void NotExceedsAverageMilliseconds_When_Called_Given_That_Result_Milliseconds_Are_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange
            var resultTime = new TimeSpan(0, 0, 60);
            RunResult measureResult = GetTestMeasureResult(resultTime.Ticks, 10);
            AssertPart assertPart = new AssertPart(measureResult);

            Assert.Catch<AssertionException>(() =>
            {
                // Act
                assertPart.NotExceedsAverageMilliseconds(5000, "some description");
            });
        }

        [Test]
        public void NotExceedsAverageSeconds_When_Called_Given_That_Result_Seconds_Arent_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange 
            var resultTime = new TimeSpan(0, 0, 60);
            RunResult measureResult = GetTestMeasureResult(resultTime.Ticks, 10);
            AssertPart assertPart = new AssertPart(measureResult);

            // Act
            assertPart.NotExceedsAverageSeconds(7);
        }
        [Test]
        public void NotExceedsAverageSeconds_When_Called_Given_That_Result_Seconds_Are_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange
            var resultTime = new TimeSpan(0, 0, 60);
            RunResult measureResult = GetTestMeasureResult(resultTime.Ticks, 10);
            AssertPart assertPart = new AssertPart(measureResult);

            Assert.Catch<AssertionException>(() =>
            {
                // Act
                assertPart.NotExceedsAverageSeconds(5, "some description");
            });
        }

        [Test]
        public void NotExceedsAverageMinutes_When_Called_Given_That_Result_Minutes_Arent_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange 
            var resultTime = new TimeSpan(0, 20, 0);
            RunResult measureResult = GetTestMeasureResult(resultTime.Ticks, 5);
            AssertPart assertPart = new AssertPart(measureResult);

            // Act
            assertPart.NotExceedsAverageMinutes(4);
            assertPart.NotExceedsAverageMinutes(5);
        }
        [Test]
        public void NotExceedsAverageMinutes_When_Called_Given_That_Result_Minutes_Are_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange
            var resultTime = new TimeSpan(0, 20, 0); 
            RunResult measureResult = GetTestMeasureResult(resultTime.Ticks, 5);
            AssertPart assertPart = new AssertPart(measureResult);

            Assert.Catch<AssertionException>(() =>
            {
                // Act
                assertPart.NotExceedsAverageMinutes(3, "some description");
            });
        }
    }
}
