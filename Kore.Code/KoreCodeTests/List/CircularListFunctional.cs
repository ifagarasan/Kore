using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KoreCode.List;
using KoreCode.List.Circular;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KoreCodeTests.List
{
    [TestClass]
    public class CircularLinkedListFunctional : LinkedListFunctional
    {
        [TestInitialize]
        public void SetUp()
        {
            list = new CircularList<int>();
        }
    }
}
