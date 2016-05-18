using System;
using KoreCode.Util;
using KoreCode.Validation;

namespace KoreCode.Sort
{
    public static class BubbleSort<T> where T : IComparable
    {
        public static void Sort(T[] input, SortDirection direction = SortDirection.Ascending)
        {
            ArrayValidation<T>.ValidateArray(input);

            var comparisonFunc = Sort<T>.GetComparisonFunc(direction);

            var sorted = false;

            while (!sorted)
            {
                sorted = true;
                for (var i = 1; i < input.Length; ++i)
                    if (comparisonFunc(input[i], input[i - 1]))
                    {
                        sorted = false;
                        Exchange<T>.ArrayExchange(input, i, i - 1);
                    }
            }
        }
    }
}