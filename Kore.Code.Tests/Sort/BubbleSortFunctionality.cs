using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kore.Code.Sort;

namespace Kore.Code.Tests.Sort
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
