using Kore.Logic.Distribution;
using Kore.Logic.Dynamic;
using Kore.Logic.Selection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Logic.FeatureTests.Dynamic
{
    [TestClass]
    public class Adjustment
    {
        [Ignore]
        [TestMethod]
        public void Add()
        {
            var grid = new Grid(new Vector.Vector<uint>(10),
                new UniformDistribution(), new Random<uint>()) {Value = 100};

            grid.Add(10u);

            Assert.AreEqual(110u, grid.Value);
        }
    }
}
