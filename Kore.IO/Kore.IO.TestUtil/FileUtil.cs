using System;
using System.Collections.Generic;
using System.IO;
using Kore.IO.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.IO.TestUtil
{
    public static class FileUtil
    {
        public static void EnsureFolderExits(IKoreFileInfo file)
        {
            FolderUtil.EnsureExits(file.DirectoryFullName);
        }

        public static void EnsureExits(IKoreFileInfo file)
        {
            if (file.Exists)
                return;

            EnsureFolderExits(file);
            File.Create(file.FullName);
        }
    }
}
