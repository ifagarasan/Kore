namespace Kore.IO.Sync
{
    public interface IFolderDiffer
    {
        IFolderDiff BuildDiff();
    }
}