using System;

namespace Kore.Exceptions
{
    public class CollectionEmptyException : Exception
    {
        public CollectionEmptyException() : base("collection is empty")
        {
        }

        public CollectionEmptyException(string message) : base(message)
        {
        }
    }
}