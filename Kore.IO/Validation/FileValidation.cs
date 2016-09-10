using System.IO;
using static Kore.Validation.ObjectValidation;

namespace Kore.IO.Validation
{
    public static class FileValidation
    {
        public static void Exists(IKoreIoNodeInfo fileInfo)
        {
            IsNotNull(fileInfo);

            if (!fileInfo.Exists)
                throw new FileNotFoundException(fileInfo.FullName);
        }
    }
}
