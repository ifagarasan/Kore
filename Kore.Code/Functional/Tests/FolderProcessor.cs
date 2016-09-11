using System;
using System.IO;

namespace Kore.Code.Functional.Tests
{
    public class FolderProcessor
    {
        public event TestFound OnTestFound;

        public void ProcessTestFolder(string testFolder)
        {
            if (OnTestFound == null)
                throw new NullReferenceException("Expected OnTestFound to be have event handlers registered");

            if (!Directory.Exists(testFolder))
                throw new IOException("Folder " + testFolder + " was not found");

            foreach (var subfolder in Directory.GetDirectories(testFolder))
                OnTestFound(subfolder);
        }
    }
}