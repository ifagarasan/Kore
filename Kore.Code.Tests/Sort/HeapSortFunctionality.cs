using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kore.Code.Sort;

namespace Kore.Code.Tests.Sort
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
