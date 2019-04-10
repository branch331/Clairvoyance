using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Clairvoyance;

namespace ClairvoyanceTests
{
    [TestClass]
    public class WeeklyAgendaTests
    {
        WeeklyAgendaViewModel testAgendaVMWorkWeek;
        WeeklyAgendaViewModel testAgendaVMFullWeek;

        [TestInitialize]
        public void setUpTestViewModels()
        {
            testAgendaVMWorkWeek = new WeeklyAgendaViewModel();
            testAgendaVMWorkWeek.isWorkWeek = true;
            testAgendaVMWorkWeek.updateDaysToDisplay();

            testAgendaVMFullWeek = new WeeklyAgendaViewModel();
            testAgendaVMFullWeek.isWorkWeek = false;
            testAgendaVMFullWeek.updateDaysToDisplay();
        }

        [TestMethod]
        public void workWeekTestCount()
        {
            Assert.IsTrue(testAgendaVMWorkWeek.DaysToDisplay.Count == 5);
        }

        [TestMethod]
        public void fullWeekTestCount()
        {
            Assert.IsTrue(testAgendaVMFullWeek.DaysToDisplay.Count == 7);
        }

        [TestMethod]
        public void workWeekTestFirstObject()
        {
            Assert.IsTrue(testAgendaVMWorkWeek.DaysToDisplay[0].NameOfDay == "Mon");
        }

        [TestMethod]
        public void fullWeekTestFirstObject()
        {
            Assert.IsTrue(testAgendaVMFullWeek.DaysToDisplay[0].NameOfDay == "Mon");
        }

        [TestMethod]
        public void workWeekTestLastObject()
        {
            List<DayPlannerModel> workWeekList = new List<DayPlannerModel>();
            workWeekList = testAgendaVMWorkWeek.DaysToDisplay;

            Assert.IsTrue(workWeekList[workWeekList.Count - 1].NameOfDay == "Fri");
        }

        [TestMethod]
        public void fullWeekTestLastObject()
        {
            List<DayPlannerModel> fullWeekList = new List<DayPlannerModel>();
            fullWeekList = testAgendaVMFullWeek.DaysToDisplay;

            Assert.IsTrue(fullWeekList[fullWeekList.Count - 1].NameOfDay == "Sun");
        }

        [TestMethod]
        public void addTaskTest()
        {
            testAgendaVMFullWeek.addTaskToDay("Test Task", "Tues");
            Assert.IsTrue(testAgendaVMFullWeek.DaysToDisplay[1].TaskList[0].TaskName == "Test Task");
        }
    }
}
