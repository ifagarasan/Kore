namespace Kore.IO.Sync
{
    public enum DiffType
    {
        Identical,
        SourceNew,
        SourceNewer,
        SourceOlder,
        DestinationOrphan
    }
}