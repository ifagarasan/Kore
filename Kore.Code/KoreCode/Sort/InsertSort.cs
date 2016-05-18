using System;
using KoreCode.Validation;

namespace KoreCode.Sort
{
    public static class InsertSort<T> where T : IComparable
    {
        public static void Sort(T[] input, SortDirection direction = SortDirection.Ascending)
        {
            ArrayValidation<T>.ValidateArray(input);

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
