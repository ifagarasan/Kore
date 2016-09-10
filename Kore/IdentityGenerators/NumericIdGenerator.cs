namespace Kore.IdentityGenerators
{
    public class NumericIdGenerator
    {
        private long _currentValue = 1;

        public long Generate()
        {
            return _currentValue++;
        }
    }
}
