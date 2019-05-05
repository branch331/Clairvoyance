using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Clairvoyance.ViewModel;
using Clairvoyance.Data;
using Clairvoyance.Model;

namespace ClairvoyanceTests
{
    [TestClass]
    public class ValidationTests
    {
        private TaskDatabaseLayer taskDbLayer;
        private TaskItem defaultTaskItem;
        private DateTime testMondayDateTime;
        private string defaultCategory = "defaultCategory";

        [TestInitialize]
        public void setUpTestViewModels()
        {
            defaultTaskItem = new TaskItem("defaultTask", defaultCategory, "5", "8");
            testMondayDateTime = new DateTime(1995, 5, 15);

            taskDbLayer = new TaskDatabaseLayer(new TaskContext());
            taskDbLayer.addNewWeekRange(new Week(testMondayDateTime, new DateTime(1995, 5, 20)));
            taskDbLayer.addNewCategory(defaultCategory);
            taskDbLayer.addTaskItem(defaultTaskItem, "Mon", testMondayDateTime);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
        "One or more task fields null or empty.")]
        public void addNullCategoryTest()
        {
            WeeklyAgendaViewModel testAgendaVM = new WeeklyAgendaViewModel()
            {
                TaskItemName = "Test Task",
                TaskItemDay = "Mon",
                TaskItemCategory = null,
                TaskItemStartTime = "4:30",
                TaskItemEndTime = "5:30"
            };

            testAgendaVM.addTaskToDay();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
        "One or more task fields null or empty.")]
        public void addNullNameTest()
        {
            WeeklyAgendaViewModel testAgendaVM = new WeeklyAgendaViewModel()
            {
                TaskItemName = null,
                TaskItemDay = "Mon",
                TaskItemCategory = "Cat",
                TaskItemStartTime = "4:30",
                TaskItemEndTime = "5:30"
            };

            testAgendaVM.addTaskToDay();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
        "One or more task fields null or empty.")]
        public void addNullDayTest()
        {
            WeeklyAgendaViewModel testAgendaVM = new WeeklyAgendaViewModel()
            {
                TaskItemName = "Test Task",
                TaskItemDay = null,
                TaskItemCategory = "Cat",
                TaskItemStartTime = "4:30",
                TaskItemEndTime = "5:30"
            };

            testAgendaVM.addTaskToDay();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
    "One or more task fields null or empty.")]
        public void addNullStartTimeTest()
        {
            WeeklyAgendaViewModel testAgendaVM = new WeeklyAgendaViewModel()
            {
                TaskItemName = "Test Task",
                TaskItemDay = "Mon",
                TaskItemCategory = "Cat",
                TaskItemStartTime = null,
                TaskItemEndTime = "5:30"
            };

            testAgendaVM.addTaskToDay();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
    "One or more task fields null or empty.")]
        public void addNullEndTimeTest()
        {
            WeeklyAgendaViewModel testAgendaVM = new WeeklyAgendaViewModel()
            {
                TaskItemName = "Test Task",
                TaskItemDay = "Mon",
                TaskItemCategory = "Cat",
                TaskItemStartTime = "4:30",
                TaskItemEndTime = null
            };

            testAgendaVM.addTaskToDay();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
        "One or more task fields null or empty.")]
        public void addNameWhiteSpaceTest()
        {
            WeeklyAgendaViewModel testAgendaVM = new WeeklyAgendaViewModel()
            {
                TaskItemName = "   ",
                TaskItemDay = "Mon",
                TaskItemCategory = null,
                TaskItemStartTime = "4:30",
                TaskItemEndTime = "5:30"
            };

            testAgendaVM.addTaskToDay();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
        "One or more task fields null or empty.")]
        public void addDayEmptyTest()
        {
            WeeklyAgendaViewModel testAgendaVM = new WeeklyAgendaViewModel()
            {
                TaskItemName = "Test Task",
                TaskItemDay = "",
                TaskItemCategory = "Cat",
                TaskItemStartTime = "4:30",
                TaskItemEndTime = "5:30"
            };

            testAgendaVM.addTaskToDay();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
        "Input times must be from 1-12.")]
        public void addStartTimeThirteen()
        {
            WeeklyAgendaViewModel testAgendaVM = new WeeklyAgendaViewModel()
            {
                TaskItemName = "Test Task",
                TaskItemDay = "Mon",
                TaskItemCategory = "Cat",
                TaskItemStartTime = "13",
                TaskItemEndTime = "5:30"
            };

            testAgendaVM.addTaskToDay();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
        "Input times must be from 1-12.")]
        public void addStartTimeZero()
        {
            WeeklyAgendaViewModel testAgendaVM = new WeeklyAgendaViewModel()
            {
                TaskItemName = "Test Task",
                TaskItemDay = "Mon",
                TaskItemCategory = "Cat",
                TaskItemStartTime = "0",
                TaskItemEndTime = "5:30"
            };

            testAgendaVM.addTaskToDay();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
        "Input times must be from 1-12.")]
        public void addStartTimeAlphanumeric()
        {
            WeeklyAgendaViewModel testAgendaVM = new WeeklyAgendaViewModel()
            {
                TaskItemName = "Test Task",
                TaskItemDay = "Mon",
                TaskItemCategory = "Cat",
                TaskItemStartTime = "a",
                TaskItemEndTime = "5:30"
            };

            testAgendaVM.addTaskToDay();
        }

        [TestMethod]
        public void addStartTimeOne()
        {
            WeeklyAgendaViewModel testAgendaVM = new WeeklyAgendaViewModel()
            {
                TaskItemName = "Test Task",
                TaskItemDay = "Mon",
                TaskItemCategory = defaultCategory,
                TaskItemStartTime = "1",
                TaskItemEndTime = "5:30"
            };

            testAgendaVM.addTaskToDay();
            int taskCount = testAgendaVM.DaysToDisplay[0].TaskList.Count;

            Assert.IsTrue(testAgendaVM.DaysToDisplay[0].TaskList[taskCount - 1].TaskName == "Test Task");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
        "Input times must be from 1-12.")]
        public void addEndTimeThirteen()
        {
            WeeklyAgendaViewModel testAgendaVM = new WeeklyAgendaViewModel()
            {
                TaskItemName = "Test Task",
                TaskItemDay = "Mon",
                TaskItemCategory = "Cat",
                TaskItemStartTime = "4",
                TaskItemEndTime = "13"
            };

            testAgendaVM.addTaskToDay();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
        "Input times must be from 1-12.")]
        public void addEndTimeZero()
        {
            WeeklyAgendaViewModel testAgendaVM = new WeeklyAgendaViewModel()
            {
                TaskItemName = "Test Task",
                TaskItemDay = "Mon",
                TaskItemCategory = "Cat",
                TaskItemStartTime = "4",
                TaskItemEndTime = "0"
            };

            testAgendaVM.addTaskToDay();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
        "Input times must be from 1-12.")]
        public void addEndTimeAlphanumeric()
        {
            WeeklyAgendaViewModel testAgendaVM = new WeeklyAgendaViewModel()
            {
                TaskItemName = "Test Task",
                TaskItemDay = "Mon",
                TaskItemCategory = "Cat",
                TaskItemStartTime = "4",
                TaskItemEndTime = "bb"
            };

            testAgendaVM.addTaskToDay();
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
