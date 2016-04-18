using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KoreCode.Util;

namespace KoreCode.Sort
{
    public static class BubbleSort<T> where T : IComparable
    {
        public static void Sort(T[] input, SortDirection direction = SortDirection.Ascending)
        {
            Validation<T>.ValidateArray(input);

            Func<T, T, bool> comparisonFunc = Sort<T>.GetComparisonFunc(direction);

            bool sorted = false;

            while (!sorted)
            {
                sorted = true;
                for (int i = 1; i < input.Length; ++i)
                    if (comparisonFunc(input[i], input[i - 1]))
                    {
                        sorted = false;
                        ArrayOps<T>.Exchange(input, i, i - 1);
                    }                        
            }       
        }
    }
}
