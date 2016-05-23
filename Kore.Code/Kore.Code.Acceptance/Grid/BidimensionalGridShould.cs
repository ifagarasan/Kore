using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kore.Code.Grid;

namespace Kore.Code.Acceptance
{
    [TestClass]
    public class BidimensionalGridShould
    {
        BidimensionalGrid<int> _grid;

        [TestInitialize]
        public void Setup()
        {
            _grid = new BidimensionalGrid<int>(10, 10);
        }

        [TestMethod]
        public void SetAndRetrieveDataBasedOnTwoDimensions()
        {
            for (uint i = 0; i < 10; ++i)
                for (uint j = 0; j < 10; ++j)
                {
                    int currentValue = (int)(i * 10 + j);
                    _grid[i, j] = currentValue;
                    Assert.AreEqual(currentValue, _grid[i, j]);
                }
        }

        [TestMethod]
        public void AccessAllCollectionUsingEnumerator()
        {
            int current = 0;
            for (uint i = 0; i < 10; ++i)
                for (uint j = 0; j < 10; ++j)
                    _grid[i, j] = current++;

            current = 0;
            foreach (var cellValue in _grid)
                Assert.AreEqual(cellValue, current++);

            Assert.AreEqual(100, current);
        }
    }
}
