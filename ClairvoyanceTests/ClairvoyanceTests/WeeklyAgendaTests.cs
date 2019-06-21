using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Clairvoyance;
using Clairvoyance.ViewModel;
using Clairvoyance.Model;
using Clairvoyance.Data;

namespace ClairvoyanceTests
{
    [TestClass]
    public class WeeklyAgendaTests
    {
        WeeklyAgendaViewModel testAgendaVMFullWeek;
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
            
            testAgendaVMFullWeek = new WeeklyAgendaViewModel()
            {
                TaskItemName = defaultTaskItem.TaskName,
                TaskItemCategory = defaultCategory,
                TaskItemStartTime = defaultTaskItem.TaskStartDateTime.ToString(),
                TaskItemEndTime = defaultTaskItem.TaskEndDateTime.ToString()
            };

            testAgendaVMFullWeek.CategoryList.Add("Test Cat");
            testAgendaVMFullWeek.updateDaysToDisplay();
        }

        [TestMethod]
        public void fullWeekTestCount()
        {
            Assert.IsTrue(testAgendaVMFullWeek.DaysToDisplay.Count == 7);
        }

        [TestMethod]
        public void fullWeekTestFirstObject()
        {
            Assert.IsTrue(testAgendaVMFullWeek.DaysToDisplay[0].NameOfDay == "Mon");
        }

        [TestMethod]
        public void fullWeekTestLastObject()
        {
            List<DayPlanner> fullWeekList = new List<DayPlanner>();
            fullWeekList = testAgendaVMFullWeek.DaysToDisplay;

            Assert.IsTrue(fullWeekList[fullWeekList.Count - 1].NameOfDay == "Sun");
        }

        [TestMethod]
        public void addTaskTest()
        {
            testAgendaVMFullWeek.TaskItemDay = "Tues";
            testAgendaVMFullWeek.addTaskToDay();

            int taskCount = testAgendaVMFullWeek.DaysToDisplay[1].TaskList.Count;
            Assert.IsTrue(testAgendaVMFullWeek.DaysToDisplay[1].TaskList[taskCount - 1].TaskName == defaultTaskItem.TaskName);
        }

        [TestMethod]
        public void addMultipleTaskSameDayTest()
        {
            testAgendaVMFullWeek.TaskItemDay = "Tues";
            int originalTaskCount = testAgendaVMFullWeek.DaysToDisplay[1].TaskList.Count;

            for (int i = 0; i < 5; i++)
            {
                testAgendaVMFullWeek.addTaskToDay();
            }

            Assert.IsTrue(testAgendaVMFullWeek.DaysToDisplay[1].TaskList.Count == originalTaskCount + 5);
        }

