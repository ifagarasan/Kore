using Kore.Logic.Distribution;
using Kore.Vector;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Logic.UnitTests.Distribution
{
    [TestClass]
    public class UniformDistributionShould
    {
        [TestMethod]
        public void InitialiseEachCellWithAverageValue()
        {
            var uniformDistribution = new UniformDistribution();

            var grid = new Vector<uint>(3);

            uniformDistribution.Initialise(grid, 9);

            foreach (Element<uint> cell in grid)
                Assert.AreEqual(3u, cell.Value);
        }

        [TestMethod]
        public void AddSurplusFromTopLeft()
        {
            var uniformDistribution = new UniformDistribution();

            var grid = new Vector<uint>(3);

            uniformDistribution.Initialise(grid, 11);

            Assert.AreEqual(4u, grid[0].Value);
            Assert.AreEqual(4u, grid[1].Value);
        }
    }
}
