using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Kore.Settings.Serializers;
using System.IO;

namespace Kore.Settings.UnitTests
{
    [TestClass]
    public class SettingsManagerShould
    {
        Mock<ISerializer<DateTime>> _mockSerializer;

        [TestInitialize]
        public void Setup()
        {
            _mockSerializer = new Mock<ISerializer<DateTime>>();
        }

        [TestMethod]
        public void CallSerializerUponWriting()
        {
            DateTime now = DateTime.Now;

            SettingsManager<DateTime> settings = new SettingsManager<DateTime>(now, _mockSerializer.Object);

            _mockSerializer.Setup(m => m.Serialize(It.IsAny<DateTime>(), It.IsAny<Stream>()));

            string file = "settings.xml";

            settings.Write(file);

            _mockSerializer.Verify(m => m.Serialize(now, It.IsAny<Stream>()));
        }

        [TestMethod]
        public void CallSerializerUponReading()
        {
            SettingsManager<DateTime> settings = new SettingsManager<DateTime>(_mockSerializer.Object);

            _mockSerializer.Setup(m => m.Deserialize(It.IsAny<Stream>()));

            settings.Read("settings.xml");

            _mockSerializer.Verify(m => m.Deserialize(It.IsAny<Stream>()));
        }
    }
}
