using Kore.IO.Util;
using Kore.Validation;
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
        public static void Exists(IKoreIoNodeInfo fileInfo)
        {
            ObjectValidation.IsNotNull(fileInfo, nameof(fileInfo));

            if (!fileInfo.Exists)
                throw new FileNotFoundException(fileInfo.FullName);
        }
    }
}
