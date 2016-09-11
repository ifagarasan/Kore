using Kore.Logic.Distribution;
using Kore.Logic.Dynamic;
using Kore.Logic.Selection;
using Kore.Vector;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Kore.Logic.UnitTests.Dynamic
{
    [TestClass]
    public class GridShould
    {
        Mock<IDistributionStrategy> _distributionStrategyMock;
        Mock<ISelectionStrategy<uint>> _selectionStrategy;
        Mock<IVector<uint>> _bidimensionalGridMock;
        Grid _grid;

        [TestInitialize]
        public void Setup()
        {
            _distributionStrategyMock = new Mock<IDistributionStrategy>();
            _distributionStrategyMock.Setup(m => m.Initialise(It.IsAny<IVector<uint>>(), It.IsAny<uint>()));

            _selectionStrategy = new Mock<ISelectionStrategy<uint>>();
            _selectionStrategy.Setup(m => m.RetrieveCell(It.IsAny<IVector<uint>>())).Returns(new Element<uint>());

            _bidimensionalGridMock = new Mock<IVector<uint>>();

            _grid = new Grid(_bidimensionalGridMock.Object,
                _distributionStrategyMock.Object, _selectionStrategy.Object);
        }

        [TestMethod]
        public void ReinitializeGridOnValueChange()
        {
            var value = 100u;

            _grid.Value = value;

            _distributionStrategyMock.Verify(m => m.Initialise(_bidimensionalGridMock.Object, value));
        }

        [TestMethod]
        public void Add()
        {
            var grid = new Grid(_bidimensionalGridMock.Object,
                _distributionStrategyMock.Object, _selectionStrategy.Object);

            grid.Add(10u);

            _selectionStrategy.Verify(m => m.RetrieveCell(_bidimensionalGridMock.Object));
        }
    }
}
