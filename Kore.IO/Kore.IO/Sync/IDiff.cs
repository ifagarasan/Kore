namespace Kore.IO.Sync
{
    public interface IDiff
    {
        IKoreFileInfo Destination { get; }
        IKoreFileInfo Source { get; }
        DiffRelation Relation { get; }
        long ID { get; }
    }
}