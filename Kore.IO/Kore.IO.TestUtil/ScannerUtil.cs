using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Kore.Dev.Util;

namespace Kore.IO.TestUtil
{
    public static class ScannerUtil
    {
        public static List<string> VisibleFileList { get; }
        public static List<string> HiddenFileList { get; }

        public static string TestFolder { get; }
        public static string TestFolderDeep { get; }
        public static string TestFolderOneLevel { get; }

        static ScannerUtil()
        {
            TestFolder = Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "TestData", "Scanners", "FileScanners");
            TestFolderDeep = Path.Combine(TestFolder, "Deep");
            TestFolderOneLevel = Path.Combine(TestFolder, "OneLevel");

            VisibleFileList = new List<string>()
            {
                "armrest.pdf",
                "insurance.PNG",
                "Inteview TODOs.txt",
                "export-31-10.xml"
            };

            HiddenFileList = new List<string>()
            {
                "to watch.txt"
            };

            SetupTestFiles();
        }

        public static void SetupTestFiles()
        {
            IoUtil.EnsureFolderExists(TestFolder);
            IoUtil.EnsureFolderExists(TestFolderDeep);
            IoUtil.EnsureFolderExists(TestFolderOneLevel);

            EnsureFilesExist(BuildDeepTestFilesList(true, false), false);
            EnsureFilesExist(BuildDeepTestFilesList(false, true), true);

            EnsureFilesExist(BuildOneLevelTestFilesList(true, false), false);
            EnsureFilesExist(BuildOneLevelTestFilesList(false, true), true);
        }

        private static void EnsureFilesExist(IEnumerable<IKoreFileInfo> files, bool hidden)
        {
            foreach (IKoreFileInfo fileInfo in files)
            {
                fileInfo.EnsureExists();
                fileInfo.Hidden = hidden;
            }
        }

        public static void AddFiles(string folder, List<string> inputList, List<IKoreFileInfo> outputList)
        {
            AddFiles(folder, inputList, outputList, string.Empty);
        }

        public static void AddFiles(string folder, List<string> inputList, List<IKoreFileInfo> outputList, string extension)
        {
            outputList.AddRange((from inputItem in inputList where inputItem.EndsWith(extension) select new KoreFileInfo(Path.Combine(folder, inputItem))).Cast<IKoreFileInfo>());
        }

        private static void BuildFilesForFolder(string folder, List<IKoreFileInfo> outputList, string extension, bool addVisible, bool addHidden)
        {
            if (addVisible)
                AddFiles(folder, VisibleFileList, outputList, extension);
            if (addHidden)
                AddFiles(folder, HiddenFileList, outputList, extension);
        }

        public static List<IKoreFileInfo> BuildDeepTestFilesList(bool addVisible, bool addHidden, string extension = "")
        {
            List<IKoreFileInfo> outputList = new List<IKoreFileInfo>();

            string currentFolder = TestFolderDeep;

            for (int i = 0; i < 2; ++i)
            {
                BuildFilesForFolder(currentFolder, outputList, extension, addVisible, addHidden);
                currentFolder = Path.Combine(currentFolder, "1");
            }

            return outputList;
        }

        public static List<IKoreFileInfo> BuildOneLevelTestFilesList(bool addVisible, bool addHidden, string extension = "")
        {
            List<IKoreFileInfo> outputList = new List<IKoreFileInfo>();

            BuildFilesForFolder(TestFolderOneLevel, outputList, extension, addVisible, addHidden);

            return outputList;
        }
    }
}
