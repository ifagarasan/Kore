using Kore.Exceptions;
using Kore.Vector;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.UnitTests.Vector
{
    [TestClass]
    public class VectorShould
    {
        private Vector<int> _vector;

        [TestMethod]
        public void StoreAndRetrieveValueAtSpecifiedIndex()
        {
            _vector = new Vector<int>(10);

            var value = -153;

            _vector[3].Value = value;

            Assert.AreEqual(value, _vector[3].Value);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidQuantityException))]
        public void ThrowInvalidQuantityExceptionIfRowsIsZero()
        {
            _vector = new Vector<int>(0);
        }
    }
}
