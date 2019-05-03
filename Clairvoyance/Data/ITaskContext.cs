using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Clairvoyance.Model;

namespace Clairvoyance.Data
{
    public interface ITaskContext
    {
        DbSet<Model.TaskItem> tasks { get; set; }
        DbSet<Day> days { get; set; }
        DbSet<Category> categories { get; set; }
        DbSet<Week> weeks { get; set; }
        int SaveChanges();
        void Dispose();
    }

}
