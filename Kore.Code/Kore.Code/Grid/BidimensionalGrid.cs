using System;
using System.Collections;
using Kore.Code.Exceptions;

namespace Kore.Code.Grid
{
    public class BidimensionalGrid<T>: IGrid
    {
        public uint Rows { get; private set; }
        public uint Columns { get; private set; }

        private readonly T[] _grid;

        public BidimensionalGrid(uint rows, uint columns)
        {
            if (rows == 0 || columns == 0)
                throw  new InvalidQuantityException();

            Rows = rows;
            Columns = columns;

            _grid = new T[rows * columns];
        }

        public T this[uint row, uint column]
        {
            get
            {
                uint index = ComputeIndex(row, column);
                return _grid[index];
            }
            set
            {
                uint index = ComputeIndex(row, column);
                _grid[index] = value;
            }
        }

        private uint ComputeIndex(uint row, uint column)
        {
            return row * Columns + column;
        }

        public IEnumerator GetEnumerator()
        {
            return _grid.GetEnumerator();
        }
    }
}