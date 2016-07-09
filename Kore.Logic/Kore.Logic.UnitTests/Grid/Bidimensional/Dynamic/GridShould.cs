using Kore.Logic.Grid.Bidimensional;
using Kore.Logic.Grid.Bidimensional.Counter;
using Kore.Logic.Grid.Bidimensional.Distribution;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Kore.Logic.UnitTests.Grid.Bidimensional.Dynamic
{
    [TestClass]
    public class GridShould
    {
        [TestMethod]
        public void ReinitializeGridOnValueChange()
        {
            var distributionStrategyMock = new Mock<IDistributionStrategy>();
            distributionStrategyMock.Setup(m => m.Initialise(It.IsAny<IBidimensionalGrid<uint>>(), It.IsAny<uint>()));

            var bidimensionalGridMock = new Mock<IBidimensionalGrid<uint>>();

            Logic.Grid.Bidimensional.Dynamic.Grid grid = new Logic.Grid.Bidimensional.Dynamic.Grid(bidimensionalGridMock.Object, distributionStrategyMock.Object);

            var value = 100u;

            grid.Value = value;

            distributionStrategyMock.Verify(m => m.Initialise(bidimensionalGridMock.Object, value));
        }
    }
}
