using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper_WPF.Models
{
    class StatisticManager
    {
       
        public List<GameLevel> GetLevels()
        {
            return new List<GameLevel>
            {
                new GameLevel("Beginer"),
                new GameLevel("Intermediate"),
                new GameLevel("Advanced"),
            };
        }
        public List<Games> GetGames()
        {
            return new List<Games>
            {
                new Games(new TimeSpan(0,0,125),DateTime.Now),
                new Games(new TimeSpan(0,0,125),DateTime.Now),
                new Games(new TimeSpan(0,0,125),DateTime.Now),
            };
        }
    }
}
