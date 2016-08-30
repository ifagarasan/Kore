using Kore.Vector;

namespace Kore.Logic.Distribution
{
    public interface IDistributionStrategy
    {
        void Initialise(IVector<uint> vector, uint value);
    }
}