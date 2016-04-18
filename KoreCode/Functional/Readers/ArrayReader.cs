using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KoreCode.Util;

namespace KoreCode.Functional.Readers
{
    public static class ArrayReader
    {
        public static T[] Read<T>(StreamReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            int length = Convert.ToInt32(reader.ReadLine());
            string[] temp = reader.ReadLine().Split(' ');

            Type type = typeof(T);
            Array array = Array.CreateInstance(type, length);

            for (int i = 0; i < length; ++i)
            {
                var elementValue = Convert.ChangeType(temp[i], type);
                array.SetValue(elementValue, i);
            }

            return (T[])(object)array;
        }
    }
}
