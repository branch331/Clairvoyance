using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clairvoyance
{
    public class DayModel
    {
        public DayModel(string day)
        {
            Day = day;
        }

        public int Id { get; set; }
        public string Day { get; set; }
    }
}
