using System;
using System.Diagnostics;

namespace RunTicks.MeasurementStrategies
{
    public class ManualStopwatchStrategy : IMeasurementStrategy
    {
        private readonly Action<Stopwatch> _action;

        public ManualStopwatchStrategy(Action<Stopwatch> action)
        {
            _action = action;
        }

        internal Action<Stopwatch> Action => _action;

        public void ExecuteAction(Stopwatch stopwatch)
        {
            try
            {
                _action(stopwatch);
            }
            finally
            {
                if (stopwatch.IsRunning)
                    stopwatch.Stop();
            }
        }
    }
}
