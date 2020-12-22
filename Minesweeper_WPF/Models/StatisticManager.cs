using Minesweeper.DAL.Entities;
using Minesweeper.DAL.Interfaces;
using Minesweeper.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper_WPF.Models
{
    class StatisticManager
    {
        UserManager userManager = new UserManager();
        private IRepository<Level> levelsRepository;
        private IRepository<Game> gamesRepository;
        private IRepository<Player> playersRepository;
        private List<Game> allGames;
        private int playerId;
        public StatisticManager()
        {
            levelsRepository = new LevelsRepository();
            gamesRepository = new GamesRepository();
            playersRepository = new PlayersRepository();
            playerId = userManager.GetCurrentUserId();
            allGames = gamesRepository.Find(r => r.IdPlayer == playerId).ToList();
        }  
        public List<Level> GetLevels()
        {
           return levelsRepository.GetAll().ToList();
        }
        public List<Games> GetWonGames(int levelId)
        {
            List<Games> games = new List<Games>();
            foreach (var item in allGames.Where(r=>r.IdLevel == levelId && r.IsWin).ToList().OrderBy(r=>r.Duration))
            {
                games.Add(new Games(item.Duration, item.Date));
            }
            return games;
        }
        public int GetTotalGamesCount(int levelId)
        {
            return allGames.Where(r => r.IdLevel == levelId).Count();
        }
        public async Task AddGame(TimeSpan duration, bool isWin, int levelId)
        {
            Game game = new Game
            {
                Date = DateTime.Now,
                Duration = duration,
                IsWin = isWin,
                IdLevel = levelId,
                IdPlayer = playerId,
                Level = ""
            };
            await gamesRepository.CreateAsync(game);
        }
        public string GetUserNickName()
        {
            return playersRepository.Find(r => r.Id == playerId).First().Nickname;
        }
        public async Task ResetStatistic()
        {
            var games = gamesRepository.GetAll().Where(r => r.IdPlayer == playerId).ToList();
            allGames.Clear();
            foreach (var item in games)
            {
                await gamesRepository.DeleteAsync(item.Id);
            }
        }
    }
}
