using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kore.Random
{
    public interface IGenerator
    {
        uint RetrieveNonNegative(uint max);
    }
}
