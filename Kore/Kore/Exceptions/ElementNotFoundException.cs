using System;

namespace Kore.Exceptions
{
    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException() : base("element not found in collection")
        {
        }

        public ElementNotFoundException(string key) : base($"element '{key}' not found in collection")
        {
        }
    }
}