using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexington.Model
{
    internal class Overtime
    {
        public DateTime Date { get; set; }
        public double Hours { get; set; }

        public override string ToString()
        {
            return $"{Date.ToShortDateString()} - {Hours} 小时";
        }
    }
}
