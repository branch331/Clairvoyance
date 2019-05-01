using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Clairvoyance.Model;
using Clairvoyance.Data;
using System.Collections.ObjectModel;

namespace ClairvoyanceTests
{
    [TestClass]
    public class DatabaseTests
    {
        TaskDatabaseLayer taskDbLayer;

        [TestInitialize]
        public void setUpTaskDbLayer()
        {
            taskDbLayer = new TaskDatabaseLayer();
        }

        [TestMethod]
        public void TestAddCategory()
        {
            string categoryName = "category 5";

            taskDbLayer.addNewCategory(categoryName);

            ObservableCollection<string> categoryList = taskDbLayer.getExistingCategoryList();

            Assert.AreEqual(categoryList[categoryList.Count - 1], categoryName);

            taskDbLayer.deleteCategory(categoryName);

            categoryList = taskDbLayer.getExistingCategoryList();

            Assert.IsFalse(categoryList[categoryList.Count - 1] == categoryName);
        }
    }
}
