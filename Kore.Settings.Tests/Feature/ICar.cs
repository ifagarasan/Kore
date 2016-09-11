using System.Collections.Generic;

namespace Kore.Settings.UnitTests.Feature
{
    public interface ICar
    {
        string Make { get; set; }
        string Model { get; set; }
        int Year { get; set; }
        List<Owner> Owners { get; set; }

        bool Equals(object car);
    }
}