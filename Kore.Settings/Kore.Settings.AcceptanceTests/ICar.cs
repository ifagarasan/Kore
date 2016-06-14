namespace Kore.Settings.AcceptanceTests
{
    public interface ICar
    {
        string Make { get; set; }
        string Model { get; set; }
        int Year { get; set; }

        bool Equals(object obj);
    }
}