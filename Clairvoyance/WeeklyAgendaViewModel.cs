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
        public double dayRowHeight;

        private const double DAYHEIGHTWORKWEEK = 105;
        private const double DAYHEIGHTFULLWEEK = 75;

        public List<string> monTaskListString;
        public List<string> tuesTaskListString;
        public List<string> wedTaskListString;
        public List<string> thursTaskListString;
        public List<string> friTaskListString;
        public List<string> satTaskListString;
        public List<string> sunTaskListString;

        public List<DayPlannerModel> daysToDisplay;

        private List<DayPlannerModel> fullWeek = new List<DayPlannerModel>();

        public WeeklyAgendaViewModel()
        {
            IsWorkWeek = false;
            fullWeek = generateFullWeekList();
            updateDaysToDisplay();
            updateRowHeight();
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

        public List<string> MonTaskListString
        {
            get { return monTaskListString; }
            set
            {
                if (monTaskListString != value)
                {
                    monTaskListString = value;
                    NotifyPropertyChanged("MonTaskListString");
                }
            }
        }

        public List<string> TuesTaskListString
        {
            get { return tuesTaskListString; }
            set
            {
                if (tuesTaskListString != value)
                {
                    tuesTaskListString = value;
                    NotifyPropertyChanged("TuesTaskListString");
                }
            }
        }

        public List<string> WedTaskListString
        {
            get { return wedTaskListString; }
            set
            {
                if (wedTaskListString != value)
                {
                    wedTaskListString = value;
                    NotifyPropertyChanged("WedTaskListString");
                }
            }
        }

        public List<string> ThursTaskListString
        {
            get { return thursTaskListString; }
            set
            {
                if (thursTaskListString != value)
                {
                    thursTaskListString = value;
                    NotifyPropertyChanged("ThursTaskListString");
                }
            }
        }

        public List<string> FriTaskListString
        {
            get { return friTaskListString; }
            set
            {
                if (friTaskListString != value)
                {
                    friTaskListString = value;
                    NotifyPropertyChanged("FriTaskListString");
                }
            }
        }

        public List<string> SatTaskListString
        {
            get { return satTaskListString; }
            set
            {
                if (satTaskListString != value)
                {
                    satTaskListString = value;
                    NotifyPropertyChanged("SatTaskListString");
                }
            }
        }

        public List<string> SunTaskListString
        {
            get { return sunTaskListString; }
            set
            {
                if (sunTaskListString != value)
                {
                    sunTaskListString = value;
                    NotifyPropertyChanged("SunTaskListString");
                }
            }
        }

        public double DayRowHeight
        {
            get { return dayRowHeight; }
            set
            {
                if (dayRowHeight != value)
                {
                    dayRowHeight = value;
                    NotifyPropertyChanged("DayRowHeight");
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
                updateTaskListStrings();
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

        public void updateTaskListStrings()
        {
            monTaskListString = (convertTaskListToStrings(DaysToDisplay[0].TaskList));
            tuesTaskListString = (convertTaskListToStrings(DaysToDisplay[1].TaskList));
            wedTaskListString = (convertTaskListToStrings(DaysToDisplay[2].TaskList));
            thursTaskListString = (convertTaskListToStrings(DaysToDisplay[3].TaskList));
            friTaskListString = (convertTaskListToStrings(DaysToDisplay[4].TaskList));
            satTaskListString = (convertTaskListToStrings(DaysToDisplay[5].TaskList));
            sunTaskListString = (convertTaskListToStrings(DaysToDisplay[6].TaskList));

            NotifyPropertyChanged("MonTaskListString");
            NotifyPropertyChanged("TuesTaskListString");
            NotifyPropertyChanged("WedTaskListString");
            NotifyPropertyChanged("ThursTaskListString");
            NotifyPropertyChanged("FriTaskListString");
            NotifyPropertyChanged("SatTaskListString");
            NotifyPropertyChanged("SunTaskListString");
        }

        public List<string> convertTaskListToStrings(List<TaskItemModel> taskList)
        {
            string taskString;
            List<string> taskStringList = new List<string>();

            foreach (TaskItemModel task in taskList)
            {
                taskString = String.Format("{0}\n{1}\n{2}\n{3}\n{4}", task.TaskName, task.TaskCategory, task.TaskDescription, task.TaskStartDateTime.TimeOfDay, task.TaskEndDateTime.TimeOfDay);
                taskStringList.Add(taskString);
            }

            return taskStringList;
        }

        public void updateRowHeight()
        {
            if (IsWorkWeek)
            {
                dayRowHeight = DAYHEIGHTWORKWEEK;
            }
            else
            {
                dayRowHeight = DAYHEIGHTFULLWEEK;
            }

            NotifyPropertyChanged("DayRowHeight");
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
                    updateRowHeight();
                }
            }
        }
    }
}
