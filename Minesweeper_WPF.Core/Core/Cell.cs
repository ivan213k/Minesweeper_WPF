using System;

namespace Minesweeper_WPF.Core
{
    public class Cell
    {
        public bool IsBomb { get; private set; }

        public bool IsEmpty { get; set; }

        public byte? Number { get; set; }

        public int RowIndex { get; set; }

        public int ColumnIndex { get; set; }
        public Cell(int rowIndex, int colIndex)
        {
            RowIndex = rowIndex;
            ColumnIndex = colIndex;
            IsEmpty = true;
        }
        public Cell(int rowIndex, int colIndex, bool isBomb)
        {
            RowIndex = rowIndex;
            ColumnIndex = colIndex;
            IsBomb = isBomb;
        }
        public Cell(int rowIndex, int colIndex, byte num)
        {
            RowIndex = rowIndex;
            ColumnIndex = colIndex;
            if (num >=1 && num<=8)
            {
                Number = num;
            }
            else
            {
                throw new ArgumentException("number can not be more then 8 and less then 1");
            }       
        }

        public override string ToString()
        {
            if (IsEmpty)
            {
                return "empty";
            }
            if (IsBomb)
            {
                return "bomb";
            }
            return Number.ToString();
        }
    }
}
