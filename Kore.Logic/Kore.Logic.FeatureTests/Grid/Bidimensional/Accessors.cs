using Kore.Logic.Grid.Bidimensional;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Logic.FeatureTests.Grid.Bidimensional
{
    [TestClass]
    public class Accessors
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
                    var currentValue = (int)(i * 10 + j);
                    _grid[i, j] = currentValue;
                    Assert.AreEqual(currentValue, _grid[i, j]);
                }
        }

        [TestMethod]
        public void AccessAllElementsUsingEnumerator()
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
