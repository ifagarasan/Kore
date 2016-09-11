using System;
using System.Collections.Generic;
using System.IO;
using Kore.IO;
using Kore.Settings.Serializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Settings.UnitTests.Feature
{
    [TestClass]
    public class SettingsManagerShould
    {
        private string _testFolder;
        KoreFileInfo _settingsFileInfo;

        [TestInitialize]
        public void Setup()
        {
            _testFolder = Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "TestData");

            if (!Directory.Exists(_testFolder))
                Directory.CreateDirectory(_testFolder);

            _settingsFileInfo = new KoreFileInfo(Path.Combine(_testFolder, $"settings_{DateTime.Now.Ticks}.set"));
        }

        #region Xml

        [TestMethod]
        public void PersistDataInXmlFormat()
        {
            TestSerialization(CreateCar(), new XmlSerializer<Car>());
        }

        [TestMethod]
        public void PersistDataInXmlFormatUsingContract()
        {
            TestSerialization(CreateCar(), new XmlSerializer<Car>());
        }

        #endregion

        #region Binary

        [TestMethod]
        public void PersistDataInBinaryFormat()
        {
            TestSerialization(CreateCar(), new BinarySerializer<Car>());
        }

        #endregion

        private void TestSerialization(Car car, ISerializer<Car> serializer=null)
        {
            var settings = new SettingsManager<Car>(serializer) {Data = car};

            settings.Write(_settingsFileInfo);

            Assert.IsTrue(_settingsFileInfo.Exists);

            settings.Data = null;
            settings.Read(_settingsFileInfo);

            Assert.AreEqual(car, settings.Data);
        }

        private static Car CreateCar()
        {
            return new Car { Make = "Ford", Model = "Focus", Year = 2008, Owners = new List<Owner>
            {
                new Owner {Name="Billy"},
                new Owner {Name="James"},
            } };
        }
    }
}
