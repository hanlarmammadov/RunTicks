using System;
using System.Globalization;
using System.Text;

namespace RunTicks.AssertionMessages
{
    internal class NotExceedAverageSecondsAssertionMessage : AssertionMessageBase
    {
        private readonly Int64 _expectedMaxAverageSeconds;
        private readonly Double _actualAverageSeconds;

        public NotExceedAverageSecondsAssertionMessage(Int64 expectedMaxAverageSeconds, Double actualAverageSeconds, string description)
            :base(description)
        {
            _expectedMaxAverageSeconds = expectedMaxAverageSeconds;
            _actualAverageSeconds = actualAverageSeconds;
        }

        internal Int64 ExpectedMaxAverageSeconds => _expectedMaxAverageSeconds;
        internal Double ActualAverageSeconds => _actualAverageSeconds;

        internal override void AddSpecificMessagePart(StringBuilder stringBuilder, CultureInfo culture)
        {
            stringBuilder.AppendLine($"Expected average (per run) seconds not to exceed {_expectedMaxAverageSeconds.ToString(culture)}, but had: {_actualAverageSeconds.ToString(culture)} average seconds.");
        }
    }
}
