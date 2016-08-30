using System;

namespace Kore.Exceptions
{
    public class NullException : Exception
    {
        public NullException() : base("reference is null") { }

        public NullException(string message) : base(message) { }
    }
}