using System;

namespace Kore.Code.Sort
{
    public static class Sort<T> where T : IComparable
    {
        public static Func<T, T, bool> GetComparisonFunc(SortDirection direction)
        {
            if (direction == SortDirection.Ascending)
                return Comparers.Comparer<T>.SmallerThan;
            return Comparers.Comparer<T>.LargerThan;
        }
    }
}