using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clairvoyance.Model
{
    public class Category
    {
        private Category()
        {

        }

        public Category(string categoryName)
        {
            CategoryName = categoryName;
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}
