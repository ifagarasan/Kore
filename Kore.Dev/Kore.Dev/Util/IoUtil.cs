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

        public static void EnsureFolderExists(string folder)
        {
            if (Directory.Exists((folder)))
                return;

            Directory.CreateDirectory(folder);
        }

        public static void EnsureFileExists(string file)
        {
            if (File.Exists((file)))
                return;

            EnsureFolderExists(Path.GetDirectoryName(file));

            using (var fs = File.Create(file)) { }
        }
    }
}
