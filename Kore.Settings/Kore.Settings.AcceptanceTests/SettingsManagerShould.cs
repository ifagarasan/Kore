using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Kore.Settings.Serializers;
using Kore.IO.Util;

namespace Kore.Settings.AcceptanceTests
{
    [TestClass]
    public class SettingsManagerShould
    {
        string testFolder;

        [TestInitialize]
        public void Setup()
        {
            testFolder = Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "TestData");
            if (!Directory.Exists(testFolder))
                Directory.CreateDirectory(testFolder);
        }

        [TestMethod]
        public void PersistData()
        {
            Car car = new Car() { Make = "Ford", Model = "Focus", Year = 2008 };
            SettingsManager<Car> settings = new SettingsManager<Car>(car, new XmlSerializer<Car>());

            KoreFileInfo fileInfo = new KoreFileInfo(Path.Combine(testFolder, string.Format("settings_{0}.xml", DateTime.Now.Ticks)));
            settings.Write(fileInfo);

            Assert.IsTrue(fileInfo.Exists);

            settings.Data = null;
            settings.Read(fileInfo);

            Assert.AreEqual(car, settings.Data);
        }
    }

    public class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        public override bool Equals(object obj)
        {
            Car car = obj as Car;

            if (car == null)
                return false;

            return Make.Equals(car.Make) && Model.Equals(car.Model) && Year == car.Year;
        }
    }
}
