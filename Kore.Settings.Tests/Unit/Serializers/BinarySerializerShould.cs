using Kore.Exceptions;
using Kore.Settings.Serializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Settings.UnitTests.Unit.Serializers
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
        [ExpectedException(typeof(NullException))]
        public void ThrowArgumentNullExceptionIfDataIsNullOnSerialize()
        {
            _serializer.Serialize(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullException))]
        public void ThrowArgumentNullExceptionIfStreamIsNullOnSerialize()
        {
            _serializer.Serialize(this, null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullException))]
        public void ThrowArgumentNullExceptionIfStreamIsNullOnDeserialize()
        {
            _serializer.Deserialize(null);
        }
    }
}
