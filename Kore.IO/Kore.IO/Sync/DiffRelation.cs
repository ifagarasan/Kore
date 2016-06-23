namespace Kore.IO.Sync
{
    public enum DiffRelation
    {
        Identical,
        SourceNew,
        SourceNewer,
        SourceOlder,
        DestinationOrphan
    }
}