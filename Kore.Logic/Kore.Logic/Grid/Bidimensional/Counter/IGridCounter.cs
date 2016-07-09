namespace Kore.Logic.Grid.Bidimensional.Counter
{
    public interface IGridCounter
    {
        uint Value { get; }

        void Initialise(uint value);
        void Subtract(int v);
    }
}