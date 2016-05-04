using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KoreCode.Util;
using KoreCode.Validation;

namespace KoreCode.Sort
{
    public static class QuickSort<T> where T : IComparable
    {
        public static void Sort(T[] input, SortDirection direction = SortDirection.Ascending)
        {
            ArrayValidation<T>.ValidateArray(input);

            Func<T, T, bool> comparisonFunc = Sort<T>.GetComparisonFunc(direction);

            Sort(input, 0, input.Length - 1, comparisonFunc);
        }

        private static void Sort(T[] input, int left, int right, Func<T, T, bool> comparisonFunc)
        {
            if (left < right)
            {
                int pivot = Partition(input, left, right, comparisonFunc);

                Sort(input, left, pivot - 1, comparisonFunc);
                Sort(input, pivot + 1, right, comparisonFunc);
            }
        }

        private static int Partition(T[] input, int left, int right, Func<T, T, bool> comparisonFunc)
        {
            int pivotIndex = new Random().Next(left, right + 1);

            Exchange<T>.ArrayExchange(input, pivotIndex, right);

            pivotIndex = right;

            int wall = left - 1;

            for (int i = left; i < right; ++i)
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
    }
}
