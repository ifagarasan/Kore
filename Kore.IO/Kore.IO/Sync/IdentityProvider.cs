using System;

namespace Kore.IO.Sync
{
    public class IdentityProvider : IIdentityProvider
    {
        public long GenerateId()
        {
            return 0; //TOOD: call Kore identity generator
        }
    }
}