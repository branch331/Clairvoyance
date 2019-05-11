using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Clairvoyance.Model;
using Clairvoyance.Data;
using Clairvoyance.Helpers;
using System.Windows.Input;

namespace Clairvoyance.ViewModel
{
    public class StatisticsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ICommand loadWeekDataCommand;

        public ObservableCollection<string> availableWeekRangeStrings;
        public List<Week> availableWeekRanges;
        public string selectedWeekRange;
        public int currentWeekId;

        public ObservableCollection<CategoryTotals> monCategoryTotals;
        public ObservableCollection<CategoryTotals> tuesCategoryTotals;
        public ObservableCollection<CategoryTotals> wedCategoryTotals;
        public ObservableCollection<CategoryTotals> thursCategoryTotals;
        public ObservableCollection<CategoryTotals> friCategoryTotals;
        public ObservableCollection<CategoryTotals> satCategoryTotals;
        public ObservableCollection<CategoryTotals> sunCategoryTotals;

        public TaskDatabaseLayer taskDbLayer;

        public StatisticsViewModel(ITaskContext context)
        {
            availableWeekRanges = new List<Week>();
            AvailableWeekRangeStrings = new ObservableCollection<string>();

            monCategoryTotals = new ObservableCollection<CategoryTotals>();
            tuesCategoryTotals = new ObservableCollection<CategoryTotals>();
            wedCategoryTotals = new ObservableCollection<CategoryTotals>();
            thursCategoryTotals = new ObservableCollection<CategoryTotals>();
            friCategoryTotals = new ObservableCollection<CategoryTotals>();
            satCategoryTotals = new ObservableCollection<CategoryTotals>();
            sunCategoryTotals = new ObservableCollection<CategoryTotals>();

            taskDbLayer = new TaskDatabaseLayer(context);

            populateAvailableWeekRangesFromDb();
            setSelectedWeekRange();

            loadWeekDataCommand = new RelayCommand(o =>
            {
                try
                {
                    clearCategoryTotals();
                    loadWeekDataFromDb();
                }
                catch (System.ArgumentException e)
                {
                    System.Windows.MessageBox.Show(e.Message);
                }
            }, o => true);
        }

        public void populateAvailableWeekRangesFromDb()
        {
            ObservableCollection<Week> existingWeekRanges = taskDbLayer.getExistingWeekRanges();

            foreach (Week week in existingWeekRanges)
            {
                availableWeekRanges.Add(week);
                availableWeekRangeStrings.Add(week.MondayDate.ToString("MM/dd/yyyy") + " - " + week.SundayDate.ToString("MM/dd/yyyy"));
            }
        }

        public void clearCategoryTotals()
        {
            monCategoryTotals = removeAllElementsFromCategoryTotals(monCategoryTotals);
            tuesCategoryTotals = removeAllElementsFromCategoryTotals(tuesCategoryTotals);
            wedCategoryTotals = removeAllElementsFromCategoryTotals(wedCategoryTotals);
            thursCategoryTotals = removeAllElementsFromCategoryTotals(thursCategoryTotals);
            friCategoryTotals = removeAllElementsFromCategoryTotals(friCategoryTotals);
            satCategoryTotals = removeAllElementsFromCategoryTotals(satCategoryTotals);
            sunCategoryTotals = removeAllElementsFromCategoryTotals(sunCategoryTotals);
        }

        public ObservableCollection<CategoryTotals> removeAllElementsFromCategoryTotals(ObservableCollection<CategoryTotals> categoryTotals)
        {
            var categoryTotalsList = categoryTotals.ToList();

            foreach (CategoryTotals item in categoryTotalsList)
            {
                categoryTotals.Remove(item);
            }

            return categoryTotals;
        }

