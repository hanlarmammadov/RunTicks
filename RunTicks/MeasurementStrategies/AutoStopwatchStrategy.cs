using System;
using System.Diagnostics;

namespace RunTicks.MeasurementStrategies
{
    internal class AutoStopwatchStrategy : IMeasurementStrategy
    {
        private readonly Action _action;

        public AutoStopwatchStrategy(Action action)
        {
            _action = action;
        }

        internal Action Action => _action;

        public void ExecuteAction(Stopwatch stopwatch)
        {
            try
            {
                stopwatch.Start();
                _action();
            }
            finally
            {
                stopwatch.Stop();
            }
        }
    }
}
