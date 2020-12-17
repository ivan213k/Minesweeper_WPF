using System;

namespace Minesweeper_WPF.Core
{
    public class Cell
    {
        public bool IsBomb { get; private set; }

        public bool IsEmpty { get; private set; }

        public byte? Number { get; private set; }

        public Cell()
        {
            IsEmpty = true;
        }
        public Cell(bool isBomb)
        {
            IsBomb = isBomb;
        }
        public Cell(byte num)
        {
            if (num >=1 && num<=8)
            {
                Number = num;
            }
            else
            {
                throw new ArgumentException("number can not be more then 8 and less then 1");
            }
            
        }
    }
}
