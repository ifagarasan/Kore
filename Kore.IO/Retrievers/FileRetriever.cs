using System.Collections.Generic;
using System.IO;
using static Kore.Validation.ObjectValidation;

namespace Kore.IO.Retrievers
{
    public delegate void FileFoundDelegate(IKoreFileInfo file);

    public class FileRetriever : IFileRetriever
    {
        public event FileFoundDelegate FileFound;

        public List<IKoreFileInfo> GetFiles(IKoreFolderInfo folder)
        {
            IsNotNull(folder);

            var fileInfos = new List<IKoreFileInfo>();

            ScanFolder(folder, fileInfos);

            return fileInfos;

        }

        private void ScanFolder(IKoreFolderInfo folder, List<IKoreFileInfo> fileInfos)
        {
            ScanFiles(folder, fileInfos);

            foreach (var subFolder in Directory.GetDirectories(folder.FullName))
                ScanFolder(new KoreFolderInfo(subFolder), fileInfos);
        }

        private void ScanFiles(IKoreFolderInfo folder, List<IKoreFileInfo> fileInfos)
        {
            foreach (var file in Directory.GetFiles(folder.FullName, "*", SearchOption.TopDirectoryOnly))
            {
                var fileInfo = new KoreFileInfo(file);

                fileInfos.Add(fileInfo);

                OnFileFound(fileInfo);
            }
        }

        protected virtual void OnFileFound(IKoreFileInfo file)
        {
            FileFound?.Invoke(file);
        }
    }
}