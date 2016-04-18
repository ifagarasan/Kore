using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KoreCode.Util;

namespace KoreCode.Sort
{
    public static class InsertSort<T> where T : IComparable
    {
        public static void Sort(T[] input, SortDirection direction = SortDirection.Ascending)
        {
            Validation<T>.ValidateArray(input);

            Func<T, T, bool> comparisonFunc = Sort<T>.GetComparisonFunc(direction);

            for (int i = 0; i < input.Length; ++i)
            {
                T currentValue = input[i];

                int k = i;
                while (k > 0 && comparisonFunc(currentValue, input[k - 1]))
                {
                    input[k] = input[k - 1];
                    k--;
                }

                input[k] = currentValue;
            }
        }
    }
}
