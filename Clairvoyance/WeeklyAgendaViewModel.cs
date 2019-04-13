using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;

namespace Clairvoyance
{
    public class WeeklyAgendaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool isWorkWeek;
        public string taskItemName;
        public string taskItemDay;
        public string taskItemCategory;
        public string taskItemDescription;
        public string taskItemStartTime;
        public string taskItemEndTime;

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

        public string TaskItemName
        {
            get { return taskItemName; }
            set
            {
                if( taskItemName != value)
                {
                    taskItemName = value;
                }
            }
        }

        public string TaskItemDay
        {
            get { return taskItemDay; }
            set
            {
                if (taskItemDay != value)
                {
                    taskItemDay = value;
                }
            }
        }

        public string TaskItemCategory
        {
            get { return taskItemCategory; }
            set
            {
                if (taskItemCategory != value)
                {
                    taskItemCategory = value;
                }
            }
        }

        public string TaskItemDescription
        {
            get { return taskItemDescription; }
            set
            {
                if (taskItemDescription != value)
                {
                    taskItemDescription = value;
                }
            }
        }

        public string TaskItemStartTime
        {
            get { return taskItemStartTime; }
            set
            {
                if (taskItemStartTime != value)
                {
                    taskItemStartTime = value;
                }
            }
        }

        public string TaskItemEndTime
        {
            get { return taskItemEndTime; }
            set
            {
                if (taskItemEndTime != value)
                {
                    taskItemEndTime = value;
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

        public void addTaskToDay()
        {
            if (DaysToDisplay.Count == 0)
            {
                throw new System.InvalidOperationException("DaysToDisplay has not been initialized.");
            }
            else
            {
                int dayIndex = DaysToDisplay.FindIndex(x => x.NameOfDay == taskItemDay);
                DaysToDisplay[dayIndex].addTask(taskItemName, taskItemCategory, taskItemDescription, taskItemStartTime, taskItemEndTime);
            }
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

                if (propertyName == "IsWorkWeek")
                {
                    updateDaysToDisplay();
                }
            }
        }
    }
}
