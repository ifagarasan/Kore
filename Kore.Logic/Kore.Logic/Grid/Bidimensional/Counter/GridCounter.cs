using System;
using Kore.Logic.Grid.Bidimensional.Distribution;
using Kore.Logic.Grid.Bidimensional.Selection;

namespace Kore.Logic.Grid.Bidimensional.Counter
{
    public class GridCounter : IGridCounter
    {
        private IDistributionStrategy _distributionStrategy;
        private BidimensionalGrid<uint> _grid;
        private RandomSelection _randomSelection;

        public GridCounter(BidimensionalGrid<uint> grid, IDistributionStrategy distributionStrategy, RandomSelection randomSelection)
        {
            _grid = grid;
            _distributionStrategy = distributionStrategy;
            _randomSelection = randomSelection;
        }

        public uint Value { get; private set; }

        public void Subtract(int v)
        {
            throw new NotImplementedException();
        }

        public void Initialise(uint value)
        {
            _distributionStrategy.Initialise(_grid, value);
            Value = value;
        }
    }
}