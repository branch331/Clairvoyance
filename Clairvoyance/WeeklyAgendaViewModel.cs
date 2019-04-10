using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Clairvoyance
{
    public class WeeklyAgendaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public bool isWorkWeek; //Change to private

        private IEnumerable<DayPlannerModel> fullWorkWeek = new List<DayPlannerModel>();

        public WeeklyAgendaViewModel()
        {
            isWorkWeek = false;
        }

        public bool IsWorkWeek
        {
            get { return isWorkWeek; }
            set
            {
                if (isWorkWeek != value)
                {
                    isWorkWeek = value;
                    NotifyPropertyChanged("IsWorkWeek");
                }
            }
        }

        public List<DayPlannerModel> DaysToDisplay
        {
            get;
            set;
        }

        public void initializeDaysToDisplayList()
        {
            DaysToDisplay = generateDaysToDisplayList();
        }

        public List<DayPlannerModel> generateDaysToDisplayList()
        {
            if (IsWorkWeek)
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

        public void addTaskToDay(string taskName, string day)
        {
            int dayIndex = DaysToDisplay.FindIndex(x => x.NameOfDay == day);
            DaysToDisplay[dayIndex].addTask(taskName);
        }

        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
