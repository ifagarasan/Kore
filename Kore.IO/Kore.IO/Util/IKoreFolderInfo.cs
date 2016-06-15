namespace Kore.IO.Util
{
    public interface IKoreFolderInfo
    {
        string FullName { get; }
        bool Exists { get; }
        void EnsureExists();
    }
}