﻿using System;
using Kore.Code.Util;
using Kore.Code.Validation;

namespace Kore.Code.Sort
{
    public static class MergeSort<T> where T : IComparable
    {
        public static void Sort(T[] input, SortDirection direction = SortDirection.Ascending)
        {
            ArrayValidation<T>.ValidateArray(input);

            Sort(input, 0, input.Length - 1, direction);
        }

        public static void Sort(T[] input, int leftIndex, int rightIndex,
            SortDirection direction = SortDirection.Ascending)
        {
            var comparisonFunc = Sort<T>.GetComparisonFunc(direction);
            Sort(input, leftIndex, rightIndex, comparisonFunc);
        }

        private static void Sort(T[] input, int leftIndex, int rightIndex, Func<T, T, bool> comparisonFunc)
        {
            if (leftIndex >= rightIndex)
                return;

            var middle = (leftIndex + rightIndex)/2;

            Sort(input, leftIndex, middle, comparisonFunc);
            Sort(input, middle + 1, rightIndex, comparisonFunc);

            ArrayOps<T>.Merge(input, leftIndex, middle - leftIndex + 1, middle + 1, rightIndex - middle, comparisonFunc);
        }
    }
}