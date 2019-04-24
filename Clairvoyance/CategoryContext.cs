using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Clairvoyance
{
    class CategoryContext : DbContext
    {
        public CategoryContext() { }
        public DbSet<CategoryModel> categories { get; set; }
    }
}
