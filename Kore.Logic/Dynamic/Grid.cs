using Kore.Logic.Distribution;
using Kore.Logic.Selection;
using Kore.Vector;

namespace Kore.Logic.Dynamic
{
    public class Grid : IGrid<uint>
    {
        private readonly IVector<uint> _vector;
        private readonly IDistributionStrategy _distributionStrategy;
        private readonly ISelectionStrategy<uint> _selectionStrategy;
        private uint _value;

        public Grid(IVector<uint> vector, IDistributionStrategy distributionStrategy, ISelectionStrategy<uint> selectionStrategy)
        {
            _vector = vector;
            _distributionStrategy = distributionStrategy;
            _selectionStrategy = selectionStrategy;
        }

        public uint Value
        {
            get { return _value; }
            set
            {
                _distributionStrategy.Initialise(_vector, value);

                _value = value;
            }
        }

        public void Add(uint amount)
        {
            _selectionStrategy.RetrieveCell(_vector).Value += amount;
        }
    }
}