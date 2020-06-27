using Examples.Helpers;
using NUnit.Framework;
using RunTicks;

namespace Examples
{
    [TestFixture]
    public class MeasurementExamples
    {
        private void LogTheMeasurement(RunResult runResult)
        {

        }

        [Test]
        public void SimplestExample()
        {
            // Create and run the measurement.
            var result = Measurement.Create("My measurement 1")
                                    .OfAction(() =>
            {
                TestHelpers.SomeAction();
            }).Run(100);

            // Save the results of your measurement for 
            // later analysis using your logging tools.
            LogTheMeasurement(result);

            // Or make some assertions.
            result.Should.NotExceedAverageMilliseconds(100);
        }

        [Test]
        public void ExampleWithManualStopwatch()
        {
            // Create the measurement.
            Measurement measurement = Measurement.Create("My measurement 1")
                                                 .OfAction((sw) =>
            {
                // Manually start and stop the Stopwatch for better accuracy.
                sw.Start();
                TestHelpers.SomeAction();
                sw.Stop();
            });

            // Conduct the measurement by running 
            // the action specified number of times.
            RunResult result = measurement.Run(200);

            // Make your assertions.
            result.Should.NotExceedAverageTicks(300_000);
        }

        [Test]
        public void ExampleWithPreAndPostActions()
        {
            // Create the measurement.
            Measurement measurement = Measurement.Create("My measurement 1")
                                                 .OfAction(() =>
                                                 {
                                                     TestHelpers.SomeAction();
                                                 })
                                                 .WithPreAction(() =>
                                                 {
                                                     // This will be called BEFORE EACH call of 
                                                     // the measured action. Of course, the execution time 
                                                     // of this function is not considered.

                                                     TestHelpers.SomeInitialization();
                                                 })
                                                 .WithPostAction(() =>
                                                 {
                                                     // This will be called AFTER EACH call of 
                                                     // the measured action. Of course, the execution time 
                                                     // of this function is not considered.

                                                     TestHelpers.SomeCleanUp();
                                                 });

            // Conduct the measurement by running 
            // the action specified number of times.
            RunResult result = measurement.Run(200);

            // Make your assertions.
            result.Should.NotExceedAverageTicks(500_000);
        }

        [Test]
        public void ExampleWithAsyncAction()
        {
            // Create the measurement.
            Measurement measurement = Measurement.Create("My measurement 1")
                                                 .OfAsyncAction(async () =>
            {
                await TestHelpers.SomeAsyncAction();
            });

            // Conduct the measurement by running 
            // the action specified number of times.
            RunResult result = measurement.Run(50);

            // Make your assertions.
            result.Should.NotExceedAverageTicks(300_000);
        }

        [Test]
        public void ExampleWithAsyncActionAndManualStopwatch()
        {
            // Create the measurement.
            Measurement measurement = Measurement.Create("My measurement 1")
                                                 .OfAsyncAction(async (sw) =>
            {
                sw.Start();
                await TestHelpers.SomeAsyncAction();
                sw.Stop();
            });

            // Conduct the measurement by running 
            // the action specified number of times.
            RunResult result = measurement.Run(50);

            // Make your assertions.
            result.Should.NotExceedAverageTicks(300_000);
        }

        [Test]
        public void ExampleWithAddingCustomDataToTheMeasurement()
        {
            // Create the measurement.
            Measurement measurement = Measurement.Create("My measurement 1")
                                                 .OfAction((sw) =>
            {
                // Manually start and stop the Stopwatch for better accuracy.
                sw.Start();
                TestHelpers.SomeAction();
                sw.Stop();
            });

            // Add custom data to measurement. It will be added to 
            // every result of this measurement.
            measurement.WithData("Issue1", "Some useful info to be saved with the results.");
            measurement.WithData("Issue2", "Some useful info to be saved with the results.");
                 
            // Conduct the measurement by running 
            // the action specified number of times.
            RunResult result = measurement.Run(150);

            // Save the results of your measurement for 
            // later analysis using your logging tools.
            LogTheMeasurement(result);
        }
    }
}