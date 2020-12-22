using System;
using System.Collections.Generic;
using System.Text;

namespace Minesweeper.DAL.Entities
{
    public partial class Level
    {
        public Level()
        {
            Games = new HashSet<Game>();
        }

        public int Id { get; set; }
        public string Complexity { get; set; }
        public int BombsCount { get; set; }
        public int SizeHeight { get; set; }
        public int SizeWidth { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
