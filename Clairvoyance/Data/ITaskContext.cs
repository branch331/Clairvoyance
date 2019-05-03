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
        IDbSet<Model.TaskItem> tasks { get; set; }
        IDbSet<Day> days { get; set; }
        IDbSet<Category> categories { get; set; }
        IDbSet<Week> weeks { get; set; }
        int SaveChanges();
        void Dispose();
    }

}
