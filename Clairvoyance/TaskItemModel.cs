using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clairvoyance
{
    public class TaskItemModel
    {
        public TaskItemModel(string taskName, string taskCategory, string startTime, string endTime)
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
        }

        public int Id { get; set; }
        public string TaskName { get; set; }
        public string TaskCategory { get; set; }

        public int CategoryId { get; set; }
        public int DayId { get; set; }
        public int WeekId { get; set; }

        [ForeignKey("CategoryId")]
        public CategoryModel CategoryModel { get; set; }

        [ForeignKey("DayId")]
        public DayModel DayModel { get; set; }

        [ForeignKey("WeekId")]
        public WeekModel WeekModel { get; set; }

        public DateTime TaskStartDateTime { get; set; }
        public DateTime TaskEndDateTime { get; set; }
        public TimeSpan TaskTimeInterval { get; set; }

        public string appendTimeMinuteDigits(string originalTime)
        {
            return originalTime += ":00";
        }
    }
}
