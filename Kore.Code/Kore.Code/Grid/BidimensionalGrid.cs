using System;

namespace Kore.Code.Grid
{
    public class BidimensionalGrid<T>
    {
        public uint Rows { get; private set; }
        public uint Columns { get; private set; }

        public BidimensionalGrid(uint rows, uint columns)
        {
            Rows = rows;
            Columns = columns;
        }

        public void SaveValue(uint row, uint column, int currentValue)
        {
            throw new NotImplementedException();
        }

        public int RetrieveValue(uint row, uint column)
        {
            throw new NotImplementedException();
        }
    }
}