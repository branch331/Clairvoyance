using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clairvoyance.Helpers;

namespace Clairvoyance.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        private Category()
        {

        }

        public Category(string categoryName)
        {
            CategoryName = categoryName;
        }
    }
}
