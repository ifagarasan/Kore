namespace Kore.IO.Util
{
    public class FileInfoProvider : IFileInfoProvider
    {
        public IKoreFileInfo GetFileInfo(string file)
        {
            return new KoreFileInfo(file);
        }
    }
}