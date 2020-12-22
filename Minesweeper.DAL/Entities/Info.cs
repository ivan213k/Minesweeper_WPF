using System;
using System.Collections.Generic;
using System.Text;

namespace Minesweeper.DAL.Entities
{
    public partial class Info
    {
        public Info()
        {
            Players = new HashSet<Player>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public string Country { get; set; }
        public int Age { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}
