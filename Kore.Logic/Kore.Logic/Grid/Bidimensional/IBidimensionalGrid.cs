namespace Kore.Logic.Grid.Bidimensional
{
    public interface IBidimensionalGrid<T>: IGrid
    {
        T this[uint row, uint column] { get; set; }

        uint Columns { get; }
        uint Rows { get; }
    }
}