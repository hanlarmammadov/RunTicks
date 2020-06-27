using System;
using System.Globalization;
using System.Text;

namespace RunTicks.AssertionMessages
{
    internal class NotExceedTotalMinutesAssertionMessage : AssertionMessageBase
    {
        private readonly Int64 _expectedMaxMinutes;
        private readonly Double _actualMinutes;

        public NotExceedTotalMinutesAssertionMessage(Int64 expectedMaxMinutes, Double actualMinutes, string description)
            :base(description)
        {
            _expectedMaxMinutes = expectedMaxMinutes;
            _actualMinutes = actualMinutes;
        }

        internal Int64 ExpectedMaxMinutes => _expectedMaxMinutes;
        internal Double ActualMinutes => _actualMinutes;

        internal override void AddSpecificMessagePart(StringBuilder stringBuilder, CultureInfo culture)
        {
            stringBuilder.AppendLine($"Expected total minutes not to exceed {_expectedMaxMinutes.ToString(culture)}, but had: {_actualMinutes.ToString(culture)} total minutes.");
        }
    }
}
