using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Kore.Validation.ObjectValidation;

namespace Kore.IO.Validation
{
    public static class FileValidation
    {
        public static void Exists(IKoreIoNodeInfo fileInfo)
        {
            IsNotNull(fileInfo, nameof(fileInfo));

            if (!fileInfo.Exists)
                throw new FileNotFoundException(fileInfo.FullName);
        }
    }
}
