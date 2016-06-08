using System;

namespace Kore.Exceptions
{
    public class DuplicateKeyException : Exception
    {
        public DuplicateKeyException() : base("duplicate key in collection")
        {
        }

        public DuplicateKeyException(string key) : base($"duplicate key {key} in collection")
        {
        }
    }
}