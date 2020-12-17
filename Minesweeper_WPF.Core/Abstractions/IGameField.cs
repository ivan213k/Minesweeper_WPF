
using System.Collections.Generic;

namespace Minesweeper_WPF.Core.Abstractions
{
    public interface IGameField
    {
        int Columns { get; }

        int Rows { get; }

        Cell GetCell(IPoint point);

        List<Cell> GetEmptyCellsAround(IPoint point);
    }
}
