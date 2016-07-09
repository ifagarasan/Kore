namespace Kore.Logic.Grid.Bidimensional.Distribution
{
    public interface IDistributionStrategy
    {
        void Initialise(IBidimensionalGrid<uint> grid, uint value);
    }
}