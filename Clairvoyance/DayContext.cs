using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Clairvoyance 
{
    class DayContext : DbContext
    {
        public DayContext() { }
        public DbSet<DayModel> days { get; set; }
    }
}
