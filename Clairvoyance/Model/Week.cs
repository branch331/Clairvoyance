using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clairvoyance.Model
{
    public class Week
    {
        public Week()
        {

        }

        public Week (DateTime mondayDate, DateTime sundayDate)
        {
            MondayDate = mondayDate;
            SundayDate = sundayDate;
        }

        public int Id { get; set; }
        public DateTime MondayDate { get; set; }
        public DateTime SundayDate { get; set; }


    }
}
