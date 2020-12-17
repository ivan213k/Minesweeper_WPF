using Minesweeper_WPF.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minesweeper_WPF.Core.Core
{
    public class GameField : IGameField
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        private int bombsCount;

        public GameField(int rows, int columns, int bombsCount)
        {
            Rows = rows;
            Columns = columns;
            this.bombsCount = bombsCount;
            CellsMatrix = new Cell[rows, columns];
            InitializeGameField();
        }
        private Cell[,] CellsMatrix;

        public Cell GetCell(IPoint point)
        {
            if (point.X>Rows || point.X<0)
            {
                throw new ArgumentException("Invalid parametr");
            }
            if (point.Y > Columns || point.Y < 0)
            {
                throw new ArgumentException("Invalid parametr");
            }
            return CellsMatrix[point.X, point.Y];
        }

        private void InitializeGameField()
        {
            SetEmptyCells(Rows, Columns);
            var bombCoordinates = SetBombs(bombsCount, Rows, Columns);
            SetNumbers(bombCoordinates, Rows, Columns);
        }
        private void SetEmptyCells(int rows, int cols)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    CellsMatrix[i, j] = new Cell(i, j);
                }
            }
        }
        private List<Point> SetBombs(int count, int maxRow, int maxCol)
        {
            List<Point> randomCoordinates = GetRandomCoordinates(count, maxRow, maxCol);
            foreach (var coordinate in randomCoordinates)
            {
                SetBomb(coordinate);
            }
            return randomCoordinates;
        }
        private void SetBomb(Point point)
        {
            CellsMatrix[point.X, point.Y] = new Cell(point.X, point.Y, isBomb:true);
        }
        private List<Point> GetRandomCoordinates(int count, int maxRow, int maxCol)
        {
            List<Point> randomCoordinates = new List<Point>();
            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                int randRow;
                int randCol;
                do
                {
                    randRow = rand.Next(0, maxRow);
                    randCol = rand.Next(0, maxCol);
                } 
                while (randomCoordinates.Any(r=>r.X == randRow && r.Y == randCol));
           
                randomCoordinates.Add(new Point(randRow, randCol));
            }
            return randomCoordinates;
        }
        private void SetNumbers(List<Point> bombCoordinates, int maxRow, int maxColumn)
        {
            foreach (var bombCoordinate in bombCoordinates)
            {
                //проходим всі клітинки по периметру бомби
                for (int i = bombCoordinate.X - 1; i <= bombCoordinate.X + 1; i++)
                {
                    for (int j = bombCoordinate.Y - 1; j <= bombCoordinate.Y + 1; j++)
                    {
                        //пропускаєм клітинки що виходять за межі поля
                        if (i < 0 || j < 0 || i > maxRow - 1 || j > maxColumn - 1)
                        {
                            continue;
                        }
                        if (CellsMatrix[i, j].IsBomb)
                        {
                            continue;
                        }
                        if (CellsMatrix[i, j].Number != null)
                        {
                            CellsMatrix[i, j].Number++;
                        }
                        else
                        {
                            CellsMatrix[i, j].Number = 1;
                            CellsMatrix[i, j].IsEmpty = false;
                        }
                    }
                }
            }
        }
    }
}
