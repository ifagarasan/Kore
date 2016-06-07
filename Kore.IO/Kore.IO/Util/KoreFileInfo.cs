using System;
using System.IO;

namespace Kore.IO.Util
{
    public class KoreFileInfo : IKoreFileInfo
    {
        readonly FileInfo _fileInfo;

        public KoreFileInfo(string file)
        {
            _fileInfo = new FileInfo(file);
        }

        public bool Hidden
        {
            get
            {
                return _fileInfo.Attributes.HasFlag(FileAttributes.Hidden);
            }
            set
            {
                File.SetAttributes(_fileInfo.FullName, value ? FileAttributes.Hidden : FileAttributes.Normal);
            }
        }

        public string FullName => _fileInfo.FullName;

        public bool Exists => _fileInfo.Exists;

        public string DirectoryFullName => _fileInfo.DirectoryName;

        public DateTime LastWriteTime
        {
            get
            {
                ValidateExistance();
                return File.GetLastWriteTime(_fileInfo.FullName);
            }

            set
            {
                ValidateExistance();
                File.SetLastWriteTime(_fileInfo.FullName, value);
            }
        }

        public void EnsureDirectoryExists()
        {
            if (!Directory.Exists(DirectoryFullName))
                Directory.CreateDirectory(DirectoryFullName);
        }

        public void EnsureExits()
        {
            if (Exists)
                return;

            EnsureDirectoryExists();
            File.Create(FullName);
        }

        private void ValidateExistance()
        {
            if (!Exists)
                throw new FileNotFoundException();
        }
    }
}