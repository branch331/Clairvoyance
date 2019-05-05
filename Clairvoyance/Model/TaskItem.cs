using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Clairvoyance.Helpers;

namespace Clairvoyance.Model
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string TaskCategory { get; set; }

        public int CategoryId { get; set; }
        public int DayId { get; set; }
        public int WeekId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [ForeignKey("DayId")]
        public Day Day { get; set; }

        [ForeignKey("WeekId")]
        public Week Week { get; set; }

        public DateTime TaskStartDateTime { get; set; }
        public DateTime TaskEndDateTime { get; set; }
        public TimeSpan TaskTimeInterval { get; set; }

        private TaskItem()
        {

        }

        public TaskItem(string taskName, string taskCategory, string startTime, string endTime)
        {
            TaskName = taskName;
            TaskCategory = taskCategory;

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

            if (TaskTimeInterval.Hours < 0)
            {
                TaskTimeInterval += TimeSpan.FromHours(12);
            }
        }

        public string appendTimeMinuteDigits(string originalTime)
        {
            return originalTime += ":00";
        }
    }
}
