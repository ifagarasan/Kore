using System;
using Kore.Comparers;
using Kore.Code.Util;
using Kore.Exceptions;

namespace Kore.Code.Validation
{
    public static class ComparisonValidation<T> where T : IComparable
    {
        public static void IsSmallerThan(T input1, T input2)
        {
            if (Comparer<T>.Compare(input1, input2) != ComparisonResult.SmallerThan)
                throw new ComparisonException(input1.ToString(), input2.ToString(), "less than");
        }

        public static void IsSmallerThanOrEqualTo(T input1, T input2)
        {
            if (Comparer<T>.Compare(input1, input2) != ComparisonResult.SmallerThan &&
                Comparer<T>.Compare(input1, input2) != ComparisonResult.Equal)
                throw new ComparisonException(input1.ToString(), input2.ToString(), "less than or equal to");
        }

        public static void IsEqualTo(T input1, T input2)
        {
            if (Comparer<T>.Compare(input1, input2) != ComparisonResult.Equal)
                throw new ComparisonException(input1.ToString(), input2.ToString(), "equal to");
        }

        public static void IsLargerThan(T input1, T input2)
        {
            if (Comparer<T>.Compare(input1, input2) != ComparisonResult.LargerThan)
                throw new ComparisonException(input1.ToString(), input2.ToString(), "larger than");
        }

        public static void IsLargerThanOrEqualTo(T input1, T input2)
        {
            if (Comparer<T>.Compare(input1, input2) != ComparisonResult.LargerThan &&
                Comparer<T>.Compare(input1, input2) != ComparisonResult.Equal)
                throw new ComparisonException(input1.ToString(), input2.ToString(), "larger than or equal to");
        }
    }
}