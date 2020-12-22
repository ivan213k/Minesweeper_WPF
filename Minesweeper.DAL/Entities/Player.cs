using System;
using System.Collections.Generic;
using System.Text;

namespace Minesweeper.DAL.Entities
{
    public partial class Player
    {
        public Player()
        {
            Games = new HashSet<Game>();
        }

        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public int IdInfo { get; set; }

        public virtual Info IdInfoNavigation { get; set; }
        public virtual ICollection<Game> Games { get; set; }
    }
}
