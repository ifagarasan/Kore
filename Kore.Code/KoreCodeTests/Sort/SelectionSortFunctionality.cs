using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

using KoreCode.Sort;

namespace KoreCodeTests.Sort
{
    [TestClass]
    public class SelectionSortFunctionality : SortFunctionality
    {
        public SelectionSortFunctionality()
        {
            sortFunc = SelectionSort<int>.Sort;
        }
    }
}
