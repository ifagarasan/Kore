using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Kore.Code.List;
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
            list = new CircularList<int>();
        }
    }
}
