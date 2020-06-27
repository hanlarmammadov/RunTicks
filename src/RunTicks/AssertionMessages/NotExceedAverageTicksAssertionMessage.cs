using System;
using System.Globalization;
using System.Text;

namespace RunTicks.AssertionMessages
{
    internal class NotExceedAverageTicksAssertionMessage : AssertionMessageBase
    {
        private readonly Int64 _expectedMaxAverageTicks;
        private readonly Double _actualAverageTicks;

        public NotExceedAverageTicksAssertionMessage(Int64 expectedMaxAverageTicks, Double actualAverageTicks, string description)
            :base(description)
        {
            _expectedMaxAverageTicks = expectedMaxAverageTicks;
            _actualAverageTicks = actualAverageTicks;
        }

        internal Int64 ExpectedMaxAverageTicks => _expectedMaxAverageTicks;
        internal Double ActualAverageTicks => _actualAverageTicks;

        internal override void AddSpecificMessagePart(StringBuilder stringBuilder, CultureInfo culture)
        {
            stringBuilder.AppendLine($"Expected average (per run) ticks not to exceed {_expectedMaxAverageTicks.ToString(culture)}, but had: {_actualAverageTicks.ToString(culture)} average ticks.");
        }
    }
}
