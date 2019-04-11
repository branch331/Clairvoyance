using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clairvoyance
{
    public class TaskItemModel
    {
        public TaskItemModel(string taskName, string taskCategory, string taskDescription, string startTime, string endTime)
        {
            TaskName = taskName;
            TaskCategory = taskCategory;
            TaskDescription = taskDescription;

            if (!startTime.Contains(":"))
            {
                startTime = appendTimeMinuteDigits(startTime);
            }

            if (!endTime.Contains(":"))
            {
                endTime = appendTimeMinuteDigits(endTime);
            }

            TaskStartDateTime = Convert.ToDateTime(startTime);
            TaskEndDateTime = Convert.ToDateTime(endTime);
            TaskTimeInterval = TaskEndDateTime - TaskStartDateTime;
        }

        public string TaskName
        {
            get;
            set;
        }

        public string TaskCategory
        {
            get;
            set;
        }

        public string TaskDescription
        {
            get;
            set;
        }

        public DateTime TaskStartDateTime
        {
            get;
            set;
        }

        public DateTime TaskEndDateTime
        {
            get;
            set;
        }

        public TimeSpan TaskTimeInterval
        {
            get;
            set;
        }

        public string appendTimeMinuteDigits(string originalTime)
        {
            return originalTime += ":00";
        }
    }
}
