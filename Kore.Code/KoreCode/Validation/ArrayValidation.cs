using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KoreCode.Exceptions;

namespace KoreCode.Validation
{
    public static class ArrayValidation<T>
    {
        #region SingleDimensionalArrays

        public static void ValidateArray(T[] input, bool allowEmpty = true)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            if (!allowEmpty && input.Length == 0)
                throw new CollectionEmptyException("collection empty");
        }

        public static void ValidateArrayIndex(T[] input, int index)
        {
            ValidateArray(input);

            if (index < 0 || index >= input.Length)
                throw new IndexOutOfRangeException("invalid value '" + index.ToString() + "' for index");
        }

        #endregion
    }
}
