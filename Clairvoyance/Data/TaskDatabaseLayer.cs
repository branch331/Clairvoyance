using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Clairvoyance.Model;

namespace Clairvoyance.Data
{
    public class TaskDatabaseLayer 
    {
        private ITaskContext _taskCtx;

        public TaskDatabaseLayer(ITaskContext taskCtx)
        {
            _taskCtx = taskCtx;
        }

        public ObservableCollection<string> getExistingCategoryList()
        {
            ObservableCollection<string> categoryStrings = new ObservableCollection<string>();

            foreach (var item in _taskCtx.categories)
            {
                categoryStrings.Add(item.CategoryName);
            }

            return categoryStrings;
        }

        public void addNewCategory(string categoryToAdd)
        {
            if (!_taskCtx.categories.Any(item => item.CategoryName == categoryToAdd))
            {
                _taskCtx.categories.Add(new Category(categoryToAdd));
                _taskCtx.SaveChanges();
            }
        }

        public void deleteCategory(string categoryToDelete)
        {
            var categoryEntityToDelete = _taskCtx.categories
                .Where(item => item.CategoryName == categoryToDelete)
                .FirstOrDefault();

            if (categoryEntityToDelete != null)
            {
                _taskCtx.categories.Remove(categoryEntityToDelete);
                _taskCtx.SaveChanges();
            }
        }

        public int findCategoryId(string categoryToFind)
        {
            var category = _taskCtx.categories
                .Where(item => item.CategoryName == categoryToFind)
                .FirstOrDefault();

            return category.Id;
        }

        public void addTaskItem(TaskItem taskItem, string taskItemDay, DateTime mondayDateTime)
        {
            taskItem.CategoryId = findCategoryId(taskItem.TaskCategory);
            taskItem.DayId = findDayId(taskItemDay);
            taskItem.WeekId = findWeekIdFromStartDate(mondayDateTime);
            _taskCtx.tasks.Add(taskItem);
            _taskCtx.SaveChanges();
        }

        public void deleteTaskItem(TaskItem taskItemToDelete)
        {
            var taskEntityToDelete = _taskCtx.tasks
                .Where(item => item.Id == taskItemToDelete.Id)
                .FirstOrDefault();

            if (taskEntityToDelete != null)
            {
                _taskCtx.tasks.Remove(taskEntityToDelete);
                _taskCtx.SaveChanges();
            }
        }

        public ObservableCollection<string> getListOfWeekdays()
        {
            ObservableCollection<string> listOfWeekdays = new ObservableCollection<string>();

            foreach (Day item in _taskCtx.days)
            {
                listOfWeekdays.Add(item.DayName);
            }

            return listOfWeekdays;
        }

        public int findDayId(string dayToFind)
        {
            using (TaskContext _taskCtx = new TaskContext())
            {
                var day = _taskCtx.days
                    .Where(item => item.DayName == dayToFind)
                    .FirstOrDefault();

                return day.Id;
            }
        }

        public int findWeekIdFromStartDate(DateTime mondayDateTime)
        {
            using (TaskContext _taskCtx = new TaskContext())
            {
                var week = _taskCtx.weeks
                    .Where(item => item.MondayDate == mondayDateTime)
                    .FirstOrDefault();

                return week.Id;
            }
        }

        public void initializeWeekdays()
        {
            _taskCtx.days.Add(new Day("Mon"));
            _taskCtx.days.Add(new Day("Tues"));
            _taskCtx.days.Add(new Day("Wed"));
            _taskCtx.days.Add(new Day("Thurs"));
            _taskCtx.days.Add(new Day("Fri"));
            _taskCtx.days.Add(new Day("Sat"));
            _taskCtx.days.Add(new Day("Sun"));

            _taskCtx.SaveChanges();
        }

        public void addNewWeekRange(Week newWeekRange)
        {
            _taskCtx.weeks.Add(newWeekRange);
            _taskCtx.SaveChanges();
        }

        public void deleteWeekRange(DateTime mondayWeekDate)
        {
            int weekId = findWeekIdFromStartDate(mondayWeekDate);
            var weekEntityToDelete = _taskCtx.tasks
                .Where(item => item.Id == weekId)
                .FirstOrDefault();

            if (weekEntityToDelete != null)
            {
                _taskCtx.tasks.Remove(weekEntityToDelete);
                _taskCtx.SaveChanges();
            }
        }

        //If there is no range in which the input dateTime exists, returns null.
        public Tuple<DateTime, DateTime> getWeekRangeFromDate(DateTime dateTime)
        {
            DateTime defaultDateTime = new DateTime(1995, 5, 15);

            foreach (var item in _taskCtx.weeks)
            {
                if (dateTime >= item.MondayDate && dateTime <= item.SundayDate.AddHours(23).AddMinutes(59).AddSeconds(59))
                {
                    return Tuple.Create(item.MondayDate, item.SundayDate);
                }
            }

            return null;
        }
    }
}
