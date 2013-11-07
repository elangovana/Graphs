using System;

namespace AE.Graphs.Library.Exceptions
{
    public class DuplicateEdgeException : ApplicationException
    {
        public DuplicateEdgeException()
        {
        }

        public DuplicateEdgeException(string message) : base(message)
        {
        }

        public DuplicateEdgeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}