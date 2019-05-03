using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Clairvoyance.Model;
using Clairvoyance.Data;
using System.Collections.ObjectModel;
using System.Data.Entity;
using Moq;

namespace ClairvoyanceTests
{
    [TestClass]
    public class DatabaseTests
    {
        private TaskDatabaseLayer taskDbLayer;
        private TaskItem defaultTaskItem;
        private DateTime testMondayDateTime;
        private string defaultCategory = "defaultCategory";

        [TestInitialize]
        public void setUpTaskDbLayer()
        {
            defaultTaskItem = new TaskItem("defaultTask", defaultCategory, "5", "8");
            testMondayDateTime = new DateTime(1995, 5, 15);

            taskDbLayer = new TaskDatabaseLayer(new TaskContext());
            taskDbLayer.addNewWeekRange(new Week(testMondayDateTime, new DateTime(1995, 5, 20)));
            taskDbLayer.addNewCategory(defaultCategory);
            taskDbLayer.addTaskItem(defaultTaskItem, "Mon", testMondayDateTime);
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

        [TestMethod]
        public void TestMockAddCategory()
        {
            var mockContext = new Mock<ITaskContext>();

            TaskDatabaseLayer mockDbLayer = new TaskDatabaseLayer(mockContext.Object);
            var fakeCategorySet = new FakeDbSet<Category>();

            fakeCategorySet.Add(new Category("Cat A"));
            fakeCategorySet.Add(new Category("Cat B"));

            mockContext.Setup(context => context.categories).Returns(fakeCategorySet);

            var categoryList = mockContext.Object.categories;

            Assert.IsTrue(categoryList.Count() == 2);
        }

        [TestCleanup]
        public void tearDownTaskDbLayer()
        {
            taskDbLayer.deleteCategory("defaultCategory");
            taskDbLayer.deleteTaskItem(defaultTaskItem);
            taskDbLayer.deleteWeekRange(testMondayDateTime);
        }
    }
}
