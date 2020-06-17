using System.Globalization;
using System.Text;

namespace RunTicks.AssertionMessages
{
    public abstract class AssertionMessageBase
    {
        protected readonly string _description;

        public AssertionMessageBase(string description)
        {
            _description = description;
        }

        internal string Description => _description;

        public string Generate(CultureInfo culture)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine("--------");
            sb.AppendLine("Assertion failed.");
            AddSpecificMessagePart(sb, culture);
            if (_description != null)
                sb.AppendLine(_description);
            sb.Append("--------");
            return sb.ToString();
        }

        internal abstract void AddSpecificMessagePart(StringBuilder stringBuilder, CultureInfo culture);
    }
}
