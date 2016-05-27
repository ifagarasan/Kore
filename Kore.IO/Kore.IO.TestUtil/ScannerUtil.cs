using System;
using System.Collections.Generic;
using System.IO;

namespace Kore.IO.TestUtil
{
    public static class ScannerUtil
    {
        public static List<string> VisibleFileList { get; }
        public static List<string> HiddenFileList { get; }

        static string TestFolder { get; }
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
            FolderUtil.EnsureExits(TestFolder);
            FolderUtil.EnsureExits(TestFolderDeep);
            FolderUtil.EnsureExits(TestFolderOneLevel);

            EnsureVisibleFilesExist(BuildDeepTestFilesList(true, false));
            EnsureHiddenFilesExist(BuildDeepTestFilesList(false, true));

            EnsureVisibleFilesExist(BuildOneLevelTestFilesList(true, false));
            EnsureHiddenFilesExist(BuildOneLevelTestFilesList(false, true));
        }

        private static void EnsureVisibleFilesExist(List<string> files)
        {
            foreach (string file in files)
            {
                FileUtil.EnsureExits(file);
                FileInfo fi = new FileInfo(file);
                if (fi.Attributes.HasFlag(FileAttributes.Hidden))
                    File.SetAttributes(file, FileAttributes.Normal);
            }
        }

        private static void EnsureHiddenFilesExist(List<string> files)
        {
            foreach (string file in files)
            {
                FileUtil.EnsureExits(file);
                FileInfo fi = new FileInfo(file);
                File.SetAttributes(file, FileAttributes.Hidden);
            }
        }

        public static void AddFiles(string folder, List<string> inputList, List<string> outputList)
        {
            AddFiles(folder, inputList, outputList, string.Empty);
        }

        public static void AddFiles(string folder, List<string> inputList, List<string> outputList, string extension)
        {
            foreach (string inputItem in inputList)
                if (inputItem.EndsWith(extension))
                    outputList.Add(Path.Combine(folder, inputItem));
        }

        private static void BuildFilesForFolder(string folder, List<string> outputList, string extension, bool addVisible, bool addHidden)
        {
            if (addVisible)
                AddFiles(folder, VisibleFileList, outputList, extension);
            if (addHidden)
                AddFiles(folder, HiddenFileList, outputList, extension);
        }

        public static List<string> BuildDeepTestFilesList(bool addVisible, bool addHidden, string extension = "")
        {
            List<string> outputList = new List<string>();

            string currentFolder = TestFolderDeep;

            for (int i = 0; i < 1; ++i)
            {
                BuildFilesForFolder(currentFolder, outputList, extension, addVisible, addHidden);
                currentFolder = Path.Combine(currentFolder, "1");
            }

            return outputList;
        }

        public static List<string> BuildOneLevelTestFilesList(bool addVisible, bool addHidden, string extension = "")
        {
            List<string> outputList = new List<string>();

            BuildFilesForFolder(TestFolderOneLevel, outputList, extension, addVisible, addHidden);

            return outputList;
        }
    }
}
