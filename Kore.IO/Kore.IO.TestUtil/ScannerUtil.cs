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
        }

        public static void BuildTestFilesList(List<string> outputList)
        {
            BuildTestFilesList(outputList, string.Empty);
        }

        public static void BuildTestFilesList(List<string> outputList, string extension)
        {
            string currentFolder = TestFolderDeep;

            AddFiles(currentFolder, VisibleFileList, outputList, extension);
            AddFiles(currentFolder, HiddenFileList, outputList, extension);

            currentFolder = Path.Combine(currentFolder, "1");
            AddFiles(currentFolder, VisibleFileList, outputList, extension);
            AddFiles(currentFolder, HiddenFileList, outputList, extension);

            currentFolder = Path.Combine(currentFolder, "1");
            AddFiles(currentFolder, HiddenFileList, outputList, extension);

            currentFolder = Path.Combine(currentFolder, "1");
            AddFiles(currentFolder, VisibleFileList, outputList, extension);
            AddFiles(currentFolder, HiddenFileList, outputList, extension);
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
    }
}
