using Kore.Logic.Grid.Bidimensional;
using Kore.Logic.Grid.Bidimensional.Distribution;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Logic.FeatureTests.Grid.Bidimensional.Dynamic
{
    [TestClass]
    public class Reinitalization
    {
        [TestMethod]
        public void SetsTheNewValue()
        {
            var grid = new Logic.Grid.Bidimensional.Dynamic.Grid(new BidimensionalGrid<uint>(10, 10), new UniformDistribution()) {Value = 100};

            Assert.AreEqual(100u, grid.Value);

            grid.Value = 50;

            Assert.AreEqual(50u, grid.Value);
        }
    }
}
