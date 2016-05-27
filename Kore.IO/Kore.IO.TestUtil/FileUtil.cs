using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.IO.TestUtil
{
    public static class FileUtil
    {
        public static void EnsureFolderExits(string file)
        {
            FolderUtil.EnsureExits(Path.GetDirectoryName(file));
        }

        public static void EnsureExits(string file)
        {
            if (File.Exists(file))
                return;

            EnsureFolderExits(file);
            File.Create(file);
        }
    }
}
