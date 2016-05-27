using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace Kore.IO.TestUtil
{
    public static class FolderUtil
    {
        public static void EnsureExits(string folder)
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
        }
    }
}
