using System;

namespace ProIdeas.Exceptions
{
    public enum ErrorCategory
    {
        Logic,

        UnAuthorized,

        InvalidInput,

        NotFound

    }

    public class LogicalException : Exception
    {
        public ErrorCategory Category { get; private set; }
        public LogicalException(ErrorCategory category)
        {
            Category = category;
        }
        public LogicalException(ErrorCategory category, string message) : base(message)
        {
            Category = category;
        }
        public LogicalException(ErrorCategory category, string message, Exception inner) : base(message, inner)
        {
            Category = category;
        }

    }
}
