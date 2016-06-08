namespace Kore.IO.Util
{
    public class FileInfoProvider : IFileInfoProvider
    {
        public IKoreFileInfo CreateFileInfo(string file)
        {
            return new KoreFileInfo(file);
        }
    }
}