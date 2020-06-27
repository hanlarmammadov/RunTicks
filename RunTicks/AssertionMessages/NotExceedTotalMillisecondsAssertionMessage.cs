using System;
using System.Globalization;
using System.Text;

namespace RunTicks.AssertionMessages
{
    public class NotExceedTotalMillisecondsAssertionMessage : AssertionMessageBase
    {
        private readonly Int64 _expectedMaxMilliseconds;
        private readonly Double _actualMilliseconds;

        public NotExceedTotalMillisecondsAssertionMessage(Int64 expectedMaxMilliseconds, Double actualMilliseconds, string description)
            :base(description)
        {
            _expectedMaxMilliseconds = expectedMaxMilliseconds;
            _actualMilliseconds = actualMilliseconds;
        }

        internal Int64 ExpectedMaxMilliseconds => _expectedMaxMilliseconds;
        internal Double ActualMilliseconds => _actualMilliseconds;

        internal override void AddSpecificMessagePart(StringBuilder stringBuilder, CultureInfo culture)
        {
            stringBuilder.AppendLine($"Expected total milliseconds not to exceed {_expectedMaxMilliseconds.ToString(culture)}, but had: {_actualMilliseconds.ToString(culture)} total milliseconds.");
        }
    }
}
