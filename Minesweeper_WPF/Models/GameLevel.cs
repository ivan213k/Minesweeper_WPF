using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper_WPF.Models
{
    class GameLevel
    {
        public string Name { get; set; }

        public GameLevel(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
