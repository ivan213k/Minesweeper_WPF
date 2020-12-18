using Minesweeper_WPF.Core.Abstractions;

namespace Minesweeper_WPF.Core.Core
{
    public class GameConfiguration : IGameConfiguration
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int BombsCount { get; set; }

        public GameConfiguration()
        {
            Rows = 9;
            Columns = 9;
            BombsCount = 10;
        }
        public GameConfiguration(int rows, int cols, int bombsCount)
        {
            Rows = rows;
            Columns = cols;
            BombsCount = bombsCount;
        }
    }
}
