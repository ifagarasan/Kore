using System;
using System.IO;

namespace Kore.Dev.Util
{
    public static class IoUtil
    {
        public static string TestRoot;

        static IoUtil()
        {
            TestRoot = Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "TestData");
        }

        public static void EnsureFolderExits(string folder)
        {
            if (Directory.Exists((folder)))
                return;

            Directory.CreateDirectory(folder);
        }

        public static void EnsureFileExits(string file)
        {
            if (File.Exists((file)))
                return;

            EnsureFolderExits(Path.GetDirectoryName(file));

            using (var fs = File.Create(file)) { }
        }
    }
}
