using System;
using System.IO;

namespace Kore.Code.Functional.Readers
{
    public static class ArrayReader
    {
        public static T[] Read<T>(StreamReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            var length = Convert.ToInt32(reader.ReadLine());
            var temp = reader.ReadLine().Split(' ');

            var type = typeof(T);
            var array = Array.CreateInstance(type, length);

            for (var i = 0; i < length; ++i)
            {
                var elementValue = Convert.ChangeType(temp[i], type);
                array.SetValue(elementValue, i);
            }

            return (T[]) array;
        }
    }
}