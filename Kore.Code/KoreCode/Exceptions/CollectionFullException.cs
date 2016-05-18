using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoreCode.Exceptions
{
    public class CollectionFullException : Exception
    {
        public CollectionFullException(): base ("collection is full") { }

        public CollectionFullException(string message): base(message) { }
    }
}
