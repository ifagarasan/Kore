namespace Kore.IO.Util
{
    public interface IFileInfoProvider
    {
        IKoreFileInfo CreateFileInfo(string file);
    }
}