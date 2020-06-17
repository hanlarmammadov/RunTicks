using System.Diagnostics;

namespace RunTicks.MeasurementStrategies
{
    public interface IMeasurementStrategy
    {
        void ExecuteAction(Stopwatch stopwatch);
    }
}
