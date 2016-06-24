using static Kore.IdentityGenerators.NumericIdGenerator;

namespace Kore.IO.Sync
{
    public class IdentityProvider : IIdentityProvider
    {
        public long GenerateId()
        {
            return Generate();
        }
    }
}