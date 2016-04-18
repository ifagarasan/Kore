using System;
using System.IO;

using KoreCode.Util;

namespace KoreCode.Tests
{
    public delegate void TestFound(string folderPath);

    public class FolderProcessor
    {
        public event TestFound OnTestFound;

        public void ProcessTestFolder(string testFolder)
        {
            if (OnTestFound == null)
                throw new NullReferenceException("Expected OnTestFound to be have event handlers registered");

            if (!Directory.Exists(testFolder))
                throw new IOException("Folder " + testFolder + " was not found");

            foreach (string subfolder in Directory.GetDirectories(testFolder))
                OnTestFound(subfolder);
        }
    }
}