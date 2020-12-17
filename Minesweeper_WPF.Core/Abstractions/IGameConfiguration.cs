
namespace Minesweeper_WPF.Core.Abstractions
{
    public interface IGameConfiguration
    {
        int Rows { get; set; }

        int Columns { get; set; }

        int BombsCount { get; set; }
    }
}
