using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void addTask(string taskName)
        {
            TaskList.Add(new TaskItemModel(taskName));
        }
    }
}
