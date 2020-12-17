using Minesweeper_WPF.Core.Abstractions;

namespace Minesweeper_WPF.Core.Core
{
    public class Point : IPoint
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
