using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Kore.Settings.Serializers;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Kore.IO.Exceptions;
using Kore.IO.Util;

namespace Kore.Settings.UnitTests
{
    [TestClass]
    public class SettingsManagerShould
    {
        Mock<ISerializer<DateTime>> _mockSerializer;
        Mock<IKoreFileInfo> _mockFileInfo;
        SettingsManager<DateTime> _settings;

        [TestInitialize]
        public void Setup()
        {
            _mockSerializer = new Mock<ISerializer<DateTime>>();

            _mockFileInfo = new Mock<IKoreFileInfo>();
            _mockFileInfo.Setup(m => m.FullName).Returns("settings.xml");
            _mockFileInfo.Setup(m => m.Exists).Returns(true);

            _settings = new SettingsManager<DateTime>(_mockSerializer.Object);
        }

        #region Write

        [TestMethod]
        public void CallSerializerUponWriting()
        {
            var now = DateTime.Now;

            _settings = new SettingsManager<DateTime>(_mockSerializer.Object) {Data = now};

            _mockSerializer.Setup(m => m.Serialize(It.IsAny<DateTime>(), It.IsAny<Stream>()));

            _settings.Write(_mockFileInfo.Object);

            _mockSerializer.Verify(m => m.Serialize(now, It.IsAny<Stream>()));
        }

        [TestMethod]
        [ExpectedException(typeof(SerializationException))]
        public void ValidateDataOnWrite()
        {
            var settings = new SettingsManager<string>(new Mock<ISerializer<string>>().Object);

            settings.Write(_mockFileInfo.Object);
        }

        #endregion

        #region Read

        [TestMethod]
        public void CallSerializerUponReading()
        {
            _mockSerializer.Setup(m => m.Deserialize(It.IsAny<Stream>()));

            _settings.Read(_mockFileInfo.Object);

            _mockSerializer.Verify(m => m.Deserialize(It.IsAny<Stream>()));
        }

        [TestMethod]
        [ExpectedException(typeof(NodeNotFoundException))]
        public void ValidateFileExistsOnRead()
        {
            _mockFileInfo.Setup(m => m.Exists).Returns(false);

            _settings.Read(_mockFileInfo.Object);
        }

        #endregion

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsArgumentNullExceptionWhenPassingNullOnWrite()
        {
            _settings.Write(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsArgumentNullExceptionWhenPassingNullOnRead()
        {
            _settings.Read(null);
        }
    }
}
