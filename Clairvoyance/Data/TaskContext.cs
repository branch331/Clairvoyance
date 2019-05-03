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
        public DbSet<Model.TaskItem> tasks { get; set; }
        public DbSet<Day> days { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Week> weeks { get; set; }
    }
}
