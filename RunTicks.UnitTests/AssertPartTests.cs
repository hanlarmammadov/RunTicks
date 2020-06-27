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
        public void NotExceedTicks_When_Called_Given_That_Result_Ticks_Arent_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange
            RunResult measureResult = GetTestMeasureResult(2, 1);
            AssertPart assertPart = new AssertPart(measureResult);

            // Act
            assertPart.NotExceedTotalTicks(3);
            assertPart.NotExceedTotalTicks(2);
        }
        [Test]
        public void NotExceedTicks_When_Called_Given_That_Result_Ticks_Are_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange
            RunResult measureResult = GetTestMeasureResult(4, 1);
            AssertPart assertPart = new AssertPart(measureResult);

            Assert.Catch<AssertionException>(() =>
            {
                // Act
                assertPart.NotExceedTotalTicks(2, "some description");
            });
        }

        [Test]
        public void NotExceedMilliseconds_When_Called_Given_That_Result_Milliseconds_Arent_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange 
            var resultTime = new TimeSpan(0, 0, 2);
            RunResult measureResult = GetTestMeasureResult(resultTime.Ticks, 1);
            AssertPart assertPart = new AssertPart(measureResult);

            // Act
            assertPart.NotExceedTotalMilliseconds(3000);
            assertPart.NotExceedTotalMilliseconds(2000);
        }
        [Test]
        public void NotExceedMilliseconds_When_Called_Given_That_Result_Milliseconds_Are_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange
            var resultTime = new TimeSpan(0, 0, 2);
            RunResult measureResult = GetTestMeasureResult(resultTime.Ticks, 1);
            AssertPart assertPart = new AssertPart(measureResult);

            Assert.Catch<AssertionException>(() =>
            {
                // Act
                assertPart.NotExceedTotalMilliseconds(1000, "some description");
            });
        }

        [Test]
        public void NotExceedSeconds_When_Called_Given_That_Result_Seconds_Arent_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange 
            var resultTime = new TimeSpan(0, 0, 2);
            RunResult measureResult = GetTestMeasureResult(resultTime.Ticks, 1);
            AssertPart assertPart = new AssertPart(measureResult);

            // Act
            assertPart.NotExceedTotalSeconds(3);
            assertPart.NotExceedTotalSeconds(2);
        }
        [Test]
        public void NotExceedSeconds_When_Called_Given_That_Result_Seconds_Are_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange
            var resultTime = new TimeSpan(0, 0, 2);
            RunResult measureResult = GetTestMeasureResult(resultTime.Ticks, 1);
            AssertPart assertPart = new AssertPart(measureResult);

            Assert.Catch<AssertionException>(() =>
            {
                // Act
                assertPart.NotExceedTotalSeconds(1, "some description");
            });
        }

        [Test]
        public void NotExceedMinutes_When_Called_Given_That_Result_Minutes_Arent_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange 
            var resultTime = new TimeSpan(0, 2, 0);
            RunResult measureResult = GetTestMeasureResult(resultTime.Ticks, 1);
            AssertPart assertPart = new AssertPart(measureResult);

            // Act
            assertPart.NotExceedTotalMinutes(3);
            assertPart.NotExceedTotalMinutes(2);
        }
        [Test]
        public void NotExceedMinutes_When_Called_Given_That_Result_Minutes_Are_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange
            var resultTime = new TimeSpan(0, 2, 0);
            RunResult measureResult = GetTestMeasureResult(resultTime.Ticks, 1);
            AssertPart assertPart = new AssertPart(measureResult);

            Assert.Catch<AssertionException>(() =>
            {
                // Act
                assertPart.NotExceedTotalMinutes(1, "some description");
            });
        }

        [Test]
        public void NotExceedTime_When_Called_Given_That_Result_Time_Arent_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange 
            var resultTime = new TimeSpan(0, 2, 0);
            RunResult measureResult = GetTestMeasureResult(resultTime.Ticks, 1);
            AssertPart assertPart = new AssertPart(measureResult);

            // Act
            assertPart.NotExceedTotalTime(new TimeSpan(0, 3, 0));
            assertPart.NotExceedTotalTime(new TimeSpan(0, 2, 0));
        }
        [Test]
        public void NotExceedTime_When_Called_Given_That_Result_Time_Are_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange
            var resultTime = new TimeSpan(0, 2, 0);
            RunResult measureResult = GetTestMeasureResult(resultTime.Ticks, 1);
            AssertPart assertPart = new AssertPart(measureResult);

            Assert.Catch<AssertionException>(() =>
            {
                // Act
                assertPart.NotExceedTotalTime(new TimeSpan(0, 1, 0), "some description");
            });
        }

        [Test]
        public void NotExceedAverageTicks_When_Called_Given_That_Result_Ticks_Arent_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange
            RunResult measureResult = GetTestMeasureResult(100, 20);
            AssertPart assertPart = new AssertPart(measureResult);

            // Act
            assertPart.NotExceedAverageTicks(5);
            assertPart.NotExceedAverageTicks(6);
        }
        [Test]
        public void NotExceedAverageTicks_When_Called_Given_That_Result_Ticks_Are_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange
            RunResult measureResult = GetTestMeasureResult(100, 20);
            AssertPart assertPart = new AssertPart(measureResult);

            Assert.Catch<AssertionException>(() =>
            {
                // Act
                assertPart.NotExceedAverageTicks(2, "some description");
            });
        }

        [Test]
        public void NotExceedAverageMilliseconds_When_Called_Given_That_Result_Milliseconds_Arent_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange 
            var resultTime = new TimeSpan(0, 0, 60);
            RunResult measureResult = GetTestMeasureResult(resultTime.Ticks, 10);
            AssertPart assertPart = new AssertPart(measureResult);

            // Act
            assertPart.NotExceedAverageMilliseconds(7000);
        }
        [Test]
        public void NotExceedAverageMilliseconds_When_Called_Given_That_Result_Milliseconds_Are_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange
            var resultTime = new TimeSpan(0, 0, 60);
            RunResult measureResult = GetTestMeasureResult(resultTime.Ticks, 10);
            AssertPart assertPart = new AssertPart(measureResult);

            Assert.Catch<AssertionException>(() =>
            {
                // Act
                assertPart.NotExceedAverageMilliseconds(5000, "some description");
            });
        }

        [Test]
        public void NotExceedAverageSeconds_When_Called_Given_That_Result_Seconds_Arent_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange 
            var resultTime = new TimeSpan(0, 0, 60);
            RunResult measureResult = GetTestMeasureResult(resultTime.Ticks, 10);
            AssertPart assertPart = new AssertPart(measureResult);

            // Act
            assertPart.NotExceedAverageSeconds(7);
        }
        [Test]
        public void NotExceedAverageSeconds_When_Called_Given_That_Result_Seconds_Are_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange
            var resultTime = new TimeSpan(0, 0, 60);
            RunResult measureResult = GetTestMeasureResult(resultTime.Ticks, 10);
            AssertPart assertPart = new AssertPart(measureResult);

            Assert.Catch<AssertionException>(() =>
            {
                // Act
                assertPart.NotExceedAverageSeconds(5, "some description");
            });
        }

        [Test]
        public void NotExceedAverageMinutes_When_Called_Given_That_Result_Minutes_Arent_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange 
            var resultTime = new TimeSpan(0, 20, 0);
            RunResult measureResult = GetTestMeasureResult(resultTime.Ticks, 5);
            AssertPart assertPart = new AssertPart(measureResult);

            // Act
            assertPart.NotExceedAverageMinutes(4);
            assertPart.NotExceedAverageMinutes(5);
        }
        [Test]
        public void NotExceedAverageMinutes_When_Called_Given_That_Result_Minutes_Are_More_That_Provided_Max_Does_Not_Throw()
        {
            // Arrange
            var resultTime = new TimeSpan(0, 20, 0); 
            RunResult measureResult = GetTestMeasureResult(resultTime.Ticks, 5);
            AssertPart assertPart = new AssertPart(measureResult);

            Assert.Catch<AssertionException>(() =>
            {
                // Act
                assertPart.NotExceedAverageMinutes(3, "some description");
            });
        }
    }
}
