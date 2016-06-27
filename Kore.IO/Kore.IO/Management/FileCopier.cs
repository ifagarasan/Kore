using System;
using System.IO;
using Kore.IO.Exceptions;
using static Kore.Validation.ObjectValidation;

namespace Kore.IO.Management
{
    public class FileCopier : IFileCopier
    {
        public void Copy(IKoreFileInfo source, IKoreFileInfo destination)
        {
            IsNotNull(source);
            IsNotNull(destination);

            if (!source.Exists)
                throw new NodeNotFoundException();

            if (source.FullName.Equals(destination.FullName))
                throw new InvalidDestinationNodeException();

            destination.FolderInfo.EnsureExists();

            File.Copy(source.FullName, destination.FullName, true);
        }
    }
}