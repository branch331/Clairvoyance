using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Clairvoyance
{
    public class DayPlannerModel
    {
        public DayPlannerModel(string dayOfWeek)
        {
            NameOfDay = dayOfWeek;
            TaskList = new List<TaskItemModel>();
        }

        public string NameOfDay
        {
            get;
            set;
        }

        public List<TaskItemModel> TaskList
        {
            get;
            set;
        }

        public void addTask(string taskName, string taskCategory, string startTime, string endTime)
        {
            TaskList.Add(new TaskItemModel(taskName, taskCategory, startTime, endTime));
        }
    }
}
