using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clairvoyance
{
    public class WeeklyAgendaViewModel
    {
        public WeeklyAgendaViewModel()
        {

        }

        public bool isWorkWeek
        {
            get;
            set;
        }

        public List<DayPlannerModel> DaysToDisplayList
        {
            get;
            set;
        }

        public List<DayPlannerModel> generateDaysToDisplayList()
        {
            if (isWorkWeek)
            {
                return generateWorkWeekList();
            }

            return generateFullWeekList();
        }

        private List<DayPlannerModel> generateWorkWeekList()
        {
            List<DayPlannerModel> workWeekList = new List<DayPlannerModel>();
            workWeekList.Add(new DayPlannerModel("Mon"));
            workWeekList.Add(new DayPlannerModel("Tues"));
            workWeekList.Add(new DayPlannerModel("Wed"));
            workWeekList.Add(new DayPlannerModel("Thurs"));
            workWeekList.Add(new DayPlannerModel("Fri"));

            return workWeekList;
        }

        private List<DayPlannerModel> generateFullWeekList()
        {
            List<DayPlannerModel> fullWeekList = new List<DayPlannerModel>();
            fullWeekList.Add(new DayPlannerModel("Mon"));
            fullWeekList.Add(new DayPlannerModel("Tues"));
            fullWeekList.Add(new DayPlannerModel("Wed"));
            fullWeekList.Add(new DayPlannerModel("Thurs"));
            fullWeekList.Add(new DayPlannerModel("Fri"));
            fullWeekList.Add(new DayPlannerModel("Sat"));
            fullWeekList.Add(new DayPlannerModel("Sun"));

            return fullWeekList;
        }
    }
}
