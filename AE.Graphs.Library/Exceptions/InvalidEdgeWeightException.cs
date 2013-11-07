using System;

namespace AE.Graphs.Library.Exceptions
{
    public class InvalidEdgeWeightException : ApplicationException
    {
        public InvalidEdgeWeightException()
        {
        }

        public InvalidEdgeWeightException(string message) : base(message)
        {
        }

        public InvalidEdgeWeightException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}