# RunTicks

> Simple library for execution time measurements.
 
Dependencies:  
* .Net Standard >= 1.0

## Installation

The NuGet <a href="https://www.nuget.org/packages/RunTicks" target="_blank">package</a>:

```powershell
PM> Install-Package RunTicks
```
 
## Quick Start

The following example shows how to use RunTicks to measure execution time for some action by executing it 100 times and getting the metrics of this measurement:

```cs
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
```

## More Examples

### Example with Manual Stopwatch

```cs
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
```

### Example with pre- and post- actions

```cs
// Create the measurement.
Measurement measurement = Measurement
        .Create("My measurement 1")
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
```

### Example with Async Action

```cs
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
```

### Example with Async Action and Manual Stopwatch

```cs
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
```

### Example with Adding Custom Data to the Measurement

```cs
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
```
 
## License

[APACHE LICENSE 2.0](https://www.apache.org/licenses/LICENSE-2.0)