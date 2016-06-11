using System.Runtime.Serialization;

namespace Kore.Settings.AcceptanceTests
{
    [KnownType(typeof(Car))]
    public class Car : ICar
    {
        [DataMember]
        public string Make { get; set; }

        [DataMember]
        public string Model { get; set; }

        [DataMember]
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