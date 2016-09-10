namespace Kore.IO.Management
{
    public interface IFileManager
    {
        void Copy(IKoreFileInfo source, IKoreFileInfo destination);
    }
}