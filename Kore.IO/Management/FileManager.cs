using Kore.Validation;
using System;

namespace Kore.IO.Management
{
    public class FileManager : IFileManager
    {
        private readonly IFileCopier _copier;

        public FileManager(IFileCopier copier)
        {
            ObjectValidation.IsNotNull(copier);

            _copier = copier;
        }

        public void Copy(IKoreFileInfo source, IKoreFileInfo destination)
        {
            _copier.Copy(source, destination);
        }
    }
}