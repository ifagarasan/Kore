using System;
using System.Collections.Generic;
using System.Linq;

namespace Kore.IO.Filters
{
    public class VisibleFileFilter: IFileFilter
    {
        public List<IKoreFileInfo> Filter(List<IKoreFileInfo> fileList)
        {
            return fileList.Where(fileInfo => !fileInfo.Hidden).ToList();
        }
    }
}