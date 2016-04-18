using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KoreCode.Util;

namespace KoreCode.Sort
{
    public static class SelectionSort<T> where T : IComparable
    {
        public static void Sort(T[] input, SortDirection direction = SortDirection.Ascending)
        {
            Validation<T>.ValidateArray(input);

            Func<T, T, bool> comparisonFunc = Sort<T>.GetComparisonFunc(direction);

            for (int i = 0; i < input.Length; ++i)
            {
                int extremeIndex = ArrayOps<T>.GetExtremeIndex(input, i, comparisonFunc);

                if (i != extremeIndex)
                    ArrayOps<T>.Exchange(input, i, extremeIndex);
            }
        }
    }
}
