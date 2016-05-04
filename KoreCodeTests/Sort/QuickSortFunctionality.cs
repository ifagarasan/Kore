using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

using KoreCode.Sort;

namespace KoreCodeTests.Sort
{
    [TestClass]
    public class QuickSortFunctionality : SortFunctionality
    {
        public QuickSortFunctionality()
        {
            sortFunc = SelectionSort<int>.Sort;
        }
    }
}
