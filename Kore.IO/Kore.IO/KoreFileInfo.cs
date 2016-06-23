using Kore.IO.Exceptions;
using Kore.IO.Validation;
using System;
using System.IO;
using System.Threading;

namespace Kore.IO
{
    [Serializable]
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

        public override long Size
        {
            get
            {
                if (!Exists)
                    throw new NodeNotFoundException();

                return new FileInfo(_file).Length;
            }
        }

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

        protected override void DeleteNode()
        {
            File.Delete(FullName);
        }

        protected override void CopyNode(IKoreIoNodeInfo nodeInfo)
        {
            IKoreFileInfo destination = new KoreFileInfo(nodeInfo.FullName);
            destination.FolderInfo.EnsureExists();

            File.Copy(FullName, nodeInfo.FullName, true);
        }
    }
}