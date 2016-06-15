namespace Kore.IO.Util
{
    public interface IKoreFolderInfo
    {
        string FullName { get; }
        void EnsureExists();
    }
}