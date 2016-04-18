using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoreCode.Util
{
    public static class RandomOps
    {
        public static int[] GetContiguousRandomSequence(int startValue, int count)
        {
            Validation<int>.IsLargerThan(count, 0);

            List<int> items = new List<int>();

            for (int i = startValue; i < count; ++i)
                items.Add(i + startValue);

            int[] result = new int[count];
            int resultIndex = 0, index;

            System.Random random = new System.Random();
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
