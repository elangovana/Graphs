using System;

namespace AE.Graphs.Core.Exceptions
{
    public class NodeNotFoundException : ApplicationException
    {
        public NodeNotFoundException()
        {
        }

        public NodeNotFoundException(string message)
            : base(message)
        {
        }

        public NodeNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}