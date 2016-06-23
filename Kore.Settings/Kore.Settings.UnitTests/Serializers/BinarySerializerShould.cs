using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kore.Settings.Serializers;

namespace Kore.Settings.UnitTests
{
    [TestClass]
    public class BinarySerializerShould
    {
        BinarySerializer<BinarySerializerShould> _serializer;

        [TestInitialize]
        public void Setup()
        {
            _serializer = new BinarySerializer<BinarySerializerShould>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowArgumentNullExceptionIfDataIsNullOnSerialize()
        {
            _serializer.Serialize(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowArgumentNullExceptionIfStreamIsNullOnSerialize()
        {
            _serializer.Serialize(this, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowArgumentNullExceptionIfStreamIsNullOnDeserialize()
        {
            _serializer.Deserialize(null);
        }
    }
}
