using System;
using System.Globalization;
using System.Text;

namespace RunTicks.AssertionMessages
{
    public class NotExceedsTicksAssertionMessage : AssertionMessageBase
    {
        private readonly Int64 _expectedMaxTicks;
        private readonly Int64 _actualTicks;

        public NotExceedsTicksAssertionMessage(Int64 expectedMaxTicks, Int64 actualTicks, string description)
            :base(description)
        {
            _expectedMaxTicks = expectedMaxTicks;
            _actualTicks = actualTicks;
        }

        internal Int64 ExpectedMaxTicks => _expectedMaxTicks;
        internal Int64 ActualTicks => _actualTicks;

        internal override void AddSpecificMessagePart(StringBuilder stringBuilder, CultureInfo culture)
        {
            stringBuilder.AppendLine($"Expected total ticks not to exceed {_expectedMaxTicks.ToString()}, but had: {_actualTicks.ToString()} total ticks.");
        }
    }
}
