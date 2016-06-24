namespace Kore.IO.Sync
{
    public class Diff : IDiff
    {
        public Diff(IKoreFileInfo source, IKoreFileInfo destination, DiffRelation relation, long id)
        {
            Source = source;
            Destination = destination;
            Relation = relation;
            Id = id;
        }

        public DiffRelation Relation { get; }
        public IKoreFileInfo Destination { get; }
        public IKoreFileInfo Source { get; }
        public long Id { get; }
    }
}