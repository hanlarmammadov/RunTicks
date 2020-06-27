using System;
using System.Globalization;
using System.Text;

namespace RunTicks.AssertionMessages
{
    public class NotExceedTotalTimeAssertionMessage : AssertionMessageBase
    {
        private readonly TimeSpan _expectedMaxTimespan;
        private readonly TimeSpan _actualTimespan;

        public NotExceedTotalTimeAssertionMessage(TimeSpan expectedMaxTimespan, TimeSpan actualTimespan, string description)
            :base(description)
        {
            _expectedMaxTimespan = expectedMaxTimespan;
            _actualTimespan = actualTimespan;
        }

        internal TimeSpan ExpectedMaxTimespan => _expectedMaxTimespan;
        internal TimeSpan ActualTimespan => _actualTimespan;

        internal override void AddSpecificMessagePart(StringBuilder stringBuilder, CultureInfo culture)
        {
            stringBuilder.AppendLine($"Expected total time span not to exceed {_expectedMaxTimespan.ToString("c")}, but had: {_actualTimespan.ToString("c")} total time span.");
        }
    }
}
