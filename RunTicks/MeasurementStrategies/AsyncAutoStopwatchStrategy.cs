using System.Diagnostics;

namespace RunTicks.MeasurementStrategies
{
    public class AsyncAutoStopwatchStrategy : IMeasurementStrategy
    {
        private readonly AsyncAction _action;

        public AsyncAutoStopwatchStrategy(AsyncAction action)
        {
            _action = action;
        }

        internal AsyncAction Action => _action;

        public void ExecuteAction(Stopwatch stopwatch)
        {
            try
            {
                stopwatch.Start();
                _action().Wait();
            }
            finally
            {
                stopwatch.Stop();
            }
        }
    }
}
