using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KoreCode.Util;
using KoreCode.Validation;

namespace KoreCode.Sort
{
    public static class MergeSort<T> where T : IComparable
    {
        public static void Sort(T[] input, SortDirection direction = SortDirection.Ascending)
        {
            ArrayValidation<T>.ValidateArray(input);

            Sort(input, 0, input.Length - 1, direction);
        }

        public static void Sort(T[] input, int leftIndex, int rightIndex, SortDirection direction = SortDirection.Ascending)
        {
            Func<T, T, bool> comparisonFunc = Sort<T>.GetComparisonFunc(direction);
            Sort(input, leftIndex, rightIndex, comparisonFunc);
        }

        private static void Sort(T[] input, int leftIndex, int rightIndex, Func<T, T, bool> comparisonFunc)
        {
            if (leftIndex >= rightIndex)
                return;

            int middle = (leftIndex + rightIndex) / 2;

            Sort(input, leftIndex, middle, comparisonFunc);
            Sort(input, middle + 1, rightIndex, comparisonFunc);

            ArrayOps<T>.Merge(input, leftIndex, middle - leftIndex + 1, middle + 1, rightIndex - middle, comparisonFunc);
        }
    }
}
