using Kore.Comparers;
using Kore.Exceptions;
using System;
using System.CodeDom;

namespace Kore.Validation
{
    public class ObjectValidation
    {
        public static void IsNotNull(object o, string argumentName=null)
        {
            if (o == null)
                throw new ArgumentNullException(argumentName ?? nameof(o));
        }
    }
}