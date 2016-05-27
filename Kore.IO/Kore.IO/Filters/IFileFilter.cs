using System.Collections.Generic;

namespace Kore.IO.Filters
{
    public interface IFileFilter
    {
        List<string> Filter(List<string> list);
    }
}