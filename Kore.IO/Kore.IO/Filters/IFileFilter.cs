using System.Collections.Generic;
using Kore.IO.Util;

namespace Kore.IO.Filters
{
    public interface IFileFilter
    {
        List<IKoreFileInfo> Filter(List<IKoreFileInfo> list);
    }
}