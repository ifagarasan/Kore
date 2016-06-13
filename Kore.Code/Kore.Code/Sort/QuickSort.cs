using System;
using Kore.Code.Util;
using Kore.Code.Validation;
using Kore.Validation;

namespace Kore.Code.Sort
{
    public static class QuickSort<T> where T : IComparable
    {
        public static void Sort(T[] input, SortDirection direction = SortDirection.Ascending)
        {
            ArrayValidation<T>.ValidateArray(input);

            var comparisonFunc = Sort<T>.GetComparisonFunc(direction);

            Sort(input, 0, input.Length - 1, comparisonFunc);
        }

        private static void Sort(T[] input, int left, int right, Func<T, T, bool> comparisonFunc)
        {
            if (left < right)
            {
                var pivot = Partition(input, left, right, comparisonFunc);

                Sort(input, left, pivot - 1, comparisonFunc);
                Sort(input, pivot + 1, right, comparisonFunc);
            }
        }

        public static int Partition(T[] input, int left, int right, Func<T, T, bool> comparisonFunc)
        {
            var pivotIndex = MedianOfThree(input, left, right, comparisonFunc);

            var wall = left - 1;

            for (var i = left; i < right; ++i)
            {
                if (comparisonFunc(input[i], input[pivotIndex]))
                {
                    wall++;
                    Exchange<T>.ArrayExchange(input, i, wall);
                }
            }

            Exchange<T>.ArrayExchange(input, right, wall + 1);

            return wall + 1;
        }

        public static int MedianOfThree(T[] input, int left, int right, Func<T, T, bool> comparisonFunc)
        {
            if (comparisonFunc == null)
                throw new ArgumentNullException("comparisonFunc");

            ArrayValidation<T>.ValidateArray(input);
            ArrayValidation<T>.ValidateArrayIndex(input, left);
            ArrayValidation<T>.ValidateArrayIndex(input, right);
            ComparisonValidation<int>.IsSmallerThan(left, right);

            var middle = (left + right)/2;

            if (!comparisonFunc(input[left], input[middle]))
                Exchange<T>.ArrayExchange(input, left, middle);

            if (!comparisonFunc(input[middle], input[right]))
                Exchange<T>.ArrayExchange(input, middle, right);

            return middle;
        }
    }
}