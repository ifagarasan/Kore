﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using Kore.Settings.Serializers;
using Kore.IO.Util;

namespace Kore.Settings.AcceptanceTests
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

            _settingsFileInfo = new KoreFileInfo(Path.Combine(_testFolder, $"settings_{DateTime.Now.Ticks}.xml"));
        }

        [TestMethod]
        public void PersistData()
        {
            TestSerialization(CreateCar());
        }

        private void TestSerialization(Car car, DataContractResolver contractResolver = null)
        {
            SettingsManager<Car> settings = new SettingsManager<Car>(car, new XmlSerializer<Car>(contractResolver));

            settings.Write(_settingsFileInfo);

            Assert.IsTrue(_settingsFileInfo.Exists);

            settings.Data = null;
            settings.Read(_settingsFileInfo);

            Assert.AreEqual(car, settings.Data);
        }

        [TestMethod]
        public void PersistDataUsingContract()
        {
            TestSerialization(CreateCar(), new CarContractResolver());
        }

        private static Car CreateCar()
        {
            return new Car() { Make = "Ford", Model = "Focus", Year = 2008 };
        }

        public class CarContractResolver : IdentityContractResolver
        {
            public override Type ResolveName(string typeName, string typeNamespace, Type declaredType, DataContractResolver knownTypeResolver)
            {
                if (typeName == "Car" && typeNamespace.EndsWith("Kore.Settings.AcceptanceTests"))
                    return typeof(Car);

                return base.ResolveName(typeName, typeNamespace, declaredType, knownTypeResolver);
            }
        }
    }
}
