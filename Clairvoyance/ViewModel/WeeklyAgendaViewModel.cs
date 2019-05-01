using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Clairvoyance.Data;
using Clairvoyance.Helpers;
using Clairvoyance.Model;

namespace Clairvoyance.ViewModel
{
    public class WeeklyAgendaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ICommand categorySubmitCommand;
        private ICommand taskSubmitCommand;
        
        public string taskItemName;
        public string taskItemDay;
        public string taskItemCategory;
        public string taskItemStartTime;
        public string taskItemEndTime;
        public DateTime weeklyStartDate;
        public DateTime weeklyEndDate;
        private string categoryToAdd;

        public List<string> monTaskListString;
        public List<string> tuesTaskListString;
        public List<string> wedTaskListString;
        public List<string> thursTaskListString;
        public List<string> friTaskListString;
        public List<string> satTaskListString;
        public List<string> sunTaskListString;

        public List<DayPlanner> daysToDisplay;
        private List<DayPlanner> fullWeek = new List<DayPlanner>();
        public ObservableCollection<string> daysOfWeekList = new ObservableCollection<string>();
        public ObservableCollection<string> categoryList = new ObservableCollection<string>();
        public ObservableCollection<WeeklyTotals> weeklyTotalsInHours = new ObservableCollection<WeeklyTotals>();

        public TaskDatabaseLayer taskDbLayer = new TaskDatabaseLayer();

        public WeeklyAgendaViewModel()
        {
            fullWeek = generateFullWeekList();
            MaxDaysInMonthDict = initializeMaxDaysInMonthDict(); 
            updateDaysToDisplay();
            populateCategoryListFromDb();
            populateWeekDayDb();
            populateDaysOfWeekFromDb();
            updateCurrentWeekDateTimes();

            categorySubmitCommand = new RelayCommand(o => 
            {
                try
                {
                    addNewCategoryToList();
                }
                catch (System.ArgumentException e)
                {
                    System.Windows.MessageBox.Show(e.Message);
                }
            }, o => true);

            taskSubmitCommand = new RelayCommand(o => 
            {
                try
                {
                    addTaskToDay();
                }
                catch (System.ArgumentException e)
                {
                    System.Windows.MessageBox.Show(e.Message);
                }
            }, o => true);
        }

