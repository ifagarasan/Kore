using System;
using KoreCode.Exceptions;
using KoreCode.Util;

namespace KoreCode.Validation
{
    public static class ComparisonValidation<T> where T : IComparable
    {
        public static void IsSmallerThan(T input1, T input2)
        {
            if (!Comparers<T>.LessThan(input1, input2))
                throw new ComparisonException(input1.ToString(), input2.ToString(), "less than");
        }

        public static void IsSmallerThanOrEqualTo(T input1, T input2)
        {
            if (!Comparers<T>.LessThan(input1, input2) && !Comparers<T>.Equal(input1, input2))
                throw new ComparisonException(input1.ToString(), input2.ToString(), "less than or equal to");
        }

        public static void IsEqualTo(T input1, T input2)
        {
            if (!Comparers<T>.Equal(input1, input2))
                throw new ComparisonException(input1.ToString(), input2.ToString(), "equal to");
        }

        public static void IsLargerThan(T input1, T input2)
        {
            if (!Comparers<T>.LargerThan(input1, input2))
                throw new ComparisonException(input1.ToString(), input2.ToString(), "larger than");
        }

        public static void IsLargerThanOrEqualTo(T input1, T input2)
        {
            if (!Comparers<T>.LargerThan(input1, input2) && !Comparers<T>.Equal(input1, input2))
                throw new ComparisonException(input1.ToString(), input2.ToString(), "larger than or equal to");
        }
    }
}