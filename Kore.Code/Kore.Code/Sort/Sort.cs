using System;
using Kore.Code.Util;

namespace Kore.Code.Sort
{
    public enum SortDirection
    {
        Ascending = 0,
        Descending
    }

    public static class Sort<T> where T : IComparable
    {
        public static Func<T, T, bool> GetComparisonFunc(SortDirection direction)
        {
            if (direction == SortDirection.Ascending)
                return Comparers<T>.LessThan;
            return Comparers<T>.LargerThan;
        }
    }
}