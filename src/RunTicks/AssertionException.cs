using System;

namespace RunTicks
{
    /// <summary>
    /// An exception that is thrown when the measurement assertion fails.
    /// </summary>
    public class AssertionException : Exception
    {
        public AssertionException(string message)
            : base(message)
        {

        }
        public override string ToString()
        {
            return this.Message;
        }
    }
}
