using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Clairvoyance
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

        public List<DayPlannerModel> daysToDisplay;
        private List<DayPlannerModel> fullWeek = new List<DayPlannerModel>();
        public ObservableCollection<string> daysOfWeekList = new ObservableCollection<string>();
        public ObservableCollection<string> categoryList = new ObservableCollection<string>();
        public ObservableCollection<WeeklyTotals> weeklyTotalsInHours = new ObservableCollection<WeeklyTotals>();

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
                    addNewCategoryToDb();
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
                    //addTaskToDay();
                    addTaskToDb();
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
            Dictionary<int, int> moxDaysInMonthDict = new Dictionary<int, int>()
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

            return moxDaysInMonthDict;
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

        public void populateWeekDayDb()
        {
            using (TaskContext taskCtx = new TaskContext())
            {
                if (taskCtx.days.Count() == 0)
                {
                    taskCtx.days.Add(new DayModel("Mon"));
                    taskCtx.days.Add(new DayModel("Tues"));
                    taskCtx.days.Add(new DayModel("Wed"));
                    taskCtx.days.Add(new DayModel("Thurs"));
                    taskCtx.days.Add(new DayModel("Fri"));
                    taskCtx.days.Add(new DayModel("Sat"));
                    taskCtx.days.Add(new DayModel("Sun"));

                    taskCtx.SaveChanges();
                }
            }
        }

        public void populateDaysOfWeekFromDb()
        {
            using (TaskContext taskCtx = new TaskContext())
            {
                foreach (DayModel item in taskCtx.days)
                {
                    daysOfWeekList.Add(item.Day);
                }
            }
        }

        public void updateCurrentWeekDateTimes()
        {
            DateTime currentDateTime = DateTime.Now;
            DateTime defaultDateTime = new DateTime(1995, 5, 15);
            weeklyStartDate = defaultDateTime;
            weeklyEndDate = defaultDateTime;

            using (TaskContext taskCtx = new TaskContext())
            {
                foreach (var item in taskCtx.weeks)
                {
                    if (currentDateTime > item.MondayDate && currentDateTime < item.SundayDate)
                    {
                        weeklyStartDate = item.MondayDate;
                        weeklyEndDate = item.SundayDate;
                    }
                }
            }

            if (weeklyStartDate == defaultDateTime)
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

                WeekModel newWeekEntry = new WeekModel(newMondayDate, newSundayDate);

                using (TaskContext taskCtx = new TaskContext())
                {
                    taskCtx.weeks.Add(newWeekEntry);
                    taskCtx.SaveChanges();
                }
            }
        }

        public void addTaskToDay()
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
            else
            {
                int dayIndex = DaysToDisplay.FindIndex(x => x.NameOfDay == taskItemDay);

                DaysToDisplay[dayIndex].addTask(taskItemName, taskItemCategory, taskItemStartTime, taskItemEndTime);
                updateTaskListStrings(dayIndex);
                updateWeeklyTotals();
            }
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
            if (string.IsNullOrWhiteSpace(categoryToAdd))
            {
                throw new System.ArgumentException("Category field must be a non-null value.");
            }

            if (!categoryList.Contains(categoryToAdd))
            {
                categoryList.Add(categoryToAdd);
            }
        }

        public void addNewCategoryToDb()
        {
            if (string.IsNullOrWhiteSpace(categoryToAdd))
            {
                throw new System.ArgumentException("Category field must be a non-null value.");
            }

            using (TaskContext taskCtx = new TaskContext())
            {
                if (!taskCtx.categories.Any(item => item.Category == categoryToAdd))
                {
                    taskCtx.categories.Add(new CategoryModel(categoryToAdd));
                    taskCtx.SaveChanges();

                    categoryList.Add(categoryToAdd);
                }
            }
        }

        public void populateCategoryListFromDb()
        {
            ObservableCollection<string> categoryStrings = new ObservableCollection<string>();

            using (TaskContext taskCtx = new TaskContext())
            {
                foreach (var item in taskCtx.categories)
                {
                    categoryStrings.Add(item.Category);
                }
            }

            categoryList = categoryStrings;
        }

        public void addTaskToDb()
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
            else
            {
                int dayIndex = DaysToDisplay.FindIndex(x => x.NameOfDay == taskItemDay);

                using (TaskContext taskCtx = new TaskContext())
                {
                    TaskItemModel taskItem = new TaskItemModel(taskItemName, taskItemCategory, taskItemStartTime, taskItemEndTime);
                    taskItem.CategoryId = findCategoryId(taskItemCategory);
                    taskItem.DayId = findDayId(taskItemDay);
                    taskItem.WeekId = findWeekIdFromStartDate(weeklyStartDate); 
                    taskCtx.tasks.Add(taskItem);
                    taskCtx.SaveChanges();
                }

                DaysToDisplay[dayIndex].addTask(taskItemName, taskItemCategory, taskItemStartTime, taskItemEndTime);
                updateTaskListStrings(dayIndex);
                updateWeeklyTotals();
            }
        }

        public int findCategoryId(string categoryToFind)
        {
            using (TaskContext taskCtx = new TaskContext())
            {
                var category = taskCtx.categories
                    .Where(item => item.Category == categoryToFind)
                    .FirstOrDefault();

                return category.Id;
            }
        }

        public int findDayId(string dayToFind)
        {
            using (TaskContext taskCtx = new TaskContext())
            {
                var day = taskCtx.days
                    .Where(item => item.Day == dayToFind)
                    .FirstOrDefault();

                return day.Id;
            }
        }

        public int findWeekIdFromStartDate(DateTime mondayDateTime)
        {
            using (TaskContext taskCtx = new TaskContext())
            {
                var week = taskCtx.weeks
                    .Where(item => item.MondayDate == mondayDateTime)
                    .FirstOrDefault();

                return week.Id;
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

        public List<string> convertTaskListToStrings(List<TaskItemModel> taskList)
        {
            string taskString;
            List<string> taskStringList = new List<string>();

            foreach (TaskItemModel task in taskList)
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
