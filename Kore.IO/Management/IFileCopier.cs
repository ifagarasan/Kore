namespace Kore.IO.Management
{
    public interface IFileCopier
    {
        void Copy(IKoreFileInfo source, IKoreFileInfo destination);
    }
}