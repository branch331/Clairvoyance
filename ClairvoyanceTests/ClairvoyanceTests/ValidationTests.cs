using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Clairvoyance.ViewModel;

namespace ClairvoyanceTests
{
    [TestClass]
    public class ValidationTests
    {
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
                TaskItemCategory = "Cat",
                TaskItemStartTime = "1",
                TaskItemEndTime = "5:30"
            };

            testAgendaVM.CategoryToAdd = "Cat";
            testAgendaVM.CategoryList.Add("Cat");

            testAgendaVM.addTaskToDay();
            Assert.IsTrue(testAgendaVM.DaysToDisplay[0].TaskList[0].TaskName == "Test Task");
        }

        [TestMethod]
        public void addStartTimeTwelve()
        {
            WeeklyAgendaViewModel testAgendaVM = new WeeklyAgendaViewModel()
            {
                TaskItemName = "Test Task",
                TaskItemDay = "Mon",
                TaskItemCategory = "Cat",
                TaskItemStartTime = "12",
                TaskItemEndTime = "5:30"
            };

            testAgendaVM.CategoryToAdd = "Cat";
            testAgendaVM.CategoryList.Add("Cat");

            testAgendaVM.addTaskToDay();
            Assert.IsTrue(testAgendaVM.DaysToDisplay[0].TaskList[0].TaskName == "Test Task");
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

        [TestMethod]
        public void addEndTimeOne()
        {
            WeeklyAgendaViewModel testAgendaVM = new WeeklyAgendaViewModel()
            {
                TaskItemName = "Test Task",
                TaskItemDay = "Mon",
                TaskItemCategory = "Cat",
                TaskItemStartTime = "4",
                TaskItemEndTime = "1"
            };

            testAgendaVM.CategoryToAdd = "Cat";
            testAgendaVM.CategoryList.Add("Cat");

            testAgendaVM.addTaskToDay();
            Assert.IsTrue(testAgendaVM.DaysToDisplay[0].TaskList[0].TaskName == "Test Task");
        }

        [TestMethod]
        public void addEndTimeTwelve()
        {
            WeeklyAgendaViewModel testAgendaVM = new WeeklyAgendaViewModel()
            {
                TaskItemName = "Test Task",
                TaskItemDay = "Mon",
                TaskItemCategory = "Cat",
                TaskItemStartTime = "4",
                TaskItemEndTime = "12"
            };

            testAgendaVM.CategoryToAdd = "Cat";
            testAgendaVM.CategoryList.Add("Cat");

            testAgendaVM.addTaskToDay();
            Assert.IsTrue(testAgendaVM.DaysToDisplay[0].TaskList[0].TaskName == "Test Task");
        }
    }
}
