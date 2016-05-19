using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kore.Code.Grid;

namespace Kore.Code.Acceptance
{
    [TestClass]
    public class BidimensionalGridShould
    {
        [TestMethod]
        public void SetAndRetrieveDataBasedOnTwoDimensions()
        {
            BidimensionalGrid<int> grid = new BidimensionalGrid<int>(10, 10);

            for (uint i = 0; i < 10; ++i)
                for (uint j = 0; j < 10; ++j)
                {
                    int currentValue = (int)(i * 10 + j);
                    grid.SaveValue(i, j, currentValue);
                    Assert.AreEqual(currentValue, grid.RetrieveValue(i, j));
                }
        }
    }
}
