namespace Kore.Logic.Grid.Bidimensional.Distribution
{
    public class UniformDistribution: IDistributionStrategy
    {
        public void Initialise(IBidimensionalGrid<uint> grid, uint value)
        {
            ApplyValueToGrid(grid, value / (grid.Rows * grid.Columns));
            ApplySurplus(grid, value % (grid.Rows * grid.Columns));
        }

        private static void ApplySurplus(IBidimensionalGrid<uint> grid, uint surplus)
        {
            var rowIndex = 0u;
            var columnIndex = 0u;

            while (surplus > 0)
            {
                if (columnIndex == grid.Columns)
                {
                    rowIndex++;
                    columnIndex = 0;
                }

                grid[rowIndex, columnIndex++]++;
                surplus--;
            }
        }

        private static void ApplyValueToGrid(IBidimensionalGrid<uint> grid, uint value)
        {
            for (var i = 0u; i < grid.Rows; ++i)
                for (var j = 0u; j < grid.Columns; ++j)
                    grid[i, j] = value;
        }
    }
} 