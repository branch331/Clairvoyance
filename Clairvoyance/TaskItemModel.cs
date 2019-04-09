using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clairvoyance
{
    public class TaskItemModel
    {
        public TaskItemModel(string taskName)
        {
            TaskName = taskName;
        }

        public string TaskName
        {
            get;
            set;
        }
    }
}
