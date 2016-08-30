using Kore.Logic.Selection;
using Kore.Random;
using Kore.Vector;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Kore.Logic.UnitTests.Selection
{
    [TestClass]
    public class RandomShould
    {
        [Ignore]
        [TestMethod]
        public void SelectRandomIndex()
        {
            var generatorMock = new Mock<IGenerator>();

            var one = 1u;
            generatorMock.Setup(m => m.RetrieveNonNegative(It.IsAny<uint>())).Returns(one);

            var cell = new Element<uint>();

            var gridMock = new Mock<IVector<uint>>();
            gridMock.Setup(m => m[It.IsAny<uint>()]).Returns(cell);
            gridMock.Setup(m => m.Count).Returns(6);

            var random = new Random<uint>();

            Assert.AreSame(cell, random.RetrieveCell(gridMock.Object));

            gridMock.VerifyGet(m => m.Count);
            
            generatorMock.Verify(m => m.RetrieveNonNegative(6));
        }
    }
}
