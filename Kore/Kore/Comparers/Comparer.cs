using System;
using Kore.Comparers;
using System.Collections.Generic;

namespace Kore
{
    public class Comparer<T> where T : IComparable
    {
        static Dictionary<int, ComparisonResult> _comparisonResults;

        static Comparer()
        {
            _comparisonResults = new Dictionary<int, ComparisonResult>();
            _comparisonResults.Add(-1, ComparisonResult.SmallerThan);
            _comparisonResults.Add(0, ComparisonResult.Equal);
            _comparisonResults.Add(1, ComparisonResult.LargerThan);
        }

        public static ComparisonResult Compare(T value1, T value2)
        {
            int comparisonResult = value1.CompareTo(value2);

            return _comparisonResults[comparisonResult];
        }

        public static bool LargerThan(T value1, T value2)
        {
            return Compare(value1, value2) == ComparisonResult.LargerThan;
        }

        public static bool Equal(T value1, T value2)
        {
            return Compare(value1, value2) == ComparisonResult.Equal;
        }

        public static bool SmallerThan(T value1, T value2)
        {
            return Compare(value1, value2) == ComparisonResult.SmallerThan;
        }
    }
}