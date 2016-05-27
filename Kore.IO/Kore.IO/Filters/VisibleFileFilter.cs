using System;
using System.Collections.Generic;
using Kore.IO.Filters;
using Kore.IO.Util;

namespace Kore.IO.Filters
{
    public class VisibleFileFilter: IFileFilter
    {
        private IFileInfoProvider _fileInfoProvider;

        public VisibleFileFilter(IFileInfoProvider fileInfoProvider)
        {
            _fileInfoProvider = fileInfoProvider;
        }

        public List<string> Filter(List<string> fileList)
        {
            List<string> output = new List<string>();

            foreach (string file in fileList)
            {
                IKoreFileInfo fileInfo = _fileInfoProvider.GetFileInfo(file);

                if (!fileInfo.Hidden)
                    output.Add(file);
            }

            return output;
        }
    }
}