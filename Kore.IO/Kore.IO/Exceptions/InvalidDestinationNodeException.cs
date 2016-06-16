using System;

namespace Kore.IO.Exceptions
{
    public class InvalidDestinationNodeException: Exception
    {
        public InvalidDestinationNodeException() { }

        public InvalidDestinationNodeException(string message): base(message) { }
    }
}