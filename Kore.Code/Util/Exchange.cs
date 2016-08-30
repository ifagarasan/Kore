using Kore.Code.Validation;

namespace Kore.Code.Util
{
    public static class Exchange<T>
    {
        public static void ArrayExchange(T[] input, int sourceIndex, int destinationIndex)
        {
            ArrayValidation<T>.ValidateArrayIndex(input, sourceIndex);
            ArrayValidation<T>.ValidateArrayIndex(input, destinationIndex);

            if (sourceIndex == destinationIndex)
                return;

            var temp = input[sourceIndex];

            input[sourceIndex] = input[destinationIndex];
            input[destinationIndex] = temp;
        }
    }
}