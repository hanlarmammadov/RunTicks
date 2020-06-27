using System;
using System.Globalization;
using System.Text;

namespace RunTicks.AssertionMessages
{
    public class NotExceedTotalSecondsAssertionMessage : AssertionMessageBase
    {
        private readonly Int64 _expectedMaxSeconds;
        private readonly Double _actualSeconds;

        public NotExceedTotalSecondsAssertionMessage(Int64 expectedMaxSeconds, Double actualSeconds, string description)
            :base(description)
        {
            _expectedMaxSeconds = expectedMaxSeconds;
            _actualSeconds = actualSeconds;
        }

        internal Int64 ExpectedMaxSeconds => _expectedMaxSeconds;
        internal Double ActualSeconds => _actualSeconds;

        internal override void AddSpecificMessagePart(StringBuilder stringBuilder, CultureInfo culture)
        {
            stringBuilder.AppendLine($"Expected total seconds not to exceed {_expectedMaxSeconds.ToString(culture)}, but had: {_actualSeconds.ToString(culture)} total seconds.");
        }
    }
}
