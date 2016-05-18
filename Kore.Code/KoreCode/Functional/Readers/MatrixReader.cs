using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KoreCode.Util;

namespace KoreCode.Functional.Readers
{
    public static class MatrixReader
    {
        public static T[,] Read<T>(StreamReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            Type type = typeof(T);
            string[] temp = reader.ReadLine().Split(' ');
            int M = Convert.ToInt32(temp[0]);
            int N = Convert.ToInt32(temp[1]);

            Array array = Array.CreateInstance(type, new[] { M, N });

            for (int i = 0; i < M; ++i)
            {
                temp = reader.ReadLine().Split(' ');

                for (int j = 0; j < N; ++j)
                {
                    var elementValue = Convert.ChangeType(temp[j], type);
                    array.SetValue(elementValue, new[] {i, j});
                }
            }

            return (T[,])(object)array;
        }
    }
}
