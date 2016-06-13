namespace Kore.IO.Sync
{
    public interface IFolderDiffer
    {
        FolderDiff BuildDiff();
    }
}