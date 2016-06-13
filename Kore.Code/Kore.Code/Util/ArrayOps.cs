using System;
using Kore.Code.Validation;
using Kore.Validation;

namespace Kore.Code.Util
{
    public static class ArrayOps<T> where T : IComparable
    {
        #region Search

        public static bool ArrayContains(T[] input, T value)
        {
            ArrayValidation<T>.ValidateArray(input);

            int leftIndex = 0, rightIndex = input.Length - 1;

            while (leftIndex <= rightIndex)
            {
                var middle = (leftIndex + rightIndex)/2;
                var comparisonResult = value.CompareTo(input[middle]);

                if (comparisonResult == 0)
                    return true;
                if (comparisonResult < 0)
                    rightIndex = middle - 1;
                else
                    leftIndex = middle + 1;
            }

            return false;
        }

        #endregion

        #region Extreme

        public static int GetMinimumIndex(T[] input)
        {
            return GetMinimumIndex(input, 0);
        }

        public static int GetMinimumIndex(T[] input, int startIndex)
        {
            return GetExtremeIndex(input, startIndex, Comparers.Comparer<T>.SmallerThan);
        }

        public static int GetMaximumIndex(T[] input)
        {
            return GetMaximumIndex(input, 0);
        }

        public static int GetMaximumIndex(T[] input, int startIndex)
        {
            return GetExtremeIndex(input, startIndex, Comparers.Comparer<T>.LargerThan);
        }

        public static int GetExtremeIndex(T[] input, Func<T, T, bool> func)
        {
            return GetExtremeIndex(input, 0, func);
        }

        public static int GetExtremeIndex(T[] input, int startIndex, Func<T, T, bool> func)
        {
            var extremeIndex = startIndex;

            for (var i = startIndex + 1; i < input.Length; ++i)
                if (func(input[i], input[extremeIndex]))
                    extremeIndex = i;

            return extremeIndex;
        }

        #endregion

        #region Copy

        public static void CopyArray(T[] source, T[] target)
        {
            ArrayValidation<T>.ValidateArray(source);
            ArrayValidation<T>.ValidateArray(target);

            CopyArray(source, 0, source.Length, target);
        }

        public static void CopyArray(T[] source, int sourceStartIndex, T[] target, int targetStartIndex)
        {
            ArrayValidation<T>.ValidateArrayIndex(source, sourceStartIndex);

            CopyArray(source, sourceStartIndex, source.Length - sourceStartIndex, target, targetStartIndex);
        }

        public static void CopyArray(T[] source, int sourceStartIndex, int sourceCount, T[] target)
        {
            CopyArray(source, sourceStartIndex, sourceCount, target, 0);
        }

        public static void CopyArray(T[] source, int sourceStartIndex, int sourceCount, T[] target, int targetStartIndex)
        {
            ArrayValidation<T>.ValidateArrayIndex(source, sourceStartIndex);
            ComparisonValidation<int>.IsSmallerThanOrEqualTo(sourceStartIndex + sourceCount, source.Length);

            ArrayValidation<T>.ValidateArrayIndex(target, targetStartIndex);
            ComparisonValidation<int>.IsSmallerThanOrEqualTo(targetStartIndex + sourceCount, target.Length);

            for (var i = 0; i < sourceCount; ++i)
                target[targetStartIndex + i] = source[sourceStartIndex + i];
        }

        #endregion

        #region Merge

        public static T[] Merge(T[] input1, T[] input2, Func<T, T, bool> comparisonFunc)
        {
            ArrayValidation<T>.ValidateArray(input1);
            ArrayValidation<T>.ValidateArray(input2);

            var totalLength = input1.Length + input2.Length;
            var result = new T[totalLength];

            Merge(input1, input2, result, 0, comparisonFunc);

            return result;
        }

        public static T[] Merge(T[] input1, T[] input2, T[] result, Func<T, T, bool> comparisonFunc)
        {
            return Merge(input1, input2, result, 0, comparisonFunc);
        }

        public static T[] Merge(T[] input, int start1, int length1, int start2, int length2,
            Func<T, T, bool> comparisonFunc)
        {
            ComparisonValidation<int>.IsSmallerThanOrEqualTo(start1, start2);

            ArrayValidation<T>.ValidateArrayIndex(input, start1);
            ComparisonValidation<int>.IsLargerThan(length1, 0);
            ComparisonValidation<int>.IsSmallerThanOrEqualTo(start1 + length1, input.Length);

            ArrayValidation<T>.ValidateArrayIndex(input, start2);
            ComparisonValidation<int>.IsLargerThan(length2, 0);
            ComparisonValidation<int>.IsSmallerThanOrEqualTo(start2 + length2, input.Length);

            var input1 = new T[length1];
            CopyArray(input, start1, length1, input1);

            var input2 = new T[length2];
            CopyArray(input, start2, length2, input2);

            Merge(input1, input2, input, start1, comparisonFunc);

            return input;
        }

        public static T[] Merge(T[] input1, T[] input2, T[] result, int resultStartIndex,
            Func<T, T, bool> comparisonFunc)
        {
            ArrayValidation<T>.ValidateArray(input1);
            ArrayValidation<T>.ValidateArray(input2);
            ArrayValidation<T>.ValidateArrayIndex(result, resultStartIndex);

            ComparisonValidation<int>.IsSmallerThanOrEqualTo(resultStartIndex + input1.Length + input2.Length,
                result.Length);

            int input1Index = 0, input2Index = 0;
            var resultIndex = resultStartIndex;

            while (input1Index < input1.Length && input2Index < input2.Length)
            {
                if (comparisonFunc(input1[input1Index], input2[input2Index]))
                    result[resultIndex++] = input1[input1Index++];
                else
                    result[resultIndex++] = input2[input2Index++];
            }

            var diff = input1.Length - input1Index;
            if (diff > 0)
                CopyArray(input1, input1Index, result, resultIndex);

            resultIndex += diff;

            diff = input2.Length - input2Index;
            if (diff > 0)
                CopyArray(input2, input2Index, result, resultIndex);

            return result;
        }

        #endregion
    }
}