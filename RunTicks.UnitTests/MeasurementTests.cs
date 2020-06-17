using NUnit.Framework;
using RunTicks.MeasurementStrategies;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RunTicks.UnitTests
{
    [TestFixture]
    public class MeasurementTests
    {
        [Test]
        public void Create_Taking_Name_When_Called_Creates_New_Measurement_And_Sets_Its_Name()
        {
            // Arrange
            string name = "Some name";

            // Act
            Measurement measurement = Measurement.Create(name);

            // Assert
            Assert.AreEqual(name, measurement.Name);
        }
        [Test]
        public void Create_Taking_Name_When_Called_Creates_New_Measurement_And_Sets_Properties_To_Null()
        {
            // Act
            Measurement measurement = Measurement.Create("Some name");

            // Assert
            Assert.IsNull(measurement.MeasurementStrategy);
            Assert.IsNull(measurement.AdditionalData);
        }
        [Test]
        public void Create_Taking_Name_When_Called_With_Null_Creates_New_Measurement_And_Sets_Its_Name_To_Null()
        {
            // Act
            Measurement measurement = Measurement.Create(null);

            // Assert
            Assert.IsNull(measurement.Name);
        }

        [Test]
        public void Create_When_Called_Creates_New_Measurement_And_Sets_Its_Name_To_Null()
        {
            // Act
            Measurement measurement = Measurement.Create();

            // Assert
            Assert.IsNull(measurement.Name);
        }
        [Test]
        public void Create_When_Called_Creates_New_Measurement_And_Sets_Properties_To_Null()
        {
            // Act
            Measurement measurement = Measurement.Create();

            // Assert
            Assert.IsNull(measurement.MeasurementStrategy);
            Assert.IsNull(measurement.AdditionalData);
        }

        [Test]
        public void OfAction_Taking_Action_When_Called_Sets_Appropriate_Stopwatch_Strategy()
        {
            // Arrange
            Measurement measurement = Measurement.Create();
            Action action = () => { };

            // Act
            measurement.OfAction(action);

            // Assert
            Assert.IsInstanceOf<AutoStopwatchStrategy>(measurement.MeasurementStrategy);
            var strategy = (AutoStopwatchStrategy)measurement.MeasurementStrategy;
            Assert.AreEqual(action, strategy.Action);
        }
        [Test]
        public void OfAction_Taking_Action_When_Called_With_Null_Throws_ArgumentNullException()
        {
            // Arrange
            Measurement measurement = Measurement.Create();

            // Assert
            Assert.Catch<ArgumentNullException>(() =>
            {
                // Act
                measurement.OfAction(null as Action);
            });
        }
        [Test]
        public void OfAction_Taking_Action_When_Called_Does_Not_Calls_The_Action()
        {
            // Arrange
            Measurement measurement = Measurement.Create();
            bool wasCalled = false;
            Action action = () => { wasCalled = true; };

            // Act
            measurement.OfAction(action);

            // Assert
            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void OfAction_Taking_ActionWithSW_When_Called_Sets_Appropriate_Stopwatch_Strategy()
        {
            // Arrange
            Measurement measurement = Measurement.Create();
            Action<Stopwatch> action = (sw) => { };

            // Act
            measurement.OfAction(action);

            // Assert
            Assert.IsInstanceOf<ManualStopwatchStrategy>(measurement.MeasurementStrategy);
            var strategy = (ManualStopwatchStrategy)measurement.MeasurementStrategy;
            Assert.AreEqual(action, strategy.Action);
        }
        [Test]
        public void OfAction_Taking_ActionWithSW_When_Called_With_Null_Throws_ArgumentNullException()
        {
            // Arrange
            Measurement measurement = Measurement.Create();

            // Assert
            Assert.Catch<ArgumentNullException>(() =>
            {
                // Act
                measurement.OfAction(null as Action<Stopwatch>);
            });
        }
        [Test]
        public void OfAction_Taking_ActionWithSW_When_Called_Does_Not_Calls_The_Action()
        {
            // Arrange
            Measurement measurement = Measurement.Create();
            bool wasCalled = false;
            Action<Stopwatch> action = (sw) => { wasCalled = true; };

            // Act
            measurement.OfAction(action);

            // Assert
            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void OfAsyncAction_Taking_AsyncAction_When_Called_Sets_Appropriate_Stopwatch_Strategy()
        {
            // Arrange
            Measurement measurement = Measurement.Create();
            AsyncAction action = async () => { };

            // Act
            measurement.OfAsyncAction(action);

            // Assert
            Assert.IsInstanceOf<AsyncAutoStopwatchStrategy>(measurement.MeasurementStrategy);
            var strategy = (AsyncAutoStopwatchStrategy)measurement.MeasurementStrategy;
            Assert.AreEqual(action, strategy.Action);
        }
        [Test]
        public void OfAsyncAction_Taking_AsyncAction_When_Called_With_Null_Throws_ArgumentNullException()
        {
            // Arrange
            Measurement measurement = Measurement.Create();

            // Assert
            Assert.Catch<ArgumentNullException>(() =>
            {
                // Act
                measurement.OfAsyncAction(null as AsyncAction);
            });
        }
        [Test]
        public void OfAsyncAction_Taking_AsyncAction_When_Called_Does_Not_Calls_The_Action()
        {
            // Arrange
            Measurement measurement = Measurement.Create();
            bool wasCalled = false;
            AsyncAction action = async () => { wasCalled = true; };

            // Act
            measurement.OfAsyncAction(action);

            // Assert
            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void OfAsyncAction_Taking_AsyncActionWithSW_When_Called_Sets_Appropriate_Stopwatch_Strategy()
        {
            // Arrange
            Measurement measurement = Measurement.Create();
            AsyncActionWithStopwatch action = async (sw) => { };

            // Act
            measurement.OfAsyncAction(action);

            // Assert
            Assert.IsInstanceOf<AsyncManualStopwatchStrategy>(measurement.MeasurementStrategy);
            var strategy = (AsyncManualStopwatchStrategy)measurement.MeasurementStrategy;
            Assert.AreEqual(action, strategy.Action);
        }
        [Test]
        public void OfAsyncAction_Taking_AsyncActionWithSW_When_Called_With_Null_Throws_ArgumentNullException()
        {
            // Arrange
            Measurement measurement = Measurement.Create();

            // Assert
            Assert.Catch<ArgumentNullException>(() =>
            {
                // Act
                measurement.OfAsyncAction(null as AsyncActionWithStopwatch);
            });
        }
        [Test]
        public void OfAsyncAction_Taking_AsyncActionWithSW_When_Called_Does_Not_Calls_The_Action()
        {
            // Arrange
            Measurement measurement = Measurement.Create();
            bool wasCalled = false;
            AsyncActionWithStopwatch action = async (sw) => { wasCalled = true; };

            // Act
            measurement.OfAsyncAction(action);

            // Assert
            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void WithPreAction_When_Called_Sets_PreAction()
        {
            // Arrange
            Measurement measurement = Measurement.Create();
            Action preAction = () => { };

            // Act
            measurement.WithPreAction(preAction);

            // Assert
            Assert.AreEqual(preAction, measurement.PreAction);
        }
        [Test]
        public void WithPreAction_When_Called_With_Null_Throws_ArgumentNullException()
        {
            // Arrange
            Measurement measurement = Measurement.Create();

            Assert.Catch<ArgumentNullException>(() =>
            {
                // Act
                measurement.WithPreAction(null as Action);
            });
        }
        [Test]
        public void WithPreAction_When_Called_Does_Not_Calls_Provided_Action()
        {
            // Arrange
            Measurement measurement = Measurement.Create();
            bool wasCalled = false;
            Action preAction = () => { wasCalled = true; };

            // Act
            measurement.WithPreAction(preAction);

            // Assert
            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void WithPostAction_When_Called_Sets_PostAction()
        {
            // Arrange
            Measurement measurement = Measurement.Create();
            Action postAction = () => { };

            // Act
            measurement.WithPostAction(postAction);

            // Assert
            Assert.AreEqual(postAction, measurement.PostAction);
        }
        [Test]
        public void WithPostAction_When_Called_With_Null_Throws_ArgumentNullException()
        {
            // Arrange
            Measurement measurement = Measurement.Create();

            Assert.Catch<ArgumentNullException>(() =>
            {
                // Act
                measurement.WithPostAction(null as Action);
            });
        }
        [Test]
        public void WithPostAction_When_Called_Does_Not_Calls_Provided_Action()
        {
            // Arrange
            Measurement measurement = Measurement.Create();
            bool wasCalled = false;
            Action postAction = () => { wasCalled = true; };

            // Act
            measurement.WithPostAction(postAction);

            // Assert
            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void WithData_When_Called_Sets_Measurement_Additional_Data()
        {
            // Arrange
            Measurement measurement = Measurement.Create();
            Dictionary<string, object> data = new Dictionary<string, object>();
            string key1 = "key1";
            object value1 = new object();
            string key2 = "key2";
            object value2 = new object();

            // Act
            measurement.WithData(key1, value1);
            measurement.WithData(key2, value2);

            // Assert
            Assert.IsTrue(measurement.AdditionalData.ContainsKey(key1));
            Assert.IsTrue(measurement.AdditionalData.ContainsValue(value1));
            Assert.IsTrue(measurement.AdditionalData.ContainsKey(key2));
            Assert.IsTrue(measurement.AdditionalData.ContainsValue(value2));
        }
        [Test]
        public void WithData_When_Called_With_Duplicate_Keys_Throws_ArgumentException()
        {
            // Arrange
            Measurement measurement = Measurement.Create();
            Dictionary<string, object> data = new Dictionary<string, object>();
            string key1 = "key1";
            object value1 = new object();
            measurement.WithData(key1, value1);
            string duplicate = "key1";

            // Assert
            Assert.Catch<ArgumentException>(() =>
            {
                // Act
                measurement.WithData(duplicate, new object());
            });

        }

        [Test]
        public void Run_When_Called_Calls_PreAction_Specified_Times()
        {
            // Arrange
            int timesOfRun = 15;
            int timesPreActionCalled = 0;
            Action preAction = () => { timesPreActionCalled++; };
            Measurement measurement = Measurement.Create()
                                                 .OfAction(() => { })
                                                 .WithPreAction(preAction);
            // Act
            measurement.Run(timesOfRun);

            // Assert
            Assert.AreEqual(timesOfRun, timesPreActionCalled);
        }
        [Test]
        public void Run_When_Called_Calls_PostAction_Specified_Times()
        {
            // Arrange
            int timesOfRun = 15;
            int timesPostActionCalled = 0;
            Action postAction = () => { timesPostActionCalled++; };
            Measurement measurement = Measurement.Create()
                                                 .OfAction(() => { })
                                                 .WithPostAction(postAction);
            // Act
            measurement.Run(timesOfRun);

            // Assert
            Assert.AreEqual(timesOfRun, timesPostActionCalled);
        }
        [Test]
        public void Run_When_Called_Given_That_No_Pre_And_Post_Actions_Were_Specified_Does_Not_Throw()
        {
            // Arrange
            Measurement measurement = Measurement.Create()
                                                 .OfAction(() => { });
            // Act
            measurement.Run(20);
        }
        [Test]
        public void Run_When_Main_Action_Throws_Exception_Post_Action_Is_Guaranteed_To_Run()
        {
            // Arrange 
            bool postActionWasCalled = false;
            Action postAction = () => { postActionWasCalled = true; };
            Measurement measurement = Measurement.Create()
                                                 .OfAction(() => { throw null; })
                                                 .WithPostAction(postAction);
            Assert.Catch<Exception>(() =>
            {
                // Act
                measurement.Run(1);
            });

            // Assert
            Assert.IsTrue(postActionWasCalled);
        }
        [Test]
        public void Run_When_Called_Calls_Main_Action_Specified_Times1()
        {
            // Arrange
            int timesOfRun = 15;
            int timesMainActionCalled = 0;
            Action mainAction = () => { timesMainActionCalled++; };
            Measurement measurement = Measurement.Create()
                                                 .OfAction(mainAction);
            // Act
            measurement.Run(timesOfRun);

            // Assert
            Assert.AreEqual(timesOfRun, timesMainActionCalled);
        }
        [Test]
        public void Run_When_Called_Calls_Main_Action_Specified_Times2()
        {
            // Arrange
            int timesOfRun = 15;
            int timesMainActionCalled = 0;
            Action<Stopwatch> mainAction = (sw) => { timesMainActionCalled++; };
            Measurement measurement = Measurement.Create()
                                                 .OfAction(mainAction);
            // Act
            measurement.Run(timesOfRun);

            // Assert
            Assert.AreEqual(timesOfRun, timesMainActionCalled);
        }
        [Test]
        public void Run_When_Called_Calls_Main_Action_Specified_Times3()
        {
            // Arrange
            int timesOfRun = 15;
            int timesMainActionCalled = 0;
            AsyncAction mainAction = async () => { timesMainActionCalled++; };
            Measurement measurement = Measurement.Create()
                                                 .OfAsyncAction(mainAction);
            // Act
            measurement.Run(timesOfRun);

            // Assert
            Assert.AreEqual(timesOfRun, timesMainActionCalled);
        }
        [Test]
        public void Run_When_Called_Calls_Main_Action_Specified_Times4()
        {
            // Arrange
            int timesOfRun = 15;
            int timesMainActionCalled = 0;
            AsyncActionWithStopwatch mainAction = async (sw) => { timesMainActionCalled++; };
            Measurement measurement = Measurement.Create()
                                                 .OfAsyncAction(mainAction);
            // Act
            measurement.Run(timesOfRun);

            // Assert
            Assert.AreEqual(timesOfRun, timesMainActionCalled);
        }

        [Test]
        public void Run_When_Completed_Returns_Run_Result_With_Result_Data()
        {
            // Arrange
            int timesOfRun = 15;
            string measurementName = "Measurement1";
            string attemptName = "Attempt1";
            string dataKey1 = "key1";
            object dataObject1 = new object();

            Measurement measurement = Measurement.Create(measurementName)
                                                 .OfAction(() => { })
                                                 .WithData(dataKey1, dataObject1);
            
            // Act
            var result = measurement.Run(timesOfRun, attemptName);

            // Assert
            Assert.AreEqual(measurementName, result.MeasurementName);
            Assert.AreEqual(attemptName, result.AttemptName);
            Assert.AreEqual(timesOfRun, result.NumberOfRuns);
            Assert.AreNotEqual(0, result.ElapsedTime.Ticks);

            Assert.AreEqual(1, result.AdditionalData.Count);
            Assert.IsTrue(result.AdditionalData.ContainsKey(dataKey1));
            Assert.IsTrue(result.AdditionalData.ContainsValue(dataObject1));
        }

        [Test]
        public void Run_When_Called_Given_That_No_Action_Was_Provided_Throws_InvalidOperationException()
        {
            // Arrange 
            Measurement measurement = Measurement.Create();

            var ex = Assert.Catch<InvalidOperationException>(() =>
            {
                // Act
                measurement.Run(5);
            });
            Assert.AreEqual("No action was provided to conduct a measurement. Please specify the action.", ex.Message);
        }
        [Test]
        public void Run_When_Called_Calls_Pre_Action_Main_Action_And_Post_Action_In_Correct_Order()
        {
            // Arrange 
            string order = null;
            Action preAction = () => { order = "preAction."; };
            Action mainAction = () => { order = order + "mainAction."; };
            Action postAction = () => { order = order + "postAction"; };
            Measurement measurement = Measurement.Create()
                                                 .OfAction(mainAction)
                                                 .WithPreAction(preAction)
                                                 .WithPostAction(postAction);
            // Act
            measurement.Run(1);

            // Assert
            Assert.AreEqual("preAction.mainAction.postAction", order);
        }
        [Test]
        public void Run_When_Called_Accumulates_Stop_Watch_Values_To_Create_A_Result_Value()
        {
            // Arrange  
            int timesOfRun = 10;
            long stopWatchAccumulatedTicks = 0;
            Action<Stopwatch> mainAction = (sw) =>
            {
                sw.Start();
                Thread.Sleep(new Random().Next(50, 100));
                sw.Stop();
                stopWatchAccumulatedTicks += sw.ElapsedTicks;
            };
            Measurement measurement = Measurement.Create()
                                                 .OfAction(mainAction);
            // Act
            var result = measurement.Run(timesOfRun);

            // Assert
            Assert.AreEqual(stopWatchAccumulatedTicks, result.ElapsedTicks);
        }
        [Test]
        public void Run_When_Called_Accumulates_Stop_Watch_Values_To_Create_A_Result_Value2()
        {
            // Arrange  
            int timesOfRun = 10;
            long stopWatchAccumulatedTicks = 0;
            AsyncActionWithStopwatch mainAction = async (sw) =>
            {
                sw.Start();
                await Task.Delay(new Random().Next(50, 100));
                sw.Stop();
                stopWatchAccumulatedTicks += sw.ElapsedTicks;
            };
            Measurement measurement = Measurement.Create()
                                                 .OfAsyncAction(mainAction);
            // Act
            var result = measurement.Run(timesOfRun);

            // Assert
            Assert.AreEqual(stopWatchAccumulatedTicks, result.ElapsedTicks);
        }
        [Test]
        public void Run_When_Called_With_Canceled_Token_Throws_OperationCanceledException()
        {
            // Arrange 
            int timesMainActionCalled = 0;
            Action mainAction = () => { timesMainActionCalled++; };
            Measurement measurement = Measurement.Create()
                                                 .OfAction(mainAction);
            CancellationToken cancellationToken = new CancellationToken(canceled: true);

            // Assert
            Assert.Catch<OperationCanceledException>(() =>
            {
                // Act
                measurement.Run(15, "", cancellationToken);
            });
            Assert.AreEqual(0, timesMainActionCalled);
        }
        [Test]
        public void Run_When_Token_Is_Canceled_Throws_OperationCanceledException()
        {
            // Arrange
            int timesMainActionCalled = 0;
            CancellationTokenSource cts = new CancellationTokenSource();
            Action mainAction = () =>
            {
                timesMainActionCalled++;
                cts.Cancel();
            };
            Measurement measurement = Measurement.Create()
                                                 .OfAction(mainAction);

            // Assert
            Assert.Catch<OperationCanceledException>(() =>
            {
                // Act
                measurement.Run(15, "", cts.Token);
            });
            Assert.AreEqual(1, timesMainActionCalled);
        }

        [Test]
        public void Run_When_Timeout_Reaches_Throws_OperationCanceledException()
        {
            // Arrange
            int timesMainActionCalled = 0;
            int timeoutMilliseconds = 100;
            CancellationTokenSource cts = new CancellationTokenSource();
            Action mainAction = () =>
            {
                timesMainActionCalled++;
                Thread.Sleep(150);
            };
            Measurement measurement = Measurement.Create()
                                                 .OfAction(mainAction);

            // Assert
            Assert.Catch<OperationCanceledException>(() =>
            {
                // Act
                measurement.Run(15, timeoutMilliseconds);
            });
            Assert.AreEqual(1, timesMainActionCalled);
        }
    }
}
