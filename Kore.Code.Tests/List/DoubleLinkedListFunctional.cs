using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Kore.Code.List;
using Kore.Code.List.Linear;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Code.Tests.List
{
    [TestClass]
    public class DoubleLinkedListFunctional: LinkedListFunctional
    {
        [TestInitialize]
        public void SetUp()
        {
            List = new DoubleLinkedList<int>();
        }
    }
}
