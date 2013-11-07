using System;

namespace AE.Graphs.Core.Exceptions
{
    public class EdgeNotFoundException : ApplicationException
    {
        public EdgeNotFoundException()
        {
        }

        public EdgeNotFoundException(string message) : base(message)
        {
        }

        public EdgeNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}