using Minesweeper.DAL.Entities;
using Minesweeper_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Minesweeper_WPF.ViewModels
{
    class StatisticsViewModel : BaseViewModel
    {
        private StatisticManager statisticManager = new StatisticManager();
        private Level selectedLevel;
        private List<Games> games;
        private double wonGamePercent;
        private int playedGamesCount;
        private int wonGamesCount = 2;

        public StatisticsViewModel()
        {
            Levels = statisticManager.GetLevels();
            SelectedLevel = Levels.FirstOrDefault();
            Games = statisticManager.GetWonGames(SelectedLevel.Id);
            UserName = statisticManager.GetUserNickName();
            ResetCommand = new Command(Reset);
        }
        public int PlayedGamesCount { get => playedGamesCount; set { playedGamesCount = value; OnePropertyChanged(); } }

        public int WonGamesCount { get => wonGamesCount; set { wonGamesCount = value; OnePropertyChanged(); } }
        public double WonGamePercent { get => wonGamePercent; set { wonGamePercent = value; OnePropertyChanged(); } }
        public string UserName { get; set; }

        public List<Level> Levels { get; set; }

        public Level SelectedLevel
        {
            get => selectedLevel;
            set
            {
                selectedLevel = value;
                Games = statisticManager.GetWonGames(value.Id);
                RefreshStatistic();
            }
        }

        public List<Games> Games { get => games; set { games = value; OnePropertyChanged(); } }

        private double CalculateWonPercent()
        {
            if (PlayedGamesCount == 0)
            {
                return 0;
            }
            return Math.Round((double)WonGamesCount / (double)PlayedGamesCount * 100);
        }
        private void RefreshStatistic()
        {
            PlayedGamesCount = statisticManager.GetTotalGamesCount(SelectedLevel.Id);
            WonGamesCount = Games.Count();
            WonGamePercent = CalculateWonPercent();
        }

        public ICommand ResetCommand { get; set; }

        private async void Reset(object parametr)
        {
            if (MessageBox.Show("Дійсно видалити всю статистику","",MessageBoxButton.YesNo,MessageBoxImage.Question)== MessageBoxResult.Yes)
            {
                await statisticManager.ResetStatistic();
                PlayedGamesCount = 0;
                WonGamesCount = 0;
                WonGamePercent = 0;
            }
            
        }
    }
}
