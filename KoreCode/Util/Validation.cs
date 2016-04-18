using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KoreCode.Util
{
    public static class Validation<T> where T : IComparable
    {
        #region Comparisons

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

        #endregion

        #region SingleDimensionalArrays

        public static void ValidateArray(T[] input, bool allowEmpty = true)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            if (!allowEmpty && input.Length == 0)
                throw new Exception("collection empty");
        }

        public static void ValidateArrayIndex(T[] input, int index)
        {
            ValidateArray(input);

            if (index < 0 || index >= input.Length)
                throw new IndexOutOfRangeException("invalid value '" + index.ToString() + "' for index");
        }

        #endregion
    }
}
