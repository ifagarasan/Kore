using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kore.Code.Sort;

namespace Kore.Code.Tests.Sort
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
