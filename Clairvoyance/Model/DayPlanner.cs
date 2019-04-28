using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Clairvoyance.Model
{
    public class DayPlanner
    {
        public DayPlanner(string dayOfWeek)
        {
            NameOfDay = dayOfWeek;
            TaskList = new List<TaskItem>();
        }

        public string NameOfDay
        {
            get;
            set;
        }

        public List<TaskItem> TaskList
        {
            get;
            set;
        }

        public void addTask(string taskName, string taskCategory, string startTime, string endTime)
        {
            TaskList.Add(new TaskItem(taskName, taskCategory, startTime, endTime));
        }
    }
}
