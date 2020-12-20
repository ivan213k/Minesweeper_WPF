using Minesweeper_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper_WPF.ViewModels
{
    class StatisticsViewModel : BaseViewModel
    {
        public int PlayedGamesCount { get; set; } = 3;

        public int WonGamesCount { get; set; } = 2;

        public int WonGamePercent { get => WonGamesCount / PlayedGamesCount * 100; }
        public List<GameLevel> Levels { get; set; }

        public List<Games> Games { get; set; }

        private StatisticManager statisticManager = new StatisticManager();

        public string UserName { get; set; } = "ivan213k";
        public StatisticsViewModel()
        {
            Levels = statisticManager.GetLevels();
            Games = statisticManager.GetGames();
        }
    }
}
