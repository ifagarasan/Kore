using System;

namespace Kore.IO.Exceptions
{
    [Serializable]
    public class NodeNotFoundException : Exception
    {
        public NodeNotFoundException() { }

        public NodeNotFoundException(string message) : base(message) { }
    }
}