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
using System.Windows;

namespace Clairvoyance.ViewModel
{
    public class WeeklyAgendaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ICommand categorySubmitCommand;
        private ICommand taskSubmitCommand;
        private ICommand taskUpdateCommand;
        private ICommand taskDeleteCommand;
        
        public string taskItemName;
        public string taskItemDay;
        public string taskItemCategory;
        public string taskItemStartTime;
        public string taskItemEndTime;
        public DateTime weeklyStartDate;
        public DateTime weeklyEndDate;
        public string weekRangeString;
        private string categoryToAdd;

        private ObservableCollection<TaskItem> monTaskItemList;
        private ObservableCollection<TaskItem> tuesTaskItemList;
        private ObservableCollection<TaskItem> wedTaskItemList;
        private ObservableCollection<TaskItem> thursTaskItemList;
        private ObservableCollection<TaskItem> friTaskItemList;
        private ObservableCollection<TaskItem> satTaskItemList;
        private ObservableCollection<TaskItem> sunTaskItemList;

        public List<DayPlanner> daysToDisplay;
        private List<DayPlanner> fullWeek = new List<DayPlanner>();
        public ObservableCollection<string> daysOfWeekList = new ObservableCollection<string>();
        public ObservableCollection<string> categoryList = new ObservableCollection<string>();
        public ObservableCollection<CategoryTotals> weeklyTotalsInHours = new ObservableCollection<CategoryTotals>();

        public TaskDatabaseLayer taskDbLayer = new TaskDatabaseLayer(new TaskContext());