        public void loadWeekDataFromDb()
        {
            Week selectedWeek = availableWeekRanges[availableWeekRangeStrings.IndexOf(selectedWeekRange)];
            currentWeekId = getCurrentWeekIdFromDb(selectedWeek);

            var taskList = taskDbLayer.getExistingTaskList();

            initializeCategoryTotals();

            foreach (TaskItem task in taskList)
            {
                if (task.WeekId == currentWeekId)
                {
                    if (task.DayId == taskDbLayer.findDayId("Mon") && monCategoryTotals.Count != 0)
                    {
                        addTaskHoursToCategoryTotals(task, monCategoryTotals);
                    }
                    else if (task.DayId == taskDbLayer.findDayId("Tues") && tuesCategoryTotals.Count != 0)
                    {
                        addTaskHoursToCategoryTotals(task, tuesCategoryTotals);
                    }
                    else if (task.DayId == taskDbLayer.findDayId("Wed") && wedCategoryTotals.Count != 0)
                    {
                        addTaskHoursToCategoryTotals(task, wedCategoryTotals);
                    }
                    else if (task.DayId == taskDbLayer.findDayId("Thurs") && thursCategoryTotals.Count != 0)
                    {
                        addTaskHoursToCategoryTotals(task, thursCategoryTotals);
                    }
                    else if (task.DayId == taskDbLayer.findDayId("Fri") && friCategoryTotals.Count != 0)
                    {
                        addTaskHoursToCategoryTotals(task, friCategoryTotals);
                    }
                    else if (task.DayId == taskDbLayer.findDayId("Sat") && satCategoryTotals.Count != 0)
                    {
                        addTaskHoursToCategoryTotals(task, satCategoryTotals);
                    }
                    else if (task.DayId == taskDbLayer.findDayId("Sun") && sunCategoryTotals.Count != 0)
                    {
                        addTaskHoursToCategoryTotals(task, sunCategoryTotals);
                    }
                }
            }
        }

        public ObservableCollection<CategoryTotals> addTaskHoursToCategoryTotals(TaskItem task, ObservableCollection<CategoryTotals> categoryTotals)
        {
            categoryTotals
                .Where(item => item.Category == task.TaskCategory)
                .FirstOrDefault()
                .TotalHours += task.TaskTimeInterval.TotalHours;

            return categoryTotals;
        }

        public void initializeCategoryTotals()
        {
            var taskList = taskDbLayer.getExistingTaskList();

            foreach (TaskItem task in taskList)
            {
                if (task.WeekId == currentWeekId)
                {
                    if (task.DayId == taskDbLayer.findDayId("Mon"))
                    {
                        if (!isWithinCategoryTotals(task.TaskCategory, monCategoryTotals))
                        {
                            monCategoryTotals.Add(new CategoryTotals(task.TaskCategory, 0));
                        }
                    }
                    else if (task.DayId == taskDbLayer.findDayId("Tues"))
                    {
                        if (!isWithinCategoryTotals(task.TaskCategory, tuesCategoryTotals))
                        {
                            tuesCategoryTotals.Add(new CategoryTotals(task.TaskCategory, 0));
                        }
                    }
                    else if (task.DayId == taskDbLayer.findDayId("Wed"))
                    {
                        if (!isWithinCategoryTotals(task.TaskCategory, wedCategoryTotals))
                        {
                            wedCategoryTotals.Add(new CategoryTotals(task.TaskCategory, 0));
                        }
                    }
                    else if (task.DayId == taskDbLayer.findDayId("Thurs"))
                    {
                        if (!isWithinCategoryTotals(task.TaskCategory, thursCategoryTotals))
                        {
                            thursCategoryTotals.Add(new CategoryTotals(task.TaskCategory, 0));
                        }
                    }
                    else if (task.DayId == taskDbLayer.findDayId("Fri"))
                    {
                        if (!isWithinCategoryTotals(task.TaskCategory, friCategoryTotals))
                        {
                            friCategoryTotals.Add(new CategoryTotals(task.TaskCategory, 0));
                        }
                    }
                    else if (task.DayId == taskDbLayer.findDayId("Sat"))
                    {
                        if (!isWithinCategoryTotals(task.TaskCategory, satCategoryTotals))
                        {
                            satCategoryTotals.Add(new CategoryTotals(task.TaskCategory, 0));
                        }
                    }
                    else if (task.DayId == taskDbLayer.findDayId("Sun"))
                    {
                        if (!isWithinCategoryTotals(task.TaskCategory, sunCategoryTotals))
                        {
                            sunCategoryTotals.Add(new CategoryTotals(task.TaskCategory, 0));
                        }
                    }
                }
            }
        }

