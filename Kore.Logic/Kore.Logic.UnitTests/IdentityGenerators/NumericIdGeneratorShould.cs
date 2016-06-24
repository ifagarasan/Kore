using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kore.Logic.IdentityGenerators;

namespace Kore.Logic.UnitTests.IdentityGenerators
{
    [TestClass]
    public class NumericIdGeneratorShould
    {
        [TestMethod]
        public void ReturnOneTheFirstTimeItsCalled()
        {
            Assert.AreEqual(1, NumericIdGenerator.Generate());
        }

        [TestMethod]
        public void IncrementWithEachCall()
        {
            var value = NumericIdGenerator.Generate();

            for (int i = 0; i < 10; ++i)
                Assert.AreEqual(value++ + 1, NumericIdGenerator.Generate());
        }
    }
}
