using System;
using System.Collections.Generic;

namespace RunTicks
{
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

        public RunResult(Int64 elapsedTicks,
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
        public TimeSpan ElapsedTime => _elapsedTime; 
        public long NumberOfRuns => _numberOfRuns;
        // Total values.
        public Int64 ElapsedTicks => _elapsedTime.Ticks; 
        public Double Milliseconds => _elapsedTime.TotalMilliseconds;
        public Double Seconds => _elapsedTime.TotalSeconds;
        public Double Minutes => _elapsedTime.TotalMinutes;
        // Average values.
        public Double AverageTicks => ((Double)_elapsedTime.Ticks) / _numberOfRuns;
        public Double AverageMilliseconds => ((Double)_elapsedTime.TotalMilliseconds) / _numberOfRuns;
        public Double AverageSeconds => ((Double)_elapsedTime.TotalSeconds) / _numberOfRuns;
        public Double AverageMinutes => ((Double)_elapsedTime.TotalMinutes) / _numberOfRuns;
        // Other data.
        public string MeasurementName => _measurementName;
        public string AttemptName => _attemptName;
        public DateTime StartDate => _startDate;
        public DateTime EndDate => _endDate;
        public Dictionary<string, object> AdditionalData => _additionalData;
        // Assert part.
        public AssertPart Should => _assertPart;
    }
}
