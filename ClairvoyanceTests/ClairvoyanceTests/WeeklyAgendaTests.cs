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
            for (int i = 0; i < 5; i++)
            {
                testAgendaVMFullWeek.addTaskToDay();
            }
            Assert.IsTrue(testAgendaVMFullWeek.DaysToDisplay[1].TaskList.Count == 5);
        }

        [TestMethod]
        public void addMultipleTaskDifferentDaysTest()
        {
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
            Assert.IsTrue(testAgendaVMFullWeek.DaysToDisplay[1].TaskList.Count == 3 && testAgendaVMFullWeek.DaysToDisplay[3].TaskList.Count == 3);
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
        public void addTaskListStringMonTest()
        {
            testAgendaVMFullWeek.TaskItemDay = "Mon";
            for (int i = 0; i < 3; i++)
            {
                testAgendaVMFullWeek.addTaskToDay();
            }
            Assert.IsTrue(testAgendaVMFullWeek.monTaskListString.Count == 3);
        }

        [TestMethod]
        public void addTaskListStringTuesTest()
        {
            testAgendaVMFullWeek.TaskItemDay = "Tues";
            for (int i = 0; i < 3; i++)
            {
                testAgendaVMFullWeek.addTaskToDay();
            }
            Assert.IsTrue(testAgendaVMFullWeek.tuesTaskListString.Count == 3);
        }

        [TestMethod]
        public void addTaskListStringWedTest()
        {
            testAgendaVMFullWeek.TaskItemDay = "Wed";
            for (int i = 0; i < 3; i++)
            {
                testAgendaVMFullWeek.addTaskToDay();
            }
            Assert.IsTrue(testAgendaVMFullWeek.wedTaskListString.Count == 3);
        }

        [TestMethod]
        public void addTaskListStringThursTest()
        {
            testAgendaVMFullWeek.TaskItemDay = "Thurs";
            for (int i = 0; i < 3; i++)
            {
                testAgendaVMFullWeek.addTaskToDay();
            }
            Assert.IsTrue(testAgendaVMFullWeek.thursTaskListString.Count == 3);
        }

        [TestMethod]
        public void addTaskListStringFriTest()
        {
            testAgendaVMFullWeek.TaskItemDay = "Fri";
            for (int i = 0; i < 3; i++)
            {
                testAgendaVMFullWeek.addTaskToDay();
            }
            Assert.IsTrue(testAgendaVMFullWeek.friTaskListString.Count == 3);
        }

        [TestMethod]
        public void addTaskListStringSatTest()
        {
            testAgendaVMFullWeek.TaskItemDay = "Sat";
            for (int i = 0; i < 3; i++)
            {
                testAgendaVMFullWeek.addTaskToDay();
            }
            Assert.IsTrue(testAgendaVMFullWeek.satTaskListString.Count == 3);
        }

        [TestMethod]
        public void addTaskListStringSunTest()
        {
            testAgendaVMFullWeek.TaskItemDay = "Sun";
            for (int i = 0; i < 3; i++)
            {
                testAgendaVMFullWeek.addTaskToDay();
            }
            Assert.IsTrue(testAgendaVMFullWeek.sunTaskListString.Count == 3);
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
