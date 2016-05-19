using System;
using Kore.Code.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kore.Code.Grid;

namespace Kore.Code.Tests.Grid
{
    [TestClass]
    public class BidimensionalGridShould
    {
        [TestMethod]
        public void StoreAndRetrieveValueAtSpecifiedIndex()
        {
            BidimensionalGrid<int> grid = new BidimensionalGrid<int>(10, 10);

            uint row = 1, column = 2;
            int value = -153;

            grid[row, column] = value;

            Assert.AreEqual(value, grid[row, column]);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidQuantityException))]
        public void ThrowsInvalidQuantityExceptionIfRowsIsZero()
        {
            new BidimensionalGrid<int>(0, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidQuantityException))]
        public void ThrowsInvalidQuantityExceptionIfColumnsIsZero()
        {
            new BidimensionalGrid<int>(10, 0);
        }
    }
}
