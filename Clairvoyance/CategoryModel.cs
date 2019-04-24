using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clairvoyance
{
    public class CategoryModel
    {
        private CategoryModel()
        {

        }

        public CategoryModel(string category)
        {
            Category = category;
        }

        public int Id { get; set; }
        public string Category { get; set; }
    }
}
