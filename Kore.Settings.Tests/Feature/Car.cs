using System;
using System.Collections.Generic;

namespace Kore.Settings.UnitTests.Feature
{
    [Serializable]
    public class Car : ICar
    {
        public Car()
        {
            Owners = new List<Owner>();
        }

        public string Make { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public List<Owner> Owners { get; set; }

        public override bool Equals(object car) => car is Car && Equals((Car) car);

        protected bool Equals(Car other)
            => string.Equals(Make, other.Make) && string.Equals(Model, other.Model) && Year == other.Year;

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Make?.GetHashCode() ?? 0;
                hashCode = (hashCode*397) ^ (Model?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ Year;
                return hashCode;
            }
        }
    }
}