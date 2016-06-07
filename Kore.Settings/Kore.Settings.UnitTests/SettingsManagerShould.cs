using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Kore.Settings.Serializers;
using System.IO;
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

        [TestMethod]
        public void CallSerializerUponWriting()
        {
            DateTime now = DateTime.Now;

            _settings = new SettingsManager<DateTime>(now, _mockSerializer.Object);

            _mockSerializer.Setup(m => m.Serialize(It.IsAny<DateTime>(), It.IsAny<Stream>()));

            _settings.Write(_mockFileInfo.Object);

            _mockSerializer.Verify(m => m.Serialize(now, It.IsAny<Stream>()));
        }

        [TestMethod]
        public void CallSerializerUponReading()
        {
            _mockSerializer.Setup(m => m.Deserialize(It.IsAny<Stream>()));

            _settings.Read(_mockFileInfo.Object);

            _mockSerializer.Verify(m => m.Deserialize(It.IsAny<Stream>()));
        }

        [TestMethod]
        public void DoesNotErrorOnReadIfFileDoesNotExist()
        {
            _mockFileInfo.Setup(m => m.FullName).Returns("settings_does_not_exist.xml");
            _mockFileInfo.Setup(m => m.Exists).Returns(false);

            _settings.Read(_mockFileInfo.Object);

            Assert.IsNotNull(_settings.Data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsArgumentNullExceptionWhenPassingNullAtConstruction()
        {
            SettingsManager<DateTime?> settings = new SettingsManager<DateTime?>(null, new Mock<ISerializer<DateTime?>>().Object);
        }

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
