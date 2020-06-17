using System;
using System.Diagnostics;

namespace RunTicks.MeasurementStrategies
{
    public class AsyncManualStopwatchStrategy : IMeasurementStrategy
    {
        private readonly AsyncActionWithStopwatch _action;

        public AsyncManualStopwatchStrategy(AsyncActionWithStopwatch action)
        {
            _action = action;
        }

        internal AsyncActionWithStopwatch Action => _action;

        public void ExecuteAction(Stopwatch stopwatch)
        {
            try
            {
                _action(stopwatch).Wait();
            }
            finally
            {
                if (stopwatch.IsRunning)
                    stopwatch.Stop();
            }
        }
    }
}
