using NUnit.Framework;
using RunTicks.MeasurementStrategies;
using System;
using System.Diagnostics;

namespace RunTicks.UnitTests.MeasurementStrategiesTests
{
    [TestFixture]
    public class ManualStopwatchStrategyTests
    {
        [Test]
        public void ExecuteAction_When_Called_Calls_Action()
        {
            // Arrange
            int actionCallTimes = 0;
            Stopwatch stopwatchSentToAction = null;
            Action<Stopwatch> action = (sw) =>
            {
                actionCallTimes++;
                stopwatchSentToAction = sw;
            };
            ManualStopwatchStrategy measurementStrategy = new ManualStopwatchStrategy(action);
            Stopwatch stopwatch = new Stopwatch();

            // Act
            measurementStrategy.ExecuteAction(stopwatch);

            // Assert
            Assert.AreEqual(1, actionCallTimes);
            Assert.AreEqual(stopwatch, stopwatchSentToAction);
            Assert.AreEqual(0, stopwatch.ElapsedTicks);
            Assert.IsFalse(stopwatch.IsRunning);
        }
        [Test]
        public void ExecuteAction_When_Called_Given_That_Action_Throws_Does_Not_Swallow_Exception()
        {
            // Arrange
            int actionCallTimes = 0;
            Action<Stopwatch> action = (sw) =>
            {
                sw.Start();
                actionCallTimes++;
                throw new InvalidOperationException();
            };
            ManualStopwatchStrategy measurementStrategy = new ManualStopwatchStrategy(action);
            Stopwatch stopwatch = new Stopwatch();

            Assert.Catch<InvalidOperationException>(() =>
            {
                // Act
                measurementStrategy.ExecuteAction(stopwatch);
            });

            // Assert
            Assert.AreEqual(1, actionCallTimes);
            Assert.AreNotEqual(0, stopwatch.ElapsedTicks);
            Assert.IsFalse(stopwatch.IsRunning);
        }
    }
}
