using RunTicks.AssertionMessages;
using System;
using System.ComponentModel;
using System.Globalization;

namespace RunTicks
{
    /// <summary>
    /// Contains various assertions for measurement result.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class AssertPart
    {
        private readonly RunResult _result;

        internal AssertPart(RunResult result)
        {
            _result = result;
        }

        internal RunResult MeasureResult => _result;
        internal CultureInfo CurrentCulture => CultureInfo.CurrentCulture ?? CultureInfo.InvariantCulture;

        // Assertions on totals.
        public void NotExceedTotalTicks(Int64 ticks, string description = null)
        {
            if (_result.TotalElapsedTicks > ticks)
                throw new AssertionException(new NotExceedTotalTicksAssertionMessage(ticks, _result.TotalElapsedTicks, description).Generate(CurrentCulture));
        }
        public void NotExceedTotalMilliseconds(Int64 milliseconds, string description = null)
        {
            if (_result.TotalMilliseconds > milliseconds)
                throw new AssertionException(new NotExceedTotalMillisecondsAssertionMessage(milliseconds, _result.TotalMilliseconds, description).Generate(CurrentCulture));
        }
        public void NotExceedTotalSeconds(Int32 seconds, string description = null)
        {
            if (_result.TotalSeconds > seconds)
                throw new AssertionException(new NotExceedTotalSecondsAssertionMessage(seconds, _result.TotalSeconds, description).Generate(CurrentCulture));
        }
        public void NotExceedTotalMinutes(Int32 minutes, string description = null)
        {
            if (_result.TotalMinutes > minutes)
                throw new AssertionException(new NotExceedTotalMinutesAssertionMessage(minutes, _result.TotalMinutes, description).Generate(CurrentCulture));
        }
        public void NotExceedTotalTime(TimeSpan time, string description = null)
        {
            if (_result.ElapsedTime > time)
                throw new AssertionException(new NotExceedTotalTimeAssertionMessage(time, _result.ElapsedTime, description).Generate(CurrentCulture));
        }
        // Assertions on averages.
        public void NotExceedAverageTicks(Int64 ticks, string description = null)
        {
            if (_result.AverageTicks > ticks)
                throw new AssertionException(new NotExceedAverageTicksAssertionMessage(ticks, _result.AverageTicks, description).Generate(CurrentCulture));
        }
        public void NotExceedAverageMilliseconds(Int64 milliseconds, string description = null)
        {
            if (_result.AverageMilliseconds > milliseconds)
                throw new AssertionException(new NotExceedAverageMillisecondsAssertionMessage(milliseconds, _result.AverageMilliseconds, description).Generate(CurrentCulture));
        }
        public void NotExceedAverageSeconds(Int32 seconds, string description = null)
        {
            if (_result.AverageSeconds > seconds)
                throw new AssertionException(new NotExceedAverageSecondsAssertionMessage(seconds, _result.AverageSeconds, description).Generate(CurrentCulture));
        }
        public void NotExceedAverageMinutes(Int32 minutes, string description = null)
        {
            if (_result.AverageMinutes > minutes)
                throw new AssertionException(new NotExceedAverageMinutesAssertionMessage(minutes, _result.AverageMinutes, description).Generate(CurrentCulture));
        }
    }
}
