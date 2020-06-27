using System;
using System.Collections.Generic;

namespace RunTicks
{
    /// <summary>
    /// Represents the measured metrics as a result of conducted measurement.
    /// </summary>
    public class RunResult
    {
        private readonly long _numberOfRuns;
        private readonly DateTime _startDate;
        private readonly DateTime _endDate;
        private readonly string _measurementName;
        private readonly string _attemptName;
        private readonly TimeSpan _elapsedTime;
        private readonly Dictionary<string, object> _additionalData;
        private readonly AssertPart _assertPart;

        /// <summary>
        /// Creates an instance of <c>RunTicks.RunResult</c> using the provided parameters.
        /// </summary>
        /// <param name="elapsedTicks">Total ticks elapsed for this attempt of the measurement.</param>
        /// <param name="numberOfRuns">Number of runs of measuring action method.</param>
        /// <param name="startDate">Start date of the measurement.</param>
        /// <param name="endDate">End date of the measurement.</param>
        /// <param name="measurementName">Name of the measurement.</param>
        /// <param name="attemptName">Name of the current attempt.</param>
        /// <param name="additionalData">Optional user data related to this measurement.</param>
        internal RunResult(Int64 elapsedTicks,
                           Int64 numberOfRuns,
                           DateTime startDate,
                           DateTime endDate,
                           string measurementName,
                           string attemptName,
                           Dictionary<string, object> additionalData = null)
        {
            _elapsedTime = new TimeSpan(elapsedTicks);
            _numberOfRuns = numberOfRuns;
            _startDate = startDate;
            _endDate = endDate;
            _measurementName = measurementName;
            _attemptName = attemptName;
            _additionalData = additionalData;
            _assertPart = new AssertPart(this);
        }

        /// <summary>
        /// Number of runs of measuring action method.
        /// </summary>
        public long NumberOfRuns => _numberOfRuns;
        /// <summary>
        /// Total time elapsed for this attempt of the measurement.
        /// </summary>
        public TimeSpan ElapsedTime => _elapsedTime;
        /// <summary>
        /// Total ticks elapsed for this attempt of the measurement.
        /// 1 millisecond = 10 000 ticks.
        /// </summary>
        public Int64 TotalElapsedTicks => _elapsedTime.Ticks;
        /// <summary>
        /// Total milliseconds elapsed for this attempt of the measurement.
        /// </summary>
        public Double TotalMilliseconds => _elapsedTime.TotalMilliseconds;
        /// <summary>
        /// Total seconds elapsed for this attempt of the measurement.
        /// </summary>
        public Double TotalSeconds => _elapsedTime.TotalSeconds;
        /// <summary>
        /// Total minutes elapsed for this attempt of the measurement.
        /// </summary>
        public Double TotalMinutes => _elapsedTime.TotalMinutes;

        /// <summary>
        /// Average ticks elapsed for a single action run.
        /// Averages are counted as totals divide number of runs.
        /// 1 millisecond = 10 000 ticks.
        /// </summary>
        public Double AverageTicks => ((Double)_elapsedTime.Ticks) / _numberOfRuns;
        /// <summary>
        /// Average milliseconds elapsed for a single action run.
        /// Averages are counted as totals divide number of runs.
        /// </summary>
        public Double AverageMilliseconds => ((Double)_elapsedTime.TotalMilliseconds) / _numberOfRuns;
        /// <summary>
        /// Average seconds elapsed for a single action run.
        /// Averages are counted as totals divide number of runs.
        /// </summary>
        public Double AverageSeconds => ((Double)_elapsedTime.TotalSeconds) / _numberOfRuns;
        /// <summary>
        /// Average minutes elapsed for a single action run.
        /// Averages are counted as totals divide number of runs.
        /// </summary>
        public Double AverageMinutes => ((Double)_elapsedTime.TotalMinutes) / _numberOfRuns;

        /// <summary>
        /// Name of the measurement.
        /// </summary>
        public string MeasurementName => _measurementName;
        /// <summary>
        /// Name of the current attempt.
        /// </summary>
        public string AttemptName => _attemptName;
        /// <summary>
        /// Start date of the measurement.
        /// </summary>
        public DateTime StartDate => _startDate;
        /// <summary>
        /// End date of the measurement.
        /// </summary>
        public DateTime EndDate => _endDate;
        /// <summary>
        /// Optional user provided data related to the measurement.
        /// </summary>
        public Dictionary<string, object> AdditionalData => _additionalData;
        /// <summary>
        /// Test assertions for this measurement result.
        /// </summary>
        public AssertPart Should => _assertPart;
    }
}
