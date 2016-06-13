using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kore.Comparers;
using Kore.Exceptions;
using Kore.Validation;

namespace Kore.UnitTests.Validation
{
    [TestClass]
    public class ObjectalidationShould
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowArgumentNullExceptionIfNull()
        {
            ObjectValidation.IsNotNull(null);
        }
    }
}
