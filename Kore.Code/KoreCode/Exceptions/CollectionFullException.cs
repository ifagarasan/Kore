﻿using System;

namespace KoreCode.Exceptions
{
    public class CollectionFullException : Exception
    {
        public CollectionFullException() : base("collection is full")
        {
        }

        public CollectionFullException(string message) : base(message)
        {
        }
    }
}