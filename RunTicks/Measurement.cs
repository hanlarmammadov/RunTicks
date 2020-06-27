using RunTicks.MeasurementStrategies;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace RunTicks
{
    /// <summary>
    /// Main class which is a starting point for creating and running the measurements. 
    /// </summary>
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

        /// <summary>
        /// Starting point for building a measurement using builder methods.
        /// </summary>
        /// <param name="name">Measurement name.</param>
        /// <returns>Created measurement.</returns>
        public static Measurement Create(string name)
        {
            return new Measurement(name);
        }
        /// <summary>
        /// Starting point for building a measurement using builder methods.
        /// </summary>
        /// <returns>Created measurement.</returns>
        public static Measurement Create()
        {
            return new Measurement(null);
        }
        /// <summary>
        /// Adds a parameterless action that is being measured.
        /// </summary>
        /// <param name="action"></param>
        /// <returns>Self.</returns>
        public Measurement OfAction(Action action)
        {
            ValidateActionAndMeasurement(action);
            _measurementStrategy = new AutoStopwatchStrategy(action);
            return this;
        }
        /// <summary>
        /// Adds a measured action taking an instance of <c>System.Diagnostics.Stopwatch</c>.
        /// </summary>
        /// <param name="action"></param>
        /// <returns>Self.</returns>
        public Measurement OfAction(Action<Stopwatch> action)
        {
            ValidateActionAndMeasurement(action);
            _measurementStrategy = new ManualStopwatchStrategy(action);
            return this;
        }
        /// <summary>
        /// Adds a measured async parameterless action that is being measured.
        /// </summary>
        /// <param name="action"></param>
        /// <returns>Self.</returns>
        public Measurement OfAsyncAction(AsyncAction action)
        {
            ValidateActionAndMeasurement(action);
            _measurementStrategy = new AsyncAutoStopwatchStrategy(action);
            return this;
        }
        /// <summary>
        /// Adds a measured async action taking an instance of <c>System.Diagnostics.Stopwatch</c>.
        /// </summary>
        /// <param name="action"></param>
        /// <returns>Self.</returns>
        public Measurement OfAsyncAction(AsyncActionWithStopwatch action)
        {
            ValidateActionAndMeasurement(action);
            _measurementStrategy = new AsyncManualStopwatchStrategy(action);
            return this;
        }
        /// <summary>
        /// Specifies a pre-action that is executed before every iteration of execution of the measured action.
        /// The execution time of pre-action is never considered.
        /// </summary>
        /// <param name="preAction"></param>
        /// <returns>Self.</returns>
        public Measurement WithPreAction(Action preAction)
        {
            _preAction = preAction ?? throw new ArgumentNullException("preAction");
            return this;
        }
        /// <summary>
        /// Specifies a post-action that is executed after every iteration of execution of the measured action.
        /// The execution time of post-action is never considered.
        /// </summary>
        /// <param name="postAction"></param>
        /// <returns>Self.</returns>
        public Measurement WithPostAction(Action postAction)
        {
            _postAction = postAction ?? throw new ArgumentNullException("postAction");
            return this;
        }
        /// <summary>
        /// Specifies some custom data that will be added to the result of this measurement.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns>Self.</returns>
        public Measurement WithData(string name, object value)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (_additionalData == null)
                _additionalData = new Dictionary<string, object>();
            _additionalData.Add(name, value);
            return this;
        }
        /// <summary>
        /// Performs the time measurement.
        /// </summary>
        /// <param name="numberOfTimes">Specifies the number of times the measured action should be called during the measurement.</param>
        /// <param name="attemptName">Name of the current attempt.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Result of the measurement containing the measured metrics.</returns>
        public RunResult Run(Int64 numberOfTimes, string attemptName = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (_measurementStrategy == null)
                throw new InvalidOperationException("No action was provided to conduct a measurement. Please specify the action.");

            // Stopwatch.
            Stopwatch stopwatch = new Stopwatch();
            Int64 elapsedTicks = 0;
            DateTime startDate = DateTime.Now;

            // Execute action.
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
        /// <summary>
        /// Performs the time measurement.
        /// </summary>
        /// <param name="numberOfTimes">Specifies the number of times the measured action should be called during the measurement.</param>
        /// <param name="attemptName">Name of the current attempt.</param>
        /// <param name="timeout">Specifies the timeout in milliseconds after which the measurement terminates with a <c>System.OperationCanceledException</c> exception.</param>
        /// <returns>Result of the measurement containing the measured metrics.</returns>
        public RunResult Run(Int64 numberOfTimes, string attemptName, int timeout)
        {
            return Run(numberOfTimes, attemptName, new CancellationTokenSource(timeout).Token);
        }
        /// <summary>
        /// Performs the time measurement.
        /// </summary>
        /// <param name="numberOfTimes">Specifies the number of times the measured action should be called during the measurement.</param>
        /// <param name="timeout">Specifies the timeout in milliseconds after which the measurement terminates with a <c>System.OperationCanceledException</c> exception.</param>
        /// <returns>Result of the measurement containing the measured metrics.</returns>
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
