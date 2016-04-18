using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoreCode.Exceptions
{
    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException(): base ("element not found in collection") { }

        public ElementNotFoundException(string key) : base(string.Format("element '{0}' not found in collection", key)) { }
    }
}
