
namespace Minesweeper_WPF.Core.Abstractions
{
    public interface IGameField
    {
        int Columns { get; set; }

        int Rows { get; set; }

        Cell[,] CellsMatrix { get; }

        Cell GetCell(IPoint point);  
    }
}
