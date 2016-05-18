using System;

namespace KoreCode.Exceptions
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