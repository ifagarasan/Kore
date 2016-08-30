using Kore.Logic.Distribution;
using Kore.Logic.Dynamic;
using Kore.Logic.Selection;
using Kore.Vector;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Logic.FeatureTests.Dynamic
{
    [TestClass]
    public class Init
    {
        [TestMethod]
        public void SetsTheNewValue()
        {
            var grid = new Grid(new Vector<uint>(10),
                new UniformDistribution(), new Random<uint>()) {Value = 100};

            Assert.AreEqual(100u, grid.Value);

            grid.Value = 50;

            Assert.AreEqual(50u, grid.Value);
        }
    }
}
