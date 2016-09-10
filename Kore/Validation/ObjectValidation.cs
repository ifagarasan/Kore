using Kore.Exceptions;

namespace Kore.Validation
{
    public class ObjectValidation
    {
        public static void IsNotNull(object o)
        {
            if (o == null)
                throw new NullException();
        }
    }
}