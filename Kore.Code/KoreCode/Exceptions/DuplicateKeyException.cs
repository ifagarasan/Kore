using System;

namespace KoreCode.Exceptions
{
    public class DuplicateKeyException : Exception
    {
        public DuplicateKeyException() : base("duplicate key in collection")
        {
        }

        public DuplicateKeyException(string key) : base(string.Format("duplicate key {0} in collection", key))
        {
        }
    }
}