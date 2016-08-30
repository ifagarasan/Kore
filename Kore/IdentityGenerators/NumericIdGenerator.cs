using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kore.IdentityGenerators
{
    public class NumericIdGenerator
    {
        private long _currentValue = 1;

        public long Generate()
        {
            return _currentValue++;
        }
    }
}
