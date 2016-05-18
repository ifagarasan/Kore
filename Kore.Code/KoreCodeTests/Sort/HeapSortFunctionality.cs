using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

using KoreCode.Sort;

namespace KoreCodeTests.Sort
{
    [TestClass]
    public class HeapSortFunctionality : SortFunctionality
    {
        public HeapSortFunctionality()
        {
            sortFunc = HeapSort<int>.Sort;
        }
    }
}
