using Kore.IdentityGenerators;

namespace Kore.IO.Sync
{
    public class IdentityProvider : IIdentityProvider
    {
        readonly NumericIdGenerator _generator = new NumericIdGenerator();

        public long GenerateId()
        {
            return _generator.Generate();
        }
    }
}