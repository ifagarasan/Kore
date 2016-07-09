using System.Collections;
using Kore.Exceptions;

namespace Kore.Logic.Grid.Bidimensional
{
    public class BidimensionalGrid<T> : IGrid, IBidimensionalGrid<T>
    {
        public uint Rows { get; private set; }
        public uint Columns { get; }

        private readonly T[] _grid;

        public BidimensionalGrid(uint rows, uint columns)
        {
            if (rows == 0 || columns == 0)
                throw new InvalidQuantityException();

            Rows = rows;
            Columns = columns;

            _grid = new T[rows * columns];
        }

        public T this[uint row, uint column]
        {
            get
            {
                return _grid[ComputeIndex(row, column)];
            }
            set
            {
                _grid[ComputeIndex(row, column)] = value;
            }
        }

        private uint ComputeIndex(uint row, uint column) => row*Columns + column;

        public IEnumerator GetEnumerator() => _grid.GetEnumerator();
    }
}