# RunTicks

> Simple library for performance testing of code.
 
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

## Description

The instance of `RunTicks.RunResult` - which is a result of measurement contains the following metrics and data:


| Metrics               | Type       | Description                               |
|-----------------------|------------|-------------------------------------------|
| NumberOfRuns          | Int64      | Number of runs of the measuring action method. |
| ElapsedTime           | TimeSpan   | Total time elapsed for this attempt of the measurement. |
| TotalElapsedTicks     | Int64      | Total ticks elapsed for this attempt of the measurement. |
| TotalMilliseconds     | Double     | Total milliseconds elapsed for this attempt of the measurement. |
| TotalSeconds          | Double     | Total seconds elapsed for this attempt of the measurement. |
| TotalMinutes          | Double     | Total minutes elapsed for this attempt of the measurement. |
| AverageTicks          | Double     | Average ticks elapsed for a single action run. |
| AverageMilliseconds   | Double     | Average milliseconds elapsed for a single action run. |
| AverageSeconds        | Double     | Average seconds elapsed for a single action run. |
| AverageMinutes        | Double     | Average minutes elapsed for a single action run. |
| MeasurementName       | String     | Name of the measurement. |
| AttemptName           | String     | Name of the current attempt. |
| StartDate             | DateTime   | Start date of the measurement. |
| EndDate               | DateTime   | End date of the measurement. |
| AdditionalData        | Dictionary<String, Object>   | Optional user provided data related to the measurement. |

*Average metrics are counted as totals divide number of runs.   
*1 millisecond = 10 000 ticks.   

## More Examples

### Example with Manual Stopwatch

Despite very convenient, using parameterless wrapper action has a flaw - the time costs of calling this wrapper function are added to the resulting time metrics. If your measurements are quite sensitive to be affected from these costs, you can use the overload of `OfAction` method which allows you to manipulate (start and stop) the stopwatch manually. This overload takes an action delegate taking an instance of `System.Diagnostics.StopWatch` as a parameter:

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
---

### Example with pre- and post- actions

Sometimes you need some steps that should be taken before and after each run of the measured code. For this you can specify pre- and post- actions which will be executed prior and after of each run of the main (measured) action. Needless to say that execution times of these actions do not affect the measured metrics.
 
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
---

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
---

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
---

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