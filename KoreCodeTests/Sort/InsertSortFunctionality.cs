using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KoreCode;
using System.Collections;

using KoreCode.Sort;

namespace KoreCodeTests.Sort
{
    [TestClass]
    public class InsertSortFunctionality: SortFunctionality
    {
        public InsertSortFunctionality()
        {
            sortFunc = InsertSort<int>.Sort;
        }
    }
}
