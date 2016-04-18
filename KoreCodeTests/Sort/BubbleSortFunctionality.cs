using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KoreCode;
using KoreCode.Util;
using System.Collections;
using System.Collections.Generic;

using KoreCode.Sort;

namespace KoreCodeTests.Sort
{
    [TestClass]
    public class BubbleSortFunctionality : SortFunctionality
    {
        public BubbleSortFunctionality()
        {
            sortFunc = BubbleSort<int>.Sort;
        }
    }
}
