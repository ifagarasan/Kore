using Kore.IO.Validation;
using System;
using System.IO;
using System.Threading;

namespace Kore.IO.Util
{
    public class KoreFileInfo : IKoreFileInfo
    {
        private readonly string _file;

        public KoreFileInfo(string file)
        {
            _file = file;
            FolderInfo = new KoreFolderInfo(Path.GetDirectoryName(_file));
        }

        public bool Hidden
        {
            get
            {
                FileValidation.Exists(this);
                return File.GetAttributes(_file).HasFlag(FileAttributes.Hidden);
            }
            set
            {
                File.SetAttributes(_file, value ? FileAttributes.Hidden : FileAttributes.Normal);
            }
        }

        public string FullName => Path.GetFullPath(_file);

        public bool Exists => File.Exists(_file);

        public DateTime LastWriteTime
        {
            get
            {
                FileValidation.Exists(this);
                return File.GetLastWriteTime(_file);
            }

            set
            {
                FileValidation.Exists(this);
                File.SetLastWriteTime(_file, value);
            }
        }

        public IKoreFolderInfo FolderInfo { get; }

        public void EnsureExists()
        {
            if (Exists)
                return;

            FolderInfo.EnsureExists();

            using (FileStream fs = File.Create(FullName)) { }
        }

        public void Copy(IKoreIoNodeInfo nodeInfo)
        {
            throw new NotImplementedException();
        }
    }
}