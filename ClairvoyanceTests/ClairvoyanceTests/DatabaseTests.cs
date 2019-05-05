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
        private TaskContext taskContext;
        private TaskItem defaultTaskItem;
        private DateTime testMondayDateTime;
        private DateTime testSundayDateTime;
        private string defaultCategory = "defaultCategory";
        private Mock<ITaskContext> mockContext;
        private TaskDatabaseLayer mockDbLayer;
        private TaskItem taskA;
        private TaskItem taskB;

        [TestInitialize]
        public void setUpTaskDbLayer()
        {
            taskContext = new TaskContext();
            defaultTaskItem = new TaskItem("defaultTask", defaultCategory, "5", "8");
            testMondayDateTime = new DateTime(1995, 5, 15);
            testSundayDateTime = new DateTime(1995, 5, 22);

            taskDbLayer = new TaskDatabaseLayer(taskContext);
            
            taskDbLayer.addNewWeekRange(new Week(testMondayDateTime, new DateTime(1995, 5, 20)));
            taskDbLayer.addNewCategory(defaultCategory);
            taskDbLayer.addTaskItem(defaultTaskItem, "Mon", testMondayDateTime);
            
            var mockContext = new Mock<ITaskContext>();
            mockDbLayer = new TaskDatabaseLayer(mockContext.Object);

            var fakeCategorySet = new FakeDbSet<Category>();
            mockContext.Setup(context => context.categories).Returns(fakeCategorySet);

            fakeCategorySet.Add(new Category("Cat A")
            {
                Id = 5
            });
            fakeCategorySet.Add(new Category("Cat B")
            {
                Id = 7
            });

            var fakeTaskSet = new FakeDbSet<TaskItem>();

            taskA = new TaskItem("Task A", "Cat A", "5", "6");
            taskB = new TaskItem("Task B", "Cat B", "5", "6");

            fakeTaskSet.Add(taskA);
            fakeTaskSet.Add(taskB);

            mockContext.Setup(context => context.tasks).Returns(fakeTaskSet);

            var fakeWeekSet = new FakeDbSet<Week>();

            fakeWeekSet.Add(new Week(testMondayDateTime, testSundayDateTime)
            {
                Id = 59
            });

            mockContext.Setup(context => context.weeks).Returns(fakeWeekSet);
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
        public void TestMockGetCategoryList()
        {
            var categoryList = mockDbLayer.getExistingCategoryList();

            Assert.IsTrue(categoryList.Count() == 2);
        }

        [TestMethod]
        public void TestMockDeleteCategory()
        {
            mockDbLayer.deleteCategory("Cat A");
            var categoryList = mockDbLayer.getExistingCategoryList();

            Assert.IsTrue(categoryList.Count() == 1);
        }

        [TestMethod]
        public void TestMockFindCategoryId()
        {
            Assert.IsTrue(mockDbLayer.findCategoryId("Cat A") == 5);
            Assert.IsTrue(mockDbLayer.findCategoryId("Cat B") == 7);
        }

        [TestMethod]
        public void TestMockGetTaskList()
        {
            var taskList = mockDbLayer.getExistingTaskList();

            Assert.IsTrue(taskList.Count == 2);
        }

        [TestMethod]
        public void TestMockDeleteTaskItem()
        {
            mockDbLayer.deleteTaskItem(taskA);

            var taskList = mockDbLayer.getExistingTaskList();

            Assert.IsTrue(taskList.Count == 1);
            Assert.IsTrue(taskList[0].TaskName == "Task B");
        }

        [TestMethod]
        public void TestAddTask()
        {
            taskDbLayer.addTaskItem(defaultTaskItem, "Mon", testMondayDateTime);
            var existingTaskList = taskDbLayer.getExistingTaskList();
            int originalNumberOfTasks = existingTaskList.Count;
            Assert.AreEqual(existingTaskList[originalNumberOfTasks - 1].TaskName, "defaultTask");

            taskDbLayer.deleteTaskItem(defaultTaskItem);
            Assert.IsTrue(taskContext.tasks.Count() == originalNumberOfTasks - 1);
        }

        [TestMethod]
        public void TestMockFindWeekId()
        {
            Assert.IsTrue(mockDbLayer.findWeekIdFromStartDate(testMondayDateTime) == 59);
        }

        [TestMethod]
        public void TestAddWeek()
        {
            taskDbLayer.addNewWeekRange(new Week(testMondayDateTime, testSundayDateTime));
            int originalNumberOfWeeks = taskContext.weeks.Count();
            Assert.AreEqual(taskDbLayer.getWeekRangeFromDate(testMondayDateTime).Item1, testMondayDateTime);

            taskDbLayer.deleteWeekRange(testMondayDateTime);
            Assert.IsTrue(taskContext.weeks.Count() == originalNumberOfWeeks - 1);
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
