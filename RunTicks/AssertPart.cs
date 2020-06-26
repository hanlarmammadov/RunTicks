using RunTicks.AssertionMessages;
using System;
using System.Globalization;
using System.Threading;

namespace RunTicks
{
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
        public void NotExceedTicks(Int64 ticks, string description = null)
        {
            if (_result.ElapsedTicks > ticks)
                throw new AssertionException(new NotExceedTicksAssertionMessage(ticks, _result.ElapsedTicks, description).Generate(CurrentCulture));
        }
        public void NotExceedMilliseconds(Int64 milliseconds, string description = null)
        {
            if (_result.Milliseconds > milliseconds)
                throw new AssertionException(new NotExceedMillisecondsAssertionMessage(milliseconds, _result.Milliseconds, description).Generate(CurrentCulture));
        }
        public void NotExceedSeconds(Int32 seconds, string description = null)
        {
            if (_result.Seconds > seconds)
                throw new AssertionException(new NotExceedSecondsAssertionMessage(seconds, _result.Seconds, description).Generate(CurrentCulture));
        }
        public void NotExceedMinutes(Int32 minutes, string description = null)
        {
            if (_result.Minutes > minutes)
                throw new AssertionException(new NotExceedMinutesAssertionMessage(minutes, _result.Minutes, description).Generate(CurrentCulture));
        }
        public void NotExceedTime(TimeSpan time, string description = null)
        {
            if (_result.ElapsedTime > time)
                throw new AssertionException(new NotExceedTimeAssertionMessage(time, _result.ElapsedTime, description).Generate(CurrentCulture));
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
