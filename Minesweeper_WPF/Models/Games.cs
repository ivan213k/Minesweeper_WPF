using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper_WPF.Models
{
    class Games
    {
        public TimeSpan Duration { get; set; }

        public DateTime Date { get; set; }

        public string DateString { get => Date.ToShortDateString(); }
        public Games(TimeSpan duration, DateTime date)
        {
            Duration = duration;
            Date = date;
        }
    }
}
