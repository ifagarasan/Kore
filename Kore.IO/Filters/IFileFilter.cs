using System.Collections.Generic;

namespace Kore.IO.Filters
{
    public interface IFileFilter
    {
        List<IKoreFileInfo> Filter(List<IKoreFileInfo> list);
    }
}