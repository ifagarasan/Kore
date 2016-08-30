using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kore.Comparers;
using Kore.Exceptions;
using Kore.Validation;

namespace Kore.UnitTests.Validation
{
    [TestClass]
    public class ComparisonValidationShould
    {
        [TestMethod]
        [ExpectedException(typeof(ComparisonException))]
        public void ThrowComparisonExceptionWhenExpectedEqualButGotNotEqual()
        {
            ComparisonValidation<int>.IsEqualTo(1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ComparisonException))]
        public void ThrowComparisonExceptionWhenExpectedLargerButGotEqual()
        {
            ComparisonValidation<int>.IsLargerThan(1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ComparisonException))]
        public void ThrowComparisonExceptionWhenExpectedLargerButGotSmaller()
        {
            ComparisonValidation<int>.IsLargerThan(1, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ComparisonException))]
        public void ThrowComparisonExceptionWhenExpectedSmallerButGotEqual()
        {
            ComparisonValidation<int>.IsSmallerThan(1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ComparisonException))]
        public void ThrowComparisonExceptionWhenExpectedSmallerButGotLarger()
        {
            ComparisonValidation<int>.IsSmallerThan(1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ComparisonException))]
        public void ThrowComparisonExceptionWhenExpectedSmallerOrEqualButGotLarger()
        {
            ComparisonValidation<int>.IsSmallerThanOrEqualTo(2, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ComparisonException))]
        public void ThrowComparisonExceptionWhenExpectedLargerOrEqualButGotSmaller()
        {
            ComparisonValidation<int>.IsLargerThanOrEqualTo(1, 2);
        }
    }
}
