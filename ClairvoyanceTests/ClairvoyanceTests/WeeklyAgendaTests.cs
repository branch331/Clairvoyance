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

            testAgendaVMFullWeek = new WeeklyAgendaViewModel();
            testAgendaVMFullWeek.isWorkWeek = false;
        }

        [TestMethod]
        public void workWeekTestCount()
        {
            Assert.IsTrue(testAgendaVMWorkWeek.generateDaysToDisplayList().Count == 5);
        }

        [TestMethod]
        public void fullWeekTestCount()
        {
            Assert.IsTrue(testAgendaVMFullWeek.generateDaysToDisplayList().Count == 7);
        }

        [TestMethod]
        public void workWeekTestFirstObject()
        {
            Assert.IsTrue(testAgendaVMWorkWeek.generateDaysToDisplayList()[0].NameOfDay == "Mon");
        }

        [TestMethod]
        public void fullWeekTestFirstObject()
        {
            Assert.IsTrue(testAgendaVMFullWeek.generateDaysToDisplayList()[0].NameOfDay == "Mon");
        }

        [TestMethod]
        public void workWeekTestLastObject()
        {
            List<DayPlannerModel> workWeekList = new List<DayPlannerModel>();
            workWeekList = testAgendaVMWorkWeek.generateDaysToDisplayList();

            Assert.IsTrue(workWeekList[workWeekList.Count - 1].NameOfDay == "Fri");
        }

        [TestMethod]
        public void fullWeekTestLastObject()
        {
            List<DayPlannerModel> fullWeekList = new List<DayPlannerModel>();
            fullWeekList = testAgendaVMFullWeek.generateDaysToDisplayList();

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
