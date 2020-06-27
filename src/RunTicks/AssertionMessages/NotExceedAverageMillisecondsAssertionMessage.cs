using System;
using System.Globalization;
using System.Text;

namespace RunTicks.AssertionMessages
{
    internal class NotExceedAverageMillisecondsAssertionMessage : AssertionMessageBase
    {
        private readonly Int64 _expectedMaxAverageMilliseconds;
        private readonly Double _actualAverageMilliseconds;

        public NotExceedAverageMillisecondsAssertionMessage(Int64 expectedMaxAverageMilliseconds, Double actualAverageMilliseconds, string description)
            :base(description)
        {
            _expectedMaxAverageMilliseconds = expectedMaxAverageMilliseconds;
            _actualAverageMilliseconds = actualAverageMilliseconds;
        }

        internal Int64 ExpectedMaxAverageMilliseconds => _expectedMaxAverageMilliseconds;
        internal Double ActualAverageMilliseconds => _actualAverageMilliseconds;

        internal override void AddSpecificMessagePart(StringBuilder stringBuilder, CultureInfo culture)
        {
            stringBuilder.AppendLine($"Expected average (per run) milliseconds not to exceed {_expectedMaxAverageMilliseconds.ToString(culture)}, but had: {_actualAverageMilliseconds.ToString(culture)} average milliseconds.");
        }
    }
}