        public WeeklyAgendaViewModel()
        {
            fullWeek = generateFullWeekList();
            MaxDaysInMonthDict = initializeMaxDaysInMonthDict(); 
            updateDaysToDisplay();
            populateCategoryListFromDb();
            populateWeekDayDb();
            populateDaysOfWeekFromDb();
            updateCurrentWeekDateTimes();
            populateTaskListFromDb();
            initializeWeeklyTotalsFromDb();

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

            taskUpdateCommand = new RelayCommand(o =>
            {
                try
                {
                    TaskItem taskItem = o as TaskItem;
                    updateTaskItemInDb(taskItem);
                    System.Windows.MessageBox.Show(taskItem.TaskName + " updated.");
                }
                catch (System.ArgumentException e)
                {
                    System.Windows.MessageBox.Show(e.Message);
                }
            }, o => true);

            taskDeleteCommand = new RelayCommand(o =>
            {
                try
                {
                    TaskItem taskItem = o as TaskItem;
                    MessageBoxResult continueWithDelete = MessageBox.Show("Delete " + taskItem + "?", "Confirm Delete", MessageBoxButton.OKCancel);

                    if (continueWithDelete == MessageBoxResult.OK)
                    {
                        deleteTaskItemFromDb(taskItem);
                    }
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

        public string WeekRangeString
        {
            get { return weekRangeString; }
            set
            {
                if (weekRangeString != value)
                {
                    weekRangeString = value;
                    NotifyPropertyChanged("WeekRangeString");
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

        public ObservableCollection<TaskItem> MonTaskItemList
        {
            get { return monTaskItemList; }
            set
            {
                if (monTaskItemList != value)
                {
                    monTaskItemList = value;
                    NotifyPropertyChanged("MonTaskItemList");
                }
            }
        }

        public ObservableCollection<TaskItem> TuesTaskItemList
        {
            get { return tuesTaskItemList; }
            set
            {
                if (tuesTaskItemList != value)
                {
                    tuesTaskItemList = value;
                    NotifyPropertyChanged("TuesTaskItemList");
                }
            }
        }

        public ObservableCollection<TaskItem> WedTaskItemList
        {
            get { return wedTaskItemList; }
            set
            {
                if (wedTaskItemList != value)
                {
                    wedTaskItemList = value;
                    NotifyPropertyChanged("WedTaskItemList");
                }
            }
        }

        public ObservableCollection<TaskItem> ThursTaskItemList
        {
            get { return thursTaskItemList; }
            set
            {
                if (thursTaskItemList != value)
                {
                    thursTaskItemList = value;
                    NotifyPropertyChanged("ThursTaskItemList");
                }
            }
        }

        public ObservableCollection<TaskItem> FriTaskItemList
        {
            get { return friTaskItemList; }
            set
            {
                if (friTaskItemList != value)
                {
                    friTaskItemList = value;
                    NotifyPropertyChanged("FriTaskItemList");
                }
            }
        }

        public ObservableCollection<TaskItem> SatTaskItemList
        {
            get { return satTaskItemList; }
            set
            {
                if (satTaskItemList != value)
                {
                    satTaskItemList = value;
                    NotifyPropertyChanged("SatTaskItemList");
                }
            }
        }

        public ObservableCollection<TaskItem> SunTaskItemList
        {
            get { return sunTaskItemList; }
            set
            {
                if (sunTaskItemList != value)
                {
                    sunTaskItemList = value;
                    NotifyPropertyChanged("SunTaskItemList");
                }
            }
        }

        public ObservableCollection<CategoryTotals> WeeklyTotalsInHours
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

        public ICommand TaskUpdateCommand
        {
            get { return taskUpdateCommand; }
            set
            {
                taskUpdateCommand = value;
            }
        }

        public ICommand TaskDeleteCommand
        {
            get { return taskDeleteCommand; }
            set
            {
                taskDeleteCommand = value;
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

            //If there is no range in which the input dateTime exists, retusrns null.
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

            weekRangeString = weeklyStartDate.ToString("MM/dd/yyyy-" + weeklyEndDate.ToString("MM/dd/yyyy"));
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

            var dayPlanner = DaysToDisplay[dayIndex];

            dayPlanner.addTask(taskItemName, taskItemCategory, taskItemStartTime, taskItemEndTime);

            dayPlanner.TaskList[dayPlanner.TaskList.Count - 1].DayId = taskDbLayer.findDayId(TaskItemDay);

            updateTaskItemLists(dayIndex);
            updateWeeklyTotals();

            addTaskToDb();
        }

        public void addTaskToDb()
        {
            TaskItem taskItem = new Model.TaskItem(taskItemName, taskItemCategory, taskItemStartTime, taskItemEndTime);
            taskDbLayer.addTaskItem(taskItem, taskItemDay, weeklyStartDate);

            populateTaskListFromDb();
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

        public void updateTaskItemLists(int dayIndex)
        {
            ObservableCollection<TaskItem> newTaskList = new ObservableCollection<TaskItem>(); //Used to convert from list >> ObservableCollection

            switch (dayIndex)
            {
                case 0:
                    foreach (TaskItem item in DaysToDisplay[0].TaskList)
                    {
                        newTaskList.Add(item);
                    }
                    monTaskItemList = newTaskList;
                    NotifyPropertyChanged("MonTaskItemList");
                    break;
                case 1:
                    foreach (TaskItem item in DaysToDisplay[1].TaskList)
                    {
                        newTaskList.Add(item);
                    }
                    tuesTaskItemList = newTaskList;
                    NotifyPropertyChanged("TuesTaskItemList");
                    break;
                case 2:
                    foreach (TaskItem item in DaysToDisplay[2].TaskList)
                    {
                        newTaskList.Add(item);
                    }
                    wedTaskItemList = newTaskList;
                    NotifyPropertyChanged("WedTaskItemList");
                    break;
                case 3:
                    foreach (TaskItem item in DaysToDisplay[3].TaskList)
                    {
                        newTaskList.Add(item);
                    }
                    thursTaskItemList = newTaskList;
                    NotifyPropertyChanged("ThursTaskItemList");
                    break;
                case 4:
                    foreach (TaskItem item in DaysToDisplay[4].TaskList)
                    {
                        newTaskList.Add(item);
                    }
                    friTaskItemList = newTaskList;
                    NotifyPropertyChanged("FriTaskItemList");
                    break;
                case 5:
                    foreach (TaskItem item in DaysToDisplay[5].TaskList)
                    {
                        newTaskList.Add(item);
                    }
                    satTaskItemList = newTaskList;
                    NotifyPropertyChanged("SatTaskItemList");
                    break;
                case 6:
                    foreach (TaskItem item in DaysToDisplay[6].TaskList)
                    {
                        newTaskList.Add(item);
                    }
                    sunTaskItemList = newTaskList;
                    NotifyPropertyChanged("SunTaskItemList");
                    break;
            }
        }

        public void populateTaskListFromDb()
        {
            var existingTaskList = taskDbLayer.getExistingTaskList();
            int currentWeekId = getCurrentWeekIdFromDb();

            resetDaysToDisplayToDefaults();

            if (existingTaskList != null)
            {
                foreach (TaskItem item in existingTaskList)
                {
                    if (item.WeekId == currentWeekId)
                    {
                        if (item.Day.DayName == DaysToDisplay[0].NameOfDay)
                        {
                            DaysToDisplay[0].TaskList.Add(item);
                            updateTaskItemLists(0);
                        }
                        else if (item.Day.DayName == DaysToDisplay[1].NameOfDay)
                        {
                            DaysToDisplay[1].TaskList.Add(item);
                            updateTaskItemLists(1);
                        }
                        else if (item.Day.DayName == DaysToDisplay[2].NameOfDay)
                        {
                            DaysToDisplay[2].TaskList.Add(item);
                            updateTaskItemLists(2);
                        }
                        else if (item.Day.DayName == DaysToDisplay[3].NameOfDay)
                        {
                            DaysToDisplay[3].TaskList.Add(item);
                            updateTaskItemLists(3);
                        }
                        else if (item.Day.DayName == DaysToDisplay[4].NameOfDay)
                        {
                            DaysToDisplay[4].TaskList.Add(item);
                            updateTaskItemLists(4);
                        }
                        else if (item.Day.DayName == DaysToDisplay[5].NameOfDay)
                        {
                            DaysToDisplay[5].TaskList.Add(item);
                            updateTaskItemLists(5);
                        }
                        else if (item.Day.DayName == DaysToDisplay[6].NameOfDay)
                        {
                            DaysToDisplay[6].TaskList.Add(item);
                            updateTaskItemLists(6);
                        }
                    }
                }
            }
        }

        public void resetDaysToDisplayToDefaults()
        {
            foreach (DayPlanner dayPlanner in DaysToDisplay)
            {
                dayPlanner.TaskList.Clear();
            }
        }

        public void updateTaskItemInDb(TaskItem updatedTaskItem)
        {
            updatedTaskItem.calculateTaskTimeInterval();
            taskDbLayer.updateTaskItem(updatedTaskItem);

            initializeWeeklyTotalsFromDb();
        }

        public void deleteTaskItemFromDb(TaskItem taskItemToDelete)
        {
            string dayName = taskDbLayer.findDayNameFromId(taskItemToDelete.DayId);
            int dayIndex = DaysToDisplay
                .FindIndex(x => x.NameOfDay == dayName);

            DaysToDisplay[dayIndex].TaskList.Remove(taskItemToDelete);
            updateTaskItemLists(dayIndex);

            taskDbLayer.deleteTaskItem(taskItemToDelete);

            initializeWeeklyTotalsFromDb();
        }

        public int getCurrentWeekIdFromDb()
        {
            var currentDateTime = DateTime.Now;
            DateTime mondayDateTime = taskDbLayer.getWeekRangeFromDate(currentDateTime).Item1;

            return taskDbLayer.findWeekIdFromStartDate(mondayDateTime);
        }

        public void initializeWeeklyTotalsFromDb()
        {
            weeklyTotalsInHours.Clear();

            var existingCategoryList = taskDbLayer.getExistingCategoryList();
            var existingTaskList = taskDbLayer.getExistingTaskList();

            int currentWeekId = getCurrentWeekIdFromDb();

            foreach (string category in existingCategoryList)
            {
                int totalHours = 0;
                foreach (TaskItem item in existingTaskList)
                {
                    if (item.Category.CategoryName == category && item.WeekId == currentWeekId)
                    {
                        totalHours += item.TaskTimeInterval.Hours;
                    }
                }

                weeklyTotalsInHours.Add(new CategoryTotals(category, totalHours));
            }
        }

        public void updateWeeklyTotals()
        {
            int differenceBetweenLists = CategoryList.Count - weeklyTotalsInHours.Count;
            int dayIndex = DaysToDisplay.FindIndex(x => x.NameOfDay == taskItemDay);
            var recentlyUpdatedTaskList = DaysToDisplay[dayIndex].TaskList;
            double hoursToAdd = recentlyUpdatedTaskList[recentlyUpdatedTaskList.Count - 1].TaskTimeInterval.TotalHours;

            for (int i = 0; i < differenceBetweenLists; i++)
            {
                weeklyTotalsInHours.Add(new CategoryTotals(CategoryList[CategoryList.Count - (differenceBetweenLists - i)], 0));
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
                taskString = string.Format("{0}\n{1}\n{2}-{3}", task.TaskName, task.TaskCategory, task.TaskStartDateTime.ToString("H:mm"), task.TaskEndDateTime.ToString("H:mm"));
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
