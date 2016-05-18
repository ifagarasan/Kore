using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KoreCode.List;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KoreCodeTests.List
{
    [TestClass]
    public class DoubleLinkedListFunctional: LinkedListFunctional
    {
        [TestInitialize]
        public void SetUp()
        {
            list = new DoubleLinkedList<int>();
        }
    }
}
