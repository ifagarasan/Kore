using System;
using System.IO;

namespace KoreCode.Functional.Readers
{
    public static class MatrixReader
    {
        public static T[,] Read<T>(StreamReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            var type = typeof(T);
            var temp = reader.ReadLine().Split(' ');
            var M = Convert.ToInt32(temp[0]);
            var N = Convert.ToInt32(temp[1]);

            var array = Array.CreateInstance(type, new[] {M, N});

            for (var i = 0; i < M; ++i)
            {
                temp = reader.ReadLine().Split(' ');

                for (var j = 0; j < N; ++j)
                {
                    var elementValue = Convert.ChangeType(temp[j], type);
                    array.SetValue(elementValue, new[] {i, j});
                }
            }

            return (T[,]) array;
        }
    }
}