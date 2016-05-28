namespace Kore.IO.Util
{
    public interface IKoreFileInfo
    {
        bool Hidden { get; }
        string FullName { get; }
        bool Exists { get; }
        string DirectoryFullName { get; }
    }
}