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
        public void NotExceedsTicks(Int64 ticks, string description = null)
        {
            if (_result.ElapsedTicks > ticks)
                throw new AssertionException(new NotExceedsTicksAssertionMessage(ticks, _result.ElapsedTicks, description).Generate(CurrentCulture));
        }
        public void NotExceedsMilliseconds(Int64 milliseconds, string description = null)
        {
            if (_result.Milliseconds > milliseconds)
                throw new AssertionException(new NotExceedsMillisecondsAssertionMessage(milliseconds, _result.Milliseconds, description).Generate(CurrentCulture));
        }
        public void NotExceedsSeconds(Int32 seconds, string description = null)
        {
            if (_result.Seconds > seconds)
                throw new AssertionException(new NotExceedsSecondsAssertionMessage(seconds, _result.Seconds, description).Generate(CurrentCulture));
        }
        public void NotExceedsMinutes(Int32 minutes, string description = null)
        {
            if (_result.Minutes > minutes)
                throw new AssertionException(new NotExceedsMinutesAssertionMessage(minutes, _result.Minutes, description).Generate(CurrentCulture));
        }
        public void NotExceedsTime(TimeSpan time, string description = null)
        {
            if (_result.ElapsedTime > time)
                throw new AssertionException(new NotExceedsTimeAssertionMessage(time, _result.ElapsedTime, description).Generate(CurrentCulture));
        }
        // Assertions on averages.
        public void NotExceedsAverageTicks(Int64 ticks, string description = null)
        {
            if (_result.AverageTicks > ticks)
                throw new AssertionException(new NotExceedsAverageTicksAssertionMessage(ticks, _result.AverageTicks, description).Generate(CurrentCulture));
        }
        public void NotExceedsAverageMilliseconds(Int64 milliseconds, string description = null)
        {
            if (_result.AverageMilliseconds > milliseconds)
                throw new AssertionException(new NotExceedsAverageMillisecondsAssertionMessage(milliseconds, _result.AverageMilliseconds, description).Generate(CurrentCulture));
        }
        public void NotExceedsAverageSeconds(Int32 seconds, string description = null)
        {
            if (_result.AverageSeconds > seconds)
                throw new AssertionException(new NotExceedsAverageSecondsAssertionMessage(seconds, _result.AverageSeconds, description).Generate(CurrentCulture));
        }
        public void NotExceedsAverageMinutes(Int32 minutes, string description = null)
        {
            if (_result.AverageMinutes > minutes)
                throw new AssertionException(new NotExceedsAverageMinutesAssertionMessage(minutes, _result.AverageMinutes, description).Generate(CurrentCulture));
        }
    }
}
