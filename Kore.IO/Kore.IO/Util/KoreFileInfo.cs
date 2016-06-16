using Kore.IO.Validation;
using System;
using System.IO;
using System.Threading;

namespace Kore.IO.Util
{
    public class KoreFileInfo : KoreIoNodeInfo, IKoreFileInfo
    {
        private readonly string _file;

        public KoreFileInfo(string file)
        {
            _file = Path.GetFullPath(file);
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

        public override string FullName => Path.GetFullPath(_file);

        public override bool Exists => File.Exists(_file);

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

        public IKoreIoNodeInfo FolderInfo { get; }

        protected override void EnsureNodeExists()
        {
            FolderInfo.EnsureExists();

            using (FileStream fs = File.Create(FullName)) { }
        }

        protected override void CopyNode(IKoreIoNodeInfo nodeInfo)
        {
            throw new NotImplementedException();
        }
    }
}