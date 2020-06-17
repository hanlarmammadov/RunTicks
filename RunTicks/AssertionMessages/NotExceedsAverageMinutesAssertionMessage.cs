using System;
using System.Globalization;
using System.Text;

namespace RunTicks.AssertionMessages
{
    public class NotExceedsAverageMinutesAssertionMessage : AssertionMessageBase
    {
        private readonly Int64 _expectedMaxAverageMinutes;
        private readonly Double _actualAverageMinutes;

        public NotExceedsAverageMinutesAssertionMessage(Int64 expectedMaxAverageMinutes, Double actualAverageMinutes, string description)
            :base(description)
        {
            _expectedMaxAverageMinutes = expectedMaxAverageMinutes;
            _actualAverageMinutes = actualAverageMinutes;
        }

        internal Int64 ExpectedMaxAverageMinutes => _expectedMaxAverageMinutes;
        internal Double ActualAverageMinutes => _actualAverageMinutes;

        internal override void AddSpecificMessagePart(StringBuilder stringBuilder, CultureInfo culture)
        {
            stringBuilder.AppendLine($"Expected average (per run) minutes not to exceed {_expectedMaxAverageMinutes.ToString(culture)}, but had: {_actualAverageMinutes.ToString(culture)} average minutes.");
        }
    }
}
