using NUnit.Framework;
using RunTicks.MeasurementStrategies;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RunTicks.UnitTests.MeasurementStrategiesTests
{
    [TestFixture]
    public class AsyncManualStopwatchStrategyTests
    {
        [Test]
        public void ExecuteAction_When_Called_Calls_Action()
        {
            // Arrange
            int actionCallTimes = 0;
            AsyncActionWithStopwatch action = async (sw) => 
            {
                sw.Start();
                actionCallTimes++;
                await Task.CompletedTask; 
            };
            AsyncManualStopwatchStrategy measurementStrategy = new AsyncManualStopwatchStrategy(action);
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
            var exception = new InvalidOperationException();
            AsyncActionWithStopwatch action = async (sw) => 
            {
                sw.Start();
                actionCallTimes++; 
                await Task.FromException<Exception>(exception);
            };
            AsyncManualStopwatchStrategy measurementStrategy = new AsyncManualStopwatchStrategy(action);
            Stopwatch stopwatch = new Stopwatch();


            // Assert
            var aex = Assert.Catch<AggregateException>(() =>
            {
                // Act
                measurementStrategy.ExecuteAction(stopwatch);
            });
            Assert.AreEqual(exception, aex.InnerException);
            Assert.AreEqual(1, actionCallTimes);
            Assert.AreNotEqual(0, stopwatch.ElapsedTicks);
            Assert.IsFalse(stopwatch.IsRunning);
        }
    }
}
