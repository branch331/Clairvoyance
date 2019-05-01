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
        private TaskContext taskCtx;

        public TaskDatabaseLayer()
        {
            taskCtx = new TaskContext();
        }

        public ObservableCollection<string> getExistingCategoryList()
        {
            ObservableCollection<string> categoryStrings = new ObservableCollection<string>();

            foreach (var item in taskCtx.categories)
            {
                categoryStrings.Add(item.CategoryName);
            }

            return categoryStrings;
        }

        public void addNewCategory(string categoryToAdd)
        {
            if (!taskCtx.categories.Any(item => item.CategoryName == categoryToAdd))
            {
                taskCtx.categories.Add(new Category(categoryToAdd));
                taskCtx.SaveChanges();
            }
        }

        public void deleteCategory(string categoryToDelete)
        {
            var categoryEntityToDelete = taskCtx.categories
                .Where(item => item.CategoryName == categoryToDelete)
                .FirstOrDefault();

            if (categoryEntityToDelete != null)
            {
                taskCtx.categories.Remove(categoryEntityToDelete);
                taskCtx.SaveChanges();
            }
        }

        public int findCategoryId(string categoryToFind)
        {
            var category = taskCtx.categories
                .Where(item => item.CategoryName == categoryToFind)
                .FirstOrDefault();

            return category.Id;
        }

        public void addTaskItem(TaskItem taskItem, string taskItemDay, DateTime mondayDateTime)
        {
            taskItem.CategoryId = findCategoryId(taskItem.TaskCategory);
            taskItem.DayId = findDayId(taskItemDay);
            taskItem.WeekId = findWeekIdFromStartDate(mondayDateTime);
            taskCtx.tasks.Add(taskItem);
            taskCtx.SaveChanges();
        }

        public void deleteTaskItem(TaskItem taskItemToDelete)
        {
            var taskEntityToDelete = taskCtx.tasks
                .Where(item => item.Id == taskItemToDelete.Id)
                .FirstOrDefault();

            if (taskEntityToDelete != null)
            {
                taskCtx.tasks.Remove(taskEntityToDelete);
                taskCtx.SaveChanges();
            }
        }

        public ObservableCollection<string> getListOfWeekdays()
        {
            ObservableCollection<string> listOfWeekdays = new ObservableCollection<string>();

            foreach (Day item in taskCtx.days)
            {
                listOfWeekdays.Add(item.DayName);
            }

            return listOfWeekdays;
        }

        public int findDayId(string dayToFind)
        {
            using (TaskContext taskCtx = new TaskContext())
            {
                var day = taskCtx.days
                    .Where(item => item.DayName == dayToFind)
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

        public void initializeWeekdays()
        {
            taskCtx.days.Add(new Day("Mon"));
            taskCtx.days.Add(new Day("Tues"));
            taskCtx.days.Add(new Day("Wed"));
            taskCtx.days.Add(new Day("Thurs"));
            taskCtx.days.Add(new Day("Fri"));
            taskCtx.days.Add(new Day("Sat"));
            taskCtx.days.Add(new Day("Sun"));

            taskCtx.SaveChanges();
        }

        public void addNewWeekRange(Week newWeekRange)
        {
            taskCtx.weeks.Add(newWeekRange);
            taskCtx.SaveChanges();
        }

        //If there is no range in which the input dateTime exists, returns null.
        public Tuple<DateTime, DateTime> getWeekRangeFromDate(DateTime dateTime)
        {
            DateTime defaultDateTime = new DateTime(1995, 5, 15);

            foreach (var item in taskCtx.weeks)
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
