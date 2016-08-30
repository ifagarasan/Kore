using Kore.Vector;

namespace Kore.Logic.Distribution
{
    public class UniformDistribution: IDistributionStrategy
    {
        public void Initialise(IVector<uint> vector, uint value)
        {
            ApplyValueToGrid(vector, value / vector.Count);
            ApplySurplus(vector, value % vector.Count);
        }

        private static void ApplySurplus(IVector<uint> vector, uint surplus)
        {
            for (var i = 0u; i < vector.Count; ++i)
            {
                if (surplus == 0)
                    break;

                vector[i].Value++;
                surplus--;
            }
        }

        private static void ApplyValueToGrid(IVector<uint> vector, uint value)
        {
            foreach (var cell in vector)
                cell.Value = value;
        }
    }
} 