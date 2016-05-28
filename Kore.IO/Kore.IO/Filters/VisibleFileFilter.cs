using System;
using System.Collections.Generic;
using System.Linq;
using Kore.IO.Filters;
using Kore.IO.Util;

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