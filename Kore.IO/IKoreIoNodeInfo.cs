namespace Kore.IO
{
    public interface IKoreIoNodeInfo
    {
        string FullName { get; }
        bool Exists { get; }
        string Name { get; }
        long Size { get; }

        void EnsureExists();
        void Delete();
    }
}