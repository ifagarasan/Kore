using Kore.IO.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kore.IO.Validation
{
    public static class FileValidation
    {
        public static void Exists(IKoreFileInfo fileInfo)
        {
            if (fileInfo == null)
                throw new ArgumentNullException(nameof(fileInfo));

            if (!fileInfo.Exists)
                throw new FileNotFoundException(fileInfo.FullName);
        }
    }
}
