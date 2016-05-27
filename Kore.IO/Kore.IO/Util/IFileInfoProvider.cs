namespace Kore.IO.Util
{
    public interface IFileInfoProvider
    {
        IKoreFileInfo GetFileInfo(string file);
    }
}