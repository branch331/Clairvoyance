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
        public bool isWorkWeek;
        public List<DayPlannerModel> daysToDisplay;

        private List<DayPlannerModel> fullWeek = new List<DayPlannerModel>();

        public WeeklyAgendaViewModel()
        {
            IsWorkWeek = true;
            fullWeek = generateFullWeekList();
            updateDaysToDisplay();
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
            get { return daysToDisplay; }
            set
            {
                if (daysToDisplay != value)
                {
                    daysToDisplay = value;
                    NotifyPropertyChanged("DaysToDisplay");
                }
            }
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

        public void updateDaysToDisplay()
        {
            if (IsWorkWeek)
            {
                daysToDisplay = returnWorkWeek();
            }
            else
            {
                daysToDisplay = fullWeek;
            }

            NotifyPropertyChanged("DaysToDisplay");
        }

        public List<DayPlannerModel> returnWorkWeek()
        {
            List<DayPlannerModel> workWeek = new List<DayPlannerModel>();
            for (int i = 0; i < 5; i++)
            {
                workWeek.Add(fullWeek[i]);
            }

            return workWeek;
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
