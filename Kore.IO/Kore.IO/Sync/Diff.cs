namespace Kore.IO.Sync
{
    public class Diff : IDiff
    {
        public Diff(IKoreFileInfo source, IKoreFileInfo destination, DiffRelation relation)
        {
            Source = source;
            Destination = destination;
            Relation = relation;
        }

        public DiffRelation Relation { get; }
        public IKoreFileInfo Destination { get; }
        public IKoreFileInfo Source { get; }
    }
}