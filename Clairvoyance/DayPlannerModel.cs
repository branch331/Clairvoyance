using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clairvoyance
{
    public class DayPlannerModel
    {
        public DayPlannerModel(string dayOfWeek)
        {
            NameOfDay = dayOfWeek;
        }

        public string NameOfDay
        {
            get;
            set;
        }
    }
}
