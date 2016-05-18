using System;

namespace Kore.Code.Util
{
    public static class Comparers<T> where T : IComparable
    {
        public static bool LessThan(T input1, T input2)
        {
            return input1.CompareTo(input2) < 0;
        }

        public static bool Equal(T input1, T input2)
        {
            return input1.CompareTo(input2) == 0;
        }

        public static bool LargerThan(T input1, T input2)
        {
            return input1.CompareTo(input2) > 0;
        }
    }
}