using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clairvoyance.Model
{
    public class Day
    {
        private Day()
        {

        }

        public Day(string dayName)
        {
            DayName = dayName;
        }

        public int Id { get; set; }
        public string DayName { get; set; }
    }
}
