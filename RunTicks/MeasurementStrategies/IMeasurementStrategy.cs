using System.Diagnostics;

namespace RunTicks.MeasurementStrategies
{
    internal interface IMeasurementStrategy
    {
        void ExecuteAction(Stopwatch stopwatch);
    }
}
