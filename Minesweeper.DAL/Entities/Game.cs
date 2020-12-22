using System;
using System.Collections.Generic;
using System.Text;

namespace Minesweeper.DAL.Entities
{
    public partial class Game
    {
        public int Id { get; set; }
        public TimeSpan Duration { get; set; }
        public bool IsWin { get; set; }
        public string Level { get; set; }
        public int IdLevel { get; set; }
        public DateTime Date { get; set; }
        public int IdPlayer { get; set; }

        public virtual Level IdLevelNavigation { get; set; }
        public virtual Player IdPlayerNavigation { get; set; }
    }
}
