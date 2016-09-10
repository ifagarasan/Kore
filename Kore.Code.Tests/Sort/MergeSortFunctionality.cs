using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kore.Code.Sort;

namespace Kore.Code.Tests.Sort
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
