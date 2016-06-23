namespace Kore.IO.Sync
{
    public interface IDiff
    {
        IKoreFileInfo DestinationFileInfo { get; }
        IKoreFileInfo SourceFileInfo { get; }
        DiffType Type { get; }
    }
}