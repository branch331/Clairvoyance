using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Clairvoyance.Model;

namespace Clairvoyance.Data
{
    public class TaskContext : DbContext, ITaskContext
    {
        public DbContext Instance => this;

        public TaskContext() { }
        public IDbSet<Model.TaskItem> tasks { get; set; }
        public IDbSet<Day> days { get; set; }
        public IDbSet<Category> categories { get; set; }
        public IDbSet<Week> weeks { get; set; }
    }
}
