﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Clairvoyance
{
    public class TaskContext : DbContext
    {
        public TaskContext() { }
        public DbSet<TaskItemModel> tasks { get; set; }
    }
}
