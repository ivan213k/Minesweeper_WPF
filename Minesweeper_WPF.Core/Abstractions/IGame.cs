using System.Collections.Generic;

namespace Minesweeper_WPF.Core.Abstractions
{
    public delegate void GameWin();
    public delegate void GameOver(Cell bombedCell);
    public interface IGame
    {
        event GameOver OnGameOver;

        event GameWin OnGameWin;
        List<Cell> OpenCell(IPoint point);
        void MarkCellAsBomb(IPoint point);
        void RemoveBombMark(IPoint point);
    }
}
