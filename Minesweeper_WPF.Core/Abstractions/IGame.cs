using System;

namespace Minesweeper_WPF.Core.Abstractions
{
    public interface IGame
    {
        event EventHandler OnGameOver;

        event EventHandler OnGameWin;
        void Start();
        void FinishGame();
        void OpenCell(IPoint point);
        void MarkCellAsBomb(IPoint point);
       
    }
}
