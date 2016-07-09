using Kore.Logic.Grid.Bidimensional.Distribution;

namespace Kore.Logic.Grid.Bidimensional.Dynamic
{
    public class Grid
    {
        private readonly IBidimensionalGrid<uint> _grid;
        private readonly IDistributionStrategy _distributionStrategy;
        private uint _value;

        public Grid(IBidimensionalGrid<uint> grid, IDistributionStrategy distributionStrategy)
        {
            _grid = grid;
            _distributionStrategy = distributionStrategy;
        }

        public uint Value
        {
            get { return _value; }
            set
            {
                _distributionStrategy.Initialise(_grid, value);

                _value = value;
            }
        }
    }
}