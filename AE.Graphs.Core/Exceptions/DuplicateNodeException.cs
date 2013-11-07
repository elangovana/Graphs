using System;

namespace AE.Graphs.Core.Exceptions
{
    public class DuplicateNodeException : ApplicationException
    {
        public DuplicateNodeException()
        {
        }

        public DuplicateNodeException(string message) : base(message)
        {
        }

        public DuplicateNodeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}