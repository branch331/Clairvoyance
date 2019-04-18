using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clairvoyance
{
    public class WeeklyTotals
    {
        public WeeklyTotals(string category, double totalHours)
        {
            Category = category;
            TotalHours = totalHours;
        }
        public string Category
        {
            get;
            set;
        }

        public double TotalHours
        {
            get;
            set;
        }
    }
}
