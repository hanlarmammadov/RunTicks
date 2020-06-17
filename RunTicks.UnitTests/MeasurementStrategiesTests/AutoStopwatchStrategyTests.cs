using NUnit.Framework;
using RunTicks.MeasurementStrategies;
using System;
using System.Diagnostics;

namespace RunTicks.UnitTests.MeasurementStrategiesTests
{
    [TestFixture]
    public class AutoStopwatchStrategyTests
    {
        [Test]
        public void ExecuteAction_When_Called_Calls_Action()
        {
            // Arrange
            int actionCallTimes = 0;
            Action action = () => { actionCallTimes++; };
            AutoStopwatchStrategy measurementStrategy = new AutoStopwatchStrategy(action);
            Stopwatch stopwatch = new Stopwatch();

            // Act
            measurementStrategy.ExecuteAction(stopwatch);

            // Assert
            Assert.AreEqual(1, actionCallTimes);
            Assert.AreNotEqual(0, stopwatch.ElapsedTicks);
            Assert.IsFalse(stopwatch.IsRunning);
        }
        [Test]
        public void ExecuteAction_When_Called_Given_That_Action_Throws_Does_Not_Swallow_Exception()
        {
            // Arrange
            int actionCallTimes = 0;
            Action action = () => {
                actionCallTimes++;
                throw new InvalidOperationException();
            };
            AutoStopwatchStrategy measurementStrategy = new AutoStopwatchStrategy(action);
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
