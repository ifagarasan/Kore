﻿using System;
using Kore.Code.Util;
using Kore.Code.Validation;

namespace Kore.Code.Sort
{
    public static class SelectionSort<T> where T : IComparable
    {
        public static void Sort(T[] input, SortDirection direction = SortDirection.Ascending)
        {
            ArrayValidation<T>.ValidateArray(input);

            var comparisonFunc = Sort<T>.GetComparisonFunc(direction);

            for (var i = 0; i < input.Length; ++i)
            {
                var extremeIndex = ArrayOps<T>.GetExtremeIndex(input, i, comparisonFunc);

                if (i != extremeIndex)
                    Exchange<T>.ArrayExchange(input, i, extremeIndex);
            }
        }
    }
}