        public bool isWithinCategoryTotals(string categoryName, ObservableCollection<CategoryTotals> categoryTotals)
        {
            foreach (CategoryTotals item in categoryTotals)
            {
                if (item.Category == categoryName)
                {
                    return true;
                }
            }

            return false;
        }

        public int getCurrentWeekIdFromDb(Week selectedWeek)
        {
            return taskDbLayer.findWeekIdFromStartDate(selectedWeek.MondayDate); 
        }

        public void setSelectedWeekRange()
        {
            DateTime currentDateTime = DateTime.Now;

            Tuple<DateTime, DateTime> currentWeekRange = taskDbLayer.getWeekRangeFromDate(currentDateTime);

            foreach (Week week in availableWeekRanges)
            {
                if (week.MondayDate == currentWeekRange.Item1)
                {
                    selectedWeekRange = availableWeekRangeStrings[availableWeekRanges.FindIndex(x => x.MondayDate == week.MondayDate)];
                }
            }
        }

        public ObservableCollection<string> AvailableWeekRangeStrings
        {
            get { return availableWeekRangeStrings; }
            set
            {
                if (value != null)
                {
                    availableWeekRangeStrings = value;
                    NotifyPropertyChanged("AvailableWeekRangeStrings");
                }
            }
        }

        public string SelectedWeekRange
        {
            get { return selectedWeekRange; }
            set
            {
                if (value != null)
                {
                    selectedWeekRange = value;
                    NotifyPropertyChanged("SelectedWeekRange");
                }
            }
        }

        public ObservableCollection<CategoryTotals> MonCategoryTotals
        {
            get { return monCategoryTotals; }
            set
            {
                if (value != null)
                {
                    monCategoryTotals = value;
                    NotifyPropertyChanged("MonCategoryTotals");
                }
            }
        }

        public ObservableCollection<CategoryTotals> TuesCategoryTotals
        {
            get { return tuesCategoryTotals; }
            set
            {
                if (value != null)
                {
                    tuesCategoryTotals = value;
                    NotifyPropertyChanged("TuesCategoryTotals");
                }
            }
        }

        public ObservableCollection<CategoryTotals> WedCategoryTotals
        {
            get { return wedCategoryTotals; }
            set
            {
                if (value != null)
                {
                    wedCategoryTotals = value;
                    NotifyPropertyChanged("WedCategoryTotals");
                }
            }
        }

        public ObservableCollection<CategoryTotals> ThursCategoryTotals
        {
            get { return thursCategoryTotals; }
            set
            {
                if (value != null)
                {
                    thursCategoryTotals = value;
                    NotifyPropertyChanged("ThursCategoryTotals");
                }
            }
        }

        public ObservableCollection<CategoryTotals> FriCategoryTotals
        {
            get { return friCategoryTotals; }
            set
            {
                if (value != null)
                {
                    friCategoryTotals = value;
                    NotifyPropertyChanged("FriCategoryTotals");
                }
            }
        }

        public ObservableCollection<CategoryTotals> SatCategoryTotals
        {
            get { return satCategoryTotals; }
            set
            {
                if (value != null)
                {
                    satCategoryTotals = value;
                    NotifyPropertyChanged("SatCategoryTotals");
                }
            }
        }

        public ObservableCollection<CategoryTotals> SunCategoryTotals
        {
            get { return sunCategoryTotals; }
            set
            {
                if (value != null)
                {
                    sunCategoryTotals = value;
                    NotifyPropertyChanged("SunCategoryTotals");
                }
            }
        }

        public ICommand LoadWeekDataCommand
        {
            get { return loadWeekDataCommand; }
            set
            {
                loadWeekDataCommand = value;
            }
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
