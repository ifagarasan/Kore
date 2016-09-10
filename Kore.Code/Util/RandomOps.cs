using System.Collections.Generic;
using Kore.Validation;

namespace Kore.Code.Util
{
    public static class RandomOps
    {
        public static int[] GetContiguousRandomSequence(int startValue, int count)
        {
            ComparisonValidation<int>.IsLargerThan(count, 0);

            var items = new List<int>();

            for (var i = startValue; i < count; ++i)
                items.Add(i + startValue);

            var result = new int[count];
            int resultIndex = 0, index;

            var random = new System.Random();
            while (items.Count > 0)
            {
                index = random.Next(items.Count);
                result[resultIndex++] = items[index];
                items.RemoveAt(index);
            }

            return result;
        }
    }
}