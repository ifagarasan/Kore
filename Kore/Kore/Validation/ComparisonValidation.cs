using Kore.Comparers;
using Kore.Exceptions;
using System;

namespace Kore.Validation
{
    public class ComparisonValidation<T> where T : IComparable
    {
        public static void IsEqualTo(T value1, T value2)
        {
            if (!Comparer<T>.Equal(value1, value2))
                throw new ComparisonException();
        }

        public static void IsLargerThan(T value1, T value2)
        {
            if (!Comparer<T>.LargerThan(value1, value2))
                throw new ComparisonException();
        }

        public static void IsSmallerThan(T value1, T value2)
        {
            if (!Comparer<T>.SmallerThan(value1, value2))
                throw new ComparisonException();
        }

        public static void IsSmallerThanOrEqualTo(T value1, T value2)
        {
            if (Comparer<T>.LargerThan(value1, value2))
                throw new ComparisonException();
        }

        public static void IsLargerThanOrEqualTo(T value1, T value2)
        {
            if (Comparer<T>.SmallerThan(value1, value2))
                throw new ComparisonException();
        }
    }
}