        public void WeeklyTotalsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }

        public Dictionary<int, int> initializeMaxDaysInMonthDict()
        {
            Dictionary<int, int> maxDaysInMonthDict = new Dictionary<int, int>()
            {
                {1, 31},
                {2, 28}, //Assumes non-leap year
                {3, 31},
                {4, 30},
                {5, 31},
                {6, 30},
                {7, 31},
                {8, 31},
                {9, 30},
                {10, 31},
                {11, 30},
                {12, 31}
            };

            return maxDaysInMonthDict;
        }

        public DateTime WeeklyStartDate
        {
            get { return weeklyStartDate; }
            set
            {
                if (weeklyStartDate != value)
                {
                    weeklyStartDate = value;
                    NotifyPropertyChanged("WeeklyStartDate");
                }
            }
        }

        public DateTime WeeklyEndDate
        {
            get { return weeklyEndDate; }
            set
            {
                if (weeklyEndDate != value)
                {
                    weeklyEndDate = value;
                    NotifyPropertyChanged("WeeklyEndDate");
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
                    NotifyPropertyChanged("TaskItemName");
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
                    NotifyPropertyChanged("TaskItemDay");
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
                    NotifyPropertyChanged("TaskItemCategory");
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
                    NotifyPropertyChanged("TaskItemStartTime");
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
                    NotifyPropertyChanged("TaskItemEndTime");
                }
            }
        }

        public List<DayPlanner> DaysToDisplay
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

        public ObservableCollection<WeeklyTotals> WeeklyTotalsInHours
        {
            get { return weeklyTotalsInHours; }
        }

        public ICommand CategorySubmitCommand
        {
            get { return categorySubmitCommand; }
            set
            {
                categorySubmitCommand = value;
            }
        }

        public ICommand TaskSubmitCommand
        {
            get { return taskSubmitCommand; }
            set
            {
                taskSubmitCommand = value;
            }
        }

        public ObservableCollection<string> CategoryList
        {
            get { return categoryList; }
            set
            {
                if (categoryList != value)
                {
                    categoryList = value;
                }
            }
        }

        public string CategoryToAdd
        {
            get { return categoryToAdd; }
            set
            {
                if (categoryToAdd != value)
                {
                    categoryToAdd = value;
                    NotifyPropertyChanged("CategoryToAdd");
                }
            }
        }

        public ObservableCollection<string> DaysOfWeekList
        {
            get { return daysOfWeekList; }
            set
            {
                if (daysOfWeekList != value)
                {
                    daysOfWeekList = value;
                    NotifyPropertyChanged("DaysOfWeekList");
                }
            }
        }

        public Dictionary<int, int> MaxDaysInMonthDict
        {
            get;
            set;
        }

        private List<DayPlanner> generateFullWeekList()
        {
            List<DayPlanner> fullWeekList = new List<DayPlanner>();
            fullWeekList.Add(new DayPlanner("Mon"));
            fullWeekList.Add(new DayPlanner("Tues"));
            fullWeekList.Add(new DayPlanner("Wed"));
            fullWeekList.Add(new DayPlanner("Thurs"));
            fullWeekList.Add(new DayPlanner("Fri"));
            fullWeekList.Add(new DayPlanner("Sat"));
            fullWeekList.Add(new DayPlanner("Sun"));

            return fullWeekList;
        }

        public void populateWeekDayDb()
        {
            using (TaskContext taskCtx = new TaskContext())
            {
                if (taskCtx.days.Count() == 0)
                {
                    taskDbLayer.initializeWeekdays();
                }
            }
        }

        public void populateDaysOfWeekFromDb()
        {
            daysOfWeekList = taskDbLayer.getListOfWeekdays();
        }

        public void updateCurrentWeekDateTimes()
        {
            DateTime currentDateTime = DateTime.Now;

            //If there is no range in which the input dateTime exists, returns null.
            Tuple<DateTime, DateTime> weekRange = taskDbLayer.getWeekRangeFromDate(currentDateTime);

            if (weekRange != null)
            {
                weeklyStartDate = weekRange.Item1;
                weeklyEndDate = weekRange.Item2;
            }
            else
            {
                setNewWeekRange(currentDateTime);
            }
        }

        public void setNewWeekRange(DateTime currentDateTime)
        {
            DateTime newSundayDate;
            DateTime newMondayDate;

            int currentDayOfWeek = (int) currentDateTime.DayOfWeek; //Sunday = 0

            int newSundayDay = currentDateTime.Day + (7 - currentDayOfWeek);
            int newSundayMonth = currentDateTime.Month;

            int newMondayDay = currentDateTime.Day - (currentDayOfWeek - 1);
            int newMondayMonth = currentDateTime.Month;

            if (currentDayOfWeek == 0)
            {
                newSundayDate = new DateTime(currentDateTime.Year, newSundayMonth, currentDateTime.Day);
                newMondayDay = currentDateTime.Day - 6;
            }
            else
            {
                if (newSundayDay > MaxDaysInMonthDict[newSundayMonth])
                {
                    newSundayDay -= MaxDaysInMonthDict[newSundayMonth];
                }

                newSundayDate = new DateTime(currentDateTime.Year, newSundayMonth, newSundayDay);
            }

            if (currentDayOfWeek == 1)
            {
                newMondayDate = new DateTime(currentDateTime.Year, newMondayMonth, newMondayDay);
            }
            else
            {
                if (newMondayDay < 0)
                {
                    newMondayMonth -= 1;
                    newMondayDay += MaxDaysInMonthDict[newMondayMonth];
                }

                newMondayDate = new DateTime(currentDateTime.Year, newMondayMonth, newMondayDay);
            }

            weeklyStartDate = newMondayDate;
            weeklyEndDate = newSundayDate;

            Week newWeekEntry = new Week(newMondayDate, newSundayDate);
            taskDbLayer.addNewWeekRange(newWeekEntry);
        }

        public void addTaskToDay()
        {
            validateNewTask();

            int dayIndex = DaysToDisplay
                .FindIndex(x => x.NameOfDay == taskItemDay);

            DaysToDisplay[dayIndex].addTask(taskItemName, taskItemCategory, taskItemStartTime, taskItemEndTime);
            updateTaskListStrings(dayIndex);
            updateWeeklyTotals();

            addTaskToDb();
        }

        public void addTaskToDb()
        {
            TaskItem taskItem = new Model.TaskItem(taskItemName, taskItemCategory, taskItemStartTime, taskItemEndTime);
            taskDbLayer.addTaskItem(taskItem, taskItemDay, weeklyStartDate);
        }

        public bool anyTaskFieldEmpty()
        {
            if (string.IsNullOrWhiteSpace(taskItemName) || 
                string.IsNullOrWhiteSpace(taskItemDay) ||
                string.IsNullOrWhiteSpace(taskItemCategory) ||
                string.IsNullOrWhiteSpace(taskItemStartTime) ||
                string.IsNullOrWhiteSpace(taskItemEndTime))
            {
                return true;
            }

            return false;
        }

        public bool inputTimesWithinRange()
        {
            string startTimeString;
            string endTimeString;
            DateTime dateTimeMin = Convert.ToDateTime("1:00");
            DateTime dateTimeMax = Convert.ToDateTime("12:00");

            if (string.IsNullOrWhiteSpace(taskItemStartTime) || string.IsNullOrWhiteSpace(taskItemEndTime))
            {
                return false;
            }

            if (!taskItemStartTime.Contains(":"))
            {
                startTimeString = taskItemStartTime + ":00"; 
            }
            else
            {
                startTimeString = taskItemStartTime;
            }

            if (!taskItemEndTime.Contains(":"))
            {
                endTimeString = taskItemEndTime + ":00";
            }
            else
            {
                endTimeString = taskItemEndTime;
            }

            try
            {
                DateTime startTime = Convert.ToDateTime(startTimeString);
                DateTime endTime = Convert.ToDateTime(endTimeString);

                if (startTime >= dateTimeMin && startTime <= dateTimeMax && endTime >= dateTimeMin && endTime <= dateTimeMax)
                {
                    return true;
                }
            }
            catch (System.FormatException)
            {
                return false;
            }

            return false;
        }

        public void addNewCategoryToList()
        {
            categoryList.Add(categoryToAdd);
            taskDbLayer.addNewCategory(categoryToAdd);
        }

        public void populateCategoryListFromDb()
        {
            categoryList = taskDbLayer.getExistingCategoryList();
        }

        public void validateNewTask()
        {
            if (DaysToDisplay.Count == 0)
            {
                throw new System.InvalidOperationException("DaysToDisplay has not been initialized.");
            }
            else if (!inputTimesWithinRange())
            {
                throw new System.ArgumentException("Input times must be from 1-12.");
            }
            else if (anyTaskFieldEmpty())
            {
                throw new System.ArgumentException("One or more task fields null or empty.");
            }
        }

        public void updateDaysToDisplay()
        {
            daysToDisplay = fullWeek;

            NotifyPropertyChanged("DaysToDisplay");
        }

        public void updateTaskListStrings(int dayIndex)
        {
            switch (dayIndex)
            {
                case 0:
                    monTaskListString = (convertTaskListToStrings(DaysToDisplay[0].TaskList));
                    NotifyPropertyChanged("MonTaskListString");
                    break;
                case 1:
                    tuesTaskListString = (convertTaskListToStrings(DaysToDisplay[1].TaskList));
                    NotifyPropertyChanged("TuesTaskListString");
                    break;
                case 2:
                    wedTaskListString = (convertTaskListToStrings(DaysToDisplay[2].TaskList));
                    NotifyPropertyChanged("WedTaskListString");
                    break;
                case 3:
                    thursTaskListString = (convertTaskListToStrings(DaysToDisplay[3].TaskList));
                    NotifyPropertyChanged("ThursTaskListString");
                    break;
                case 4:
                    friTaskListString = (convertTaskListToStrings(DaysToDisplay[4].TaskList));
                    NotifyPropertyChanged("FriTaskListString");
                    break;
                case 5:
                    satTaskListString = (convertTaskListToStrings(DaysToDisplay[5].TaskList));
                    NotifyPropertyChanged("SatTaskListString");
                    break;
                case 6:
                    sunTaskListString = (convertTaskListToStrings(DaysToDisplay[6].TaskList));
                    NotifyPropertyChanged("SunTaskListString");
                    break;
            }
        }

        public void updateWeeklyTotals()
        {
            int differenceBetweenLists = CategoryList.Count - weeklyTotalsInHours.Count;
            int dayIndex = DaysToDisplay.FindIndex(x => x.NameOfDay == taskItemDay);
            var recentlyUpdatedTaskList = DaysToDisplay[dayIndex].TaskList;
            double hoursToAdd = recentlyUpdatedTaskList[recentlyUpdatedTaskList.Count - 1].TaskTimeInterval.Hours;

            for (int i = 0; i < differenceBetweenLists; i++)
            {
                weeklyTotalsInHours.Add(new WeeklyTotals(CategoryList[CategoryList.Count - (differenceBetweenLists - i)], 0));
            }

            weeklyTotalsInHours
                .Where(x => x.Category == taskItemCategory)
                .FirstOrDefault()
                .TotalHours += hoursToAdd;
        }

        public List<string> convertTaskListToStrings(List<Model.TaskItem> taskList)
        {
            string taskString;
            List<string> taskStringList = new List<string>();

            foreach (Model.TaskItem task in taskList)
            {
                taskString = string.Format("{0}\n{1}\n{2}\n{3}", task.TaskName, task.TaskCategory, task.TaskStartDateTime.TimeOfDay, task.TaskEndDateTime.TimeOfDay);
                taskStringList.Add(taskString);
            }

            return taskStringList;
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
