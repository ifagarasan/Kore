using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoreCode.Exceptions
{
    public class DuplicateKeyException : Exception
    {
        public DuplicateKeyException(): base ("duplicate key in collection") { }

        public DuplicateKeyException(string key) : base(string.Format("duplicate key {0} in collection", key)) { }
    }
}
