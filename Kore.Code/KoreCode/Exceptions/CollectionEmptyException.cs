using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoreCode.Exceptions
{
    public class CollectionEmptyException: Exception
    {
        public CollectionEmptyException(): base ("collection is empty") { }

        public CollectionEmptyException(string message) : base(message) { }
    }
}
