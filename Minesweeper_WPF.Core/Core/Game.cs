using Minesweeper_WPF.Core.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Minesweeper_WPF.Core.Core
{ 
    public class Game : IGame
    {
        public event GameOver OnGameOver;
        public event GameWin OnGameWin;

        private IGameField gameField;
        private int bombsCount;
        private List<Cell> cellsMarkedAsBomb = new List<Cell>();
        public Game(IGameConfiguration gameConfiguration)
        {
            gameField = new GameField(gameConfiguration.Rows,gameConfiguration.Columns,gameConfiguration.BombsCount);
            bombsCount = gameConfiguration.BombsCount;
        }
        public void MarkCellAsBomb(IPoint point)
        {
            var cell = gameField.GetCell(point);
            if (!cellsMarkedAsBomb.Contains(cell))
            {
                cellsMarkedAsBomb.Add(cell);
                CheckForWin();
            }
        }
        public void RemoveBombMark(IPoint point)
        {
            var cell = cellsMarkedAsBomb.Find(r => r.RowIndex == point.X && r.ColumnIndex == point.Y);
            if (cell != null)
            {
                cellsMarkedAsBomb.Remove(cell);
                CheckForWin();
            }
        }

        public List<Cell> OpenCell(IPoint point)
        {
            var cell = gameField.GetCell(point);
            var cells = new List<Cell>();
            if (cell.IsBomb)
            {
                OnGameOver?.Invoke(cell);
                return cells;
            }
            cells.Add(cell);
            if (cell.IsEmpty)
            {
                cells.AddRange(gameField.GetEmptyCellsAround(new Point(cell.RowIndex, cell.ColumnIndex)));
            }
            return cells;
        }

        private void CheckForWin()
        {
            if (cellsMarkedAsBomb.Count == bombsCount && cellsMarkedAsBomb.All(r=>r.IsBomb))
            {
                OnGameWin?.Invoke();
            }
        }
    }
}
