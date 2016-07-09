using Kore.Exceptions;
using Kore.Logic.Grid.Bidimensional;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Logic.UnitTests.Grid.Bidimensional
{
    [TestClass]
    public class BidimensionalGridShould
    {
        private BidimensionalGrid<int> _grid;

        [TestMethod]
        public void StoreAndRetrieveValueAtSpecifiedIndex()
        {
            _grid = new BidimensionalGrid<int>(10, 10);

            var row = 1u;
            var column = 2u;
            var value = -153;

            _grid[row, column] = value;

            Assert.AreEqual(value, _grid[row, column]);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidQuantityException))]
        public void ThrowsInvalidQuantityExceptionIfRowsIsZero()
        {
            _grid = new BidimensionalGrid<int>(0, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidQuantityException))]
        public void ThrowsInvalidQuantityExceptionIfColumnsIsZero()
        {
            _grid = new BidimensionalGrid<int>(10, 0);
        }
    }
}
