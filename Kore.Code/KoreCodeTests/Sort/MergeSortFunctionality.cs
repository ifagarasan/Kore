using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

using KoreCode.Sort;

namespace KoreCodeTests.Sort
{
    [TestClass]
    public class MergeSortFunctionality : SortFunctionality
    {
        public MergeSortFunctionality()
        {
            sortFunc = MergeSort<int>.Sort;
        }
    }
}
