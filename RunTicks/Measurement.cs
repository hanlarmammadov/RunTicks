using RunTicks.MeasurementStrategies;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace RunTicks
{
    public class Measurement
    {
        private string _measurementName;
        private IMeasurementStrategy _measurementStrategy;
        private Action _preAction;
        private Action _postAction;
        private Dictionary<string, object> _additionalData;

        private Measurement(string name)
        {
            _measurementName = name;
        }

        public string Name => _measurementName;
        public IMeasurementStrategy MeasurementStrategy => _measurementStrategy;
        public Dictionary<string, object> AdditionalData => _additionalData;
        internal Action PreAction => _preAction;
        internal Action PostAction => _postAction;

        public static Measurement Create(string name)
        {
            return new Measurement(name);
        }
        public static Measurement Create()
        {
            return new Measurement(null);
        }
        public Measurement OfAction(Action action)
        {
            ValidateActionAndMeasurement(action);
            _measurementStrategy = new AutoStopwatchStrategy(action);
            return this;
        }
        public Measurement OfAction(Action<Stopwatch> action)
        {
            ValidateActionAndMeasurement(action);
            _measurementStrategy = new ManualStopwatchStrategy(action);
            return this;
        }
        public Measurement OfAsyncAction(AsyncAction action)
        {
            ValidateActionAndMeasurement(action);
            _measurementStrategy = new AsyncAutoStopwatchStrategy(action);
            return this;
        }
        public Measurement OfAsyncAction(AsyncActionWithStopwatch action)
        {
            ValidateActionAndMeasurement(action);
            _measurementStrategy = new AsyncManualStopwatchStrategy(action);
            return this;
        }
        public Measurement WithPreAction(Action preAction)
        {

            _preAction = preAction??throw new ArgumentNullException("preAction");
            return this;
        }
        public Measurement WithPostAction(Action postAction)
        {
            _postAction = postAction ?? throw new ArgumentNullException("postAction");
            return this;
        }
        public Measurement WithData(string name, object value)
        {
            if (_additionalData == null)
                _additionalData = new Dictionary<string, object>();
            _additionalData.Add(name, value);
            return this;
        }
        public RunResult Run(Int64 numberOfTimes, string attemptName = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (_measurementStrategy == null)
                throw new InvalidOperationException("No action was provided to conduct a measurement. Please specify the action.");

            // Stopwatch
                Stopwatch stopwatch = new Stopwatch();
            Int64 elapsedTicks = 0;
            DateTime startDate = DateTime.Now;

            // Execute action
            for (int i = 1; i <= numberOfTimes; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                try
                {
                    _preAction?.Invoke();
                    _measurementStrategy.ExecuteAction(stopwatch);
                }
                finally
                {
                    _postAction?.Invoke();
                }
                elapsedTicks += stopwatch.ElapsedTicks;
                stopwatch.Reset();
            }

            DateTime endDate = DateTime.Now;
            return new RunResult(elapsedTicks, numberOfTimes, startDate, endDate, _measurementName, attemptName, _additionalData);
        }
        public RunResult Run(Int64 numberOfTimes, string attemptName, int timeout)
        {
            return Run(numberOfTimes, attemptName, new CancellationTokenSource(timeout).Token);
        }
        public RunResult Run(Int64 numberOfTimes, int timeout)
        {
            return Run(numberOfTimes, null, new CancellationTokenSource(timeout).Token);
        }
        private void ValidateActionAndMeasurement(Object action)
        {
            if (action == null)
                throw new ArgumentNullException("action");
            if (_measurementStrategy != null)
                throw new InvalidOperationException("Measurement action has already been set.");
        }
    }
}
