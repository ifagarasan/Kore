﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KoreCode.Util;

namespace KoreCode.Sort
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
            else
                return Comparers<T>.LargerThan;
        }
    }
}