        [TestMethod]
        public void addMultipleTaskDifferentDaysTest()
        {
            int origTuesTaskCount = testAgendaVMFullWeek.DaysToDisplay[1].TaskList.Count;
            int origThursTaskCount = testAgendaVMFullWeek.DaysToDisplay[3].TaskList.Count;

            testAgendaVMFullWeek.TaskItemDay = "Tues";
            for (int i = 0; i < 3; i++)
            {
                testAgendaVMFullWeek.addTaskToDay();
            }
            testAgendaVMFullWeek.TaskItemDay = "Thurs";
            for (int i = 0; i < 3; i++)
            {
                testAgendaVMFullWeek.addTaskToDay();
            }

            int newTuesTaskCount = testAgendaVMFullWeek.DaysToDisplay[1].TaskList.Count;
            int newThursTaskCount = testAgendaVMFullWeek.DaysToDisplay[3].TaskList.Count;
            Assert.IsTrue(newTuesTaskCount == origTuesTaskCount + 3);
            Assert.IsTrue(newThursTaskCount == origThursTaskCount + 3);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException),
            "DaysToDisplay has not been initialized.")]
        public void addTaskDaysToDisplayNullTest()
        {
            testAgendaVMFullWeek.DaysToDisplay = new List<DayPlanner>();
            testAgendaVMFullWeek.TaskItemDay = "Tues";
            testAgendaVMFullWeek.addTaskToDay();
        }

        [TestMethod]
        public void addTaskListMonTest()
        {
            int origTaskCount = 0;

            if (testAgendaVMFullWeek.MonTaskItemList != null)
            {
                origTaskCount = testAgendaVMFullWeek.MonTaskItemList.Count;
            }

            testAgendaVMFullWeek.TaskItemDay = "Mon";
            for (int i = 0; i < 3; i++)
            {
                testAgendaVMFullWeek.addTaskToDay();
            }

            int newTaskCount = testAgendaVMFullWeek.MonTaskItemList.Count;

            Assert.IsTrue(newTaskCount - origTaskCount == 3);
        }

        [TestMethod]
        public void addTaskListTuesTest()
        {
            int origTaskCount = 0;

            if (testAgendaVMFullWeek.TuesTaskItemList != null)
            {
                origTaskCount = testAgendaVMFullWeek.TuesTaskItemList.Count;
            }

            testAgendaVMFullWeek.TaskItemDay = "Tues";
            for (int i = 0; i < 3; i++)
            {
                testAgendaVMFullWeek.addTaskToDay();
            }

            int newTaskCount = testAgendaVMFullWeek.TuesTaskItemList.Count;

            Assert.IsTrue(newTaskCount - origTaskCount == 3);
        }

        [TestMethod]
        public void addTaskListWedTest()
        {
            int origTaskCount = 0;

            if (testAgendaVMFullWeek.WedTaskItemList != null)
            {
                origTaskCount = testAgendaVMFullWeek.WedTaskItemList.Count;
            }

            testAgendaVMFullWeek.TaskItemDay = "Wed";
            for (int i = 0; i < 3; i++)
            {
                testAgendaVMFullWeek.addTaskToDay();
            }

            int newTaskCount = testAgendaVMFullWeek.WedTaskItemList.Count;

            Assert.IsTrue(newTaskCount - origTaskCount == 3);
        }

        [TestMethod]
        public void addTaskListThursTest()
        {
            int origTaskCount = 0;

            if (testAgendaVMFullWeek.ThursTaskItemList != null)
            {
                origTaskCount = testAgendaVMFullWeek.ThursTaskItemList.Count;
            }

            testAgendaVMFullWeek.TaskItemDay = "Thurs";
            for (int i = 0; i < 3; i++)
            {
                testAgendaVMFullWeek.addTaskToDay();
            }

            int newTaskCount = testAgendaVMFullWeek.ThursTaskItemList.Count;

            Assert.IsTrue(newTaskCount - origTaskCount == 3);
        }

        [TestMethod]
        public void addTaskListFriTest()
        {
            int origTaskCount = 0;

            if (testAgendaVMFullWeek.FriTaskItemList != null)
            {
                origTaskCount = testAgendaVMFullWeek.FriTaskItemList.Count;
            }

            testAgendaVMFullWeek.TaskItemDay = "Fri";
            for (int i = 0; i < 3; i++)
            {
                testAgendaVMFullWeek.addTaskToDay();
            }

            int newTaskCount = testAgendaVMFullWeek.FriTaskItemList.Count;

            Assert.IsTrue(newTaskCount - origTaskCount == 3);
        }

        [TestMethod]
        public void addTaskListSatTest()
        {
            int origTaskCount = 0;

            if (testAgendaVMFullWeek.SatTaskItemList != null)
            {
                origTaskCount = testAgendaVMFullWeek.SatTaskItemList.Count;
            }

            testAgendaVMFullWeek.TaskItemDay = "Sat";
            for (int i = 0; i < 3; i++)
            {
                testAgendaVMFullWeek.addTaskToDay();
            }

            int newTaskCount = testAgendaVMFullWeek.SatTaskItemList.Count;

            Assert.IsTrue(newTaskCount - origTaskCount == 3);
        }

        [TestMethod]
        public void addTaskListSunTest()
        {
            int origTaskCount = 0;

            if (testAgendaVMFullWeek.SunTaskItemList != null)
            {
                origTaskCount = testAgendaVMFullWeek.SunTaskItemList.Count;
            }

            testAgendaVMFullWeek.TaskItemDay = "Sun";
            for (int i = 0; i < 3; i++)
            {
                testAgendaVMFullWeek.addTaskToDay();
            }

            int newTaskCount = testAgendaVMFullWeek.SunTaskItemList.Count;

            Assert.IsTrue(newTaskCount - origTaskCount == 3);
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
