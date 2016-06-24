using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kore.Logic.IdentityGenerators
{
    public static class NumericIdGenerator
    {
        private static long _currentValue = 1;

        public static long Generate()
        {
            return _currentValue++;
        }
    }
}
