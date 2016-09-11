using Kore.Code.List.Circular;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Code.Tests.List
{
    [TestClass]
    public class CircularLinkedListFunctional : LinkedListFunctional
    {
        [TestInitialize]
        public void SetUp()
        {
            List = new CircularList<int>();
        }
    }
}
