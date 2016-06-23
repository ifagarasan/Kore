using System;
using System.IO;

namespace Kore.IO.Management
{
    public class FileCopier : IFileCopier
    {
        public void Copy(IKoreFileInfo source, IKoreFileInfo destination)
        {
            File.Copy(source.FullName, destination.FullName);
        }
    }
}