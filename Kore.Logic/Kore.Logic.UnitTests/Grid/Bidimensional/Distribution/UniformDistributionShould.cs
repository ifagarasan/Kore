using Kore.Logic.Grid.Bidimensional;
using Kore.Logic.Grid.Bidimensional.Distribution;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Kore.Logic.UnitTests.Grid.Bidimensional.Distribution
{
    [TestClass]
    public class UniformDistributionShould
    {
        [TestMethod]
        public void InitialiseEachCellWithAverageValue()
        {
            var uniformDistribution = new UniformDistribution();

            var grid = new BidimensionalGrid<uint>(3, 3);

            uniformDistribution.Initialise(grid, 90);

            foreach (var cellValue in grid)
                Assert.AreEqual(10u, cellValue);
        }

        [TestMethod]
        public void AddSurplusFromTopLeft()
        {
            var uniformDistribution = new UniformDistribution();

            var grid = new BidimensionalGrid<uint>(3, 3);

            uniformDistribution.Initialise(grid, 92);

            Assert.AreEqual(11u, grid[0, 0]);
            Assert.AreEqual(11u, grid[0, 1]);
        }
    }
}
