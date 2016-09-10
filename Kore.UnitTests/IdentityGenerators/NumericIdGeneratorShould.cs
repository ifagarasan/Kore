using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kore.IdentityGenerators;

namespace Kore.UnitTests.IdentityGenerators
{
    [TestClass]
    public class NumericIdGeneratorShould
    {
        private NumericIdGenerator _numericGenerator;

        [TestInitialize]
        public void Setup()
        {
            _numericGenerator = new NumericIdGenerator();
        }

        [TestMethod]
        public void ReturnOneTheFirstTimeItsCalled()
        {
            Assert.AreEqual(1, _numericGenerator.Generate());
        }

        [TestMethod]
        public void IncrementWithEachCall()
        {
            var value = _numericGenerator.Generate();

            for (int i = 0; i < 10; ++i)
                Assert.AreEqual(value++ + 1, _numericGenerator.Generate());
        }
    }
